using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using nscreg.Server.Common.Models.DataAccess;
using nscreg.Utilities;

namespace nscreg.Server.Common.Models.Users
{
    /// <summary>
    /// Модель редактирования пользователя
    /// </summary>
    public class UserEditM : IUserSubmit
    {
        [Required, PrintableString]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public IEnumerable<string> AssignedRoles { get; set; }

        public string Description { get; set; }

        public DataAccessModel DataAccess { get; set; }

        [Required]
        public IEnumerable<int> UserRegions { get; set; }
    }
}