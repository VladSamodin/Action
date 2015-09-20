using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        void AddUser(DalRole role, DalUser user);
        void RemoveUser(DalRole role, DalUser user);
        IEnumerable<DalUser> GetUsers(DalRole role);
    }
}
