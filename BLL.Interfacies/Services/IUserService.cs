using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService : IService<BllUser>
    {
        void AddRole(BllUser user, BllRole role);
        void RemoveRole(BllUser user, BllRole role);
        IEnumerable<BllRole> GetRoles(BllUser user);
    }
}