﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using nscreg.Data.Entities;
using nscreg.ModelGeneration.PropertiesMetadata;

namespace nscreg.ModelGeneration.PropertyCreators
{
    public class ActivityPropertyCreator : PropertyCreatorBase
    {
        public override bool CanCreate(PropertyInfo propInfo)
        {
            var type = propInfo.PropertyType;
            return type.GetTypeInfo().IsGenericType
                   && type.GetGenericTypeDefinition() == typeof(ICollection<>)
                   && type.GenericTypeArguments[0] == typeof(Activity);
        }

        public override PropertyMetadataBase Create(PropertyInfo propInfo, object obj)
        {
            return new ActivityPropertyMetadata(
                propInfo.Name,
                true,
                obj == null ? Enumerable.Empty<Activity>() : (IEnumerable<Activity>) propInfo.GetValue(obj)
            );
        }
    }
}
