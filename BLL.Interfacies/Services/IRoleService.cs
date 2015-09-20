using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IRoleService : IService<BllRole>
    {
        void AddUser(BllRole role, BllUser user);
        void RemoveUser(BllRole role, BllUser user);
        IEnumerable<BllUser> GetUsers(BllRole role);
    }
}
