using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using DAL.Interface.Repository;
using ExpressionTransformer;

namespace BLL.Services
{
    public abstract class AbstractService<TBll, TDal> : IService<TBll> 
        where TBll : BLL.Interface.Entities.IEntity 
        where TDal : DAL.Interface.DTO.IEntity
    {
        protected readonly IUnitOfWork uow;
        protected readonly IRepository<TDal> repository;

        public AbstractService(IUnitOfWork uow, IRepository<TDal> repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        //посмотреть visitor
        public void Create(TBll bllEntity)
        {
            repository.Create((TDal)bllEntity.ToDal());
            uow.Commit();
        }

        public void Delete(TBll bllEntity)
        {
            repository.Delete((TDal)bllEntity.ToDal());
            uow.Commit();
        }

        public void Update(TBll bllEntity)
        {
            repository.Update((dynamic)bllEntity.ToDal());
            uow.Commit();
        }

        public int Count()
        {
            return repository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<TBll, bool>> expression)
        {
            return repository.Count(ExpressionTransformer<TBll, TDal>.Transform(expression));
        }

        public IEnumerable<TBll> GetAll()
        {
            return (IEnumerable<TBll>)repository.GetAll().Select(dalEntity => ((dynamic)dalEntity).ToBll());
        }

        public TBll GetById(int id)
        {
            TDal dalEntity = repository.GetById(id);
            return dalEntity != null ? ((dynamic)dalEntity).ToBll() : null;
        }

        public IEnumerable<TBll> GetByPredicate(System.Linq.Expressions.Expression<System.Func<TBll, bool>> expression)
        {
            return (IEnumerable<TBll>)repository.GetByPredicate(ExpressionTransformer<TBll, TDal>.Transform(expression)).Select(dalEntity => ((dynamic)dalEntity).ToBll());
        }
    }
}
