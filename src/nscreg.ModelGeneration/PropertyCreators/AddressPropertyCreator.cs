using System.ComponentModel.DataAnnotations;
using System.Reflection;
using nscreg.Data.Entities;
using nscreg.ModelGeneration.PropertiesMetadata;

namespace nscreg.ModelGeneration.PropertyCreators
{
    /// <summary>
    /// Класс создатель свойства адреса
    /// </summary>
    public class AddressPropertyCreator : IPropertyCreator
    {
        public bool CanCreate(PropertyInfo propInfo)
        {
            return propInfo.PropertyType == typeof(Address);
        }

        /// <summary>
        /// Метод создатель свойства адреса
        /// </summary>
        public PropertyMetadataBase Create(PropertyInfo propInfo, object obj, bool writable, bool mandatory = false)
        {
            return new AddressPropertyMetadata(
               propInfo.Name,
               mandatory,
               obj == null ? new Address() : (Address)propInfo.GetValue(obj),
               propInfo.GetCustomAttribute<DisplayAttribute>()?.GroupName,
               writable: writable
           );
        }
    }
}
