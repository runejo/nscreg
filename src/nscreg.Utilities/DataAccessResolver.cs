using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using nscreg.Utilities.Classes;
using nscreg.Utilities.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace nscreg.Utilities
{
    /// <summary>
    /// Класс распознования доступа к данным
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal static class DataAccessResolver<T> where T: class
    {
        private static Dictionary<string, DataProperty<T>> _properties
            = typeof(T).GetProperties().ToDictionary(v => v.Name, v => new DataProperty<T>(v));

        private static JsonSerializer _serializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static object Execute(T obj, ISet<string> propNames, Action<JObject> postProcessor)
        {
            var jo = new JObject();
            foreach (var property in _properties)
            {
                if (propNames.Contains(DataAccessAttributesHelper.GetName<T>(property.Key)) && property.Value.Getter != null)
                {
                    var value = property.Value.Getter(obj); 
                    //TODO: noraml processing of serialization
                    var metadata = property.Value.Property.PropertyType.GetTypeInfo();
                    if (metadata.IsGenericType && metadata.GetGenericTypeDefinition() == typeof(ICollection<>)) continue;
                    jo.Add(property.Key.LowerFirstLetter(), value == null ? null : JToken.FromObject(value, _serializer));
                }
            }
            postProcessor?.Invoke(jo);
            return jo.ToObject<object>();
        }
    }

    //TODO: Replace Action<JObject> to Extend with object / base class for serializer
    public static class DataAccessResolver
    {
        private static readonly MethodInfo DataAccessDowncast =
            typeof(DataAccessResolver).GetMethod(nameof(Execute));

        public static object Execute<T>(T obj, ISet<string> propNames, Action<JObject> postProcessor = null) where T: class
        {
            if (obj.GetType() != typeof(T))
            {
                return Downcast(obj, propNames, postProcessor);
            }
            return DataAccessResolver<T>.Execute(obj, propNames, postProcessor);
        }

        private static object Downcast(object obj, ISet<string> propNames, Action<JObject> postProcessor = null)
        {
            var generic = DataAccessDowncast.MakeGenericMethod(obj.GetType());
            return generic.Invoke(null, new[] { obj, propNames, postProcessor });
        }
    }
}