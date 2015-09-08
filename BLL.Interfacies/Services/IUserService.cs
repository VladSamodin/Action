using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        BllUser GetUserEntity(int id);
        IEnumerable<BllUser> GetAllUserEntities();
        void CreateUser(BllUser user);
        void DeleteUser(BllUser user);     
        //etc.
    }
}