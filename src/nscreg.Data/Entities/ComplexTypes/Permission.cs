namespace nscreg.Data.Entities.ComplexTypes
{
    public class Permission
    {
        public Permission(string propertyName, bool canRead, bool canWrite)
        {
            PropertyName = propertyName;
            CanRead = canRead;
            CanWrite = canWrite;
        }

        public Permission()
        {
        }

        public string PropertyName { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (Permission) obj;
            return PropertyName == p.PropertyName && CanRead == p.CanRead && CanWrite == p.CanWrite;
        }
    }
}
