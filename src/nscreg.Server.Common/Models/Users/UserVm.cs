using System.Collections.Generic;
using System.Linq;
using nscreg.Data.Constants;
using nscreg.Data.Entities;
using nscreg.Server.Common.Models.DataAccess;

namespace nscreg.Server.Common.Models.Users
{
    /// <summary>
    /// Вью модель пользователя
    /// </summary>
    public class UserVm
    {
        /// <summary>
        /// Метод создания вью модели пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static UserVm Create(User user, IEnumerable<string> roles) => new UserVm
        {
            Id = user.Id,
            Login = user.Login,
            Name = user.Name,
            Phone = user.PhoneNumber,
            Email = user.Email,
            Description = user.Description,
            AssignedRoles = roles,
            Status = user.Status,
            DataAccess = DataAccessModel.FromString(user.DataAccess),
            UserRegions = user.UserRegions.Select(x => x.RegionId.ToString()).ToList(),
        };

        public string Id { get; private set; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<string> AssignedRoles { get; private set; }
        public DataAccessModel DataAccess { get; private set; }
        public UserStatuses Status { get; private set; }
        public ICollection<string> UserRegions { get; set; }
    }
}
