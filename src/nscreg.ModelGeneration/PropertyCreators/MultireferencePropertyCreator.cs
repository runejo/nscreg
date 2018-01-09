using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using nscreg.Data.Entities;
using nscreg.ModelGeneration.PropertiesMetadata;
using nscreg.Utilities.Attributes;

namespace nscreg.ModelGeneration.PropertyCreators
{
    /// <summary>
    /// Класс создатель свойства много-ссылочности
    /// </summary>
    public class MultireferencePropertyCreator : PropertyCreatorBase
    {
        /// <summary>
        /// Метод проверки создания свойства много-ссылочности
        /// </summary>
        public override bool CanCreate(PropertyInfo propInfo)
        {
            var type = propInfo.PropertyType;
            return type.GetTypeInfo().IsGenericType
                   && type.GetGenericTypeDefinition() == typeof(ICollection<>)
                   && (typeof(IStatisticalUnit).IsAssignableFrom(type.GetGenericArguments()[0])
                   || typeof(IIdentifiable).IsAssignableFrom(type.GetGenericArguments()[0]))
                   && propInfo.IsDefined(typeof(ReferenceAttribute));
        }

        /// <summary>
        /// Метод создатель свойства много-ссылочности
        /// </summary>
        public override PropertyMetadataBase Create(PropertyInfo propInfo, object obj, bool writable, bool mandatory = false)
        {
            var isIidentifiable =
                typeof(IIdentifiable).IsAssignableFrom(propInfo.PropertyType.GetGenericArguments()[0]);
            return new MultiReferenceProperty(
                propInfo.Name,
                obj == null
                    ? Enumerable.Empty<int>()
                    : isIidentifiable
                        ? ((IEnumerable<object>)propInfo.GetValue(obj)).Cast<IIdentifiable>().Select(x => x.Id)
                        : ((IEnumerable<object>) propInfo.GetValue(obj)).Cast<IStatisticalUnit>()
                        .Where(v => !v.IsDeleted && v.ParentId == null).Select(x => x.RegId),
                ((ReferenceAttribute) propInfo.GetCustomAttribute(typeof(ReferenceAttribute))).Lookup,
                mandatory,
                propInfo.GetCustomAttribute<DisplayAttribute>()?.GroupName,
                writable: writable);
        }
    }
}
