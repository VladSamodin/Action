using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using ExpressionTransformer;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        //private readonly IRepository<DalUser> userRepository;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        public BllUser Create(BllUser bllUser)
        {
            BllUser newUser = userRepository.Create(bllUser.ToDalUser()).ToBll();
            uow.Commit();
            return newUser;
        }

        public void Delete(BllUser bllUser)
        {
            userRepository.Delete(bllUser.ToDalUser());
            uow.Commit();
        }

        public void Update(BllUser bllUser)
        {
            userRepository.Update(bllUser.ToDalUser());
            uow.Commit();
        }

        public int Count()
        {
            return userRepository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<BllUser, bool>> expression)
        {
            return userRepository.Count(ExpressionTransformer<BllUser, DalUser>.Transform(expression));
        }

        public IEnumerable<BllUser> GetAll()
        {
            return userRepository.GetAll().Select(dalUser => dalUser.ToBllUser());
        }

        public BllUser GetById(int id)
        {
            DalUser dalUser = userRepository.GetById(id);
            return dalUser != null ? dalUser.ToBllUser() : null;
        }

        public IEnumerable<BllUser> GetByPredicate(System.Linq.Expressions.Expression<System.Func<BllUser, bool>> expression)
        {
            return userRepository.GetByPredicate(ExpressionTransformer<BllUser, DalUser>.Transform(expression)).Select(dalUser => dalUser.ToBllUser());
        }

        public void AddRole(BllUser bllUser, BllRole bllRole)
        {
            DalUser dalUser = userRepository.GetById(bllUser.Id);
            //??????????????????????????????????????????????????
            userRepository.AddRole(dalUser, bllRole.ToDal());
        }

        public void RemoveRole(BllUser bllUser, BllRole bllRole)
        {
            DalUser dalUser = userRepository.GetById(bllUser.Id);
            userRepository.RemoveRole(dalUser, bllRole.ToDal());
        }

        public IEnumerable<BllRole> GetRoles(BllUser bllUser)
        {
            return userRepository.GetRoles(bllUser.ToDal()).Select(r => r.ToBll());
        }
    }
}
