using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        void AddRole(DalUser user, DalRole role);
        void RemoveRole(DalUser user, DalRole role);
        IEnumerable<DalRole> GetRoles(DalUser user);
    }
}