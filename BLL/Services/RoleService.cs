﻿using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using ExpressionTransformer;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class RoleService : IService<BllRole>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalRole> roleRepository;

        public RoleService(IUnitOfWork uow, IRepository<DalRole> repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        public void Create(BllRole bllRole)
        {
            roleRepository.Create(bllRole.ToDalRole());
            uow.Commit();
        }

        public void Delete(BllRole bllRole)
        {
            roleRepository.Delete(bllRole.ToDalRole());
            uow.Commit();
        }

        public void Update(BllRole bllRole)
        {
            roleRepository.Update(bllRole.ToDalRole());
            uow.Commit();
        }

        public int Count()
        {
            return roleRepository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<BllRole, bool>> expression)
        {
            return roleRepository.Count(ExpressionTransformer<BllRole, DalRole>.Transform(expression));
        }

        public IEnumerable<BllRole> GetAll()
        {
            return roleRepository.GetAll().Select(dalRole => dalRole.ToBllRole());
        }

        public BllRole GetById(int id)
        {
            DalRole dalRole = roleRepository.GetById(id);
            return dalRole != null ? dalRole.ToBllRole() : null;
        }

        public IEnumerable<BllRole> GetByPredicate(System.Linq.Expressions.Expression<System.Func<BllRole, bool>> expression)
        {
            return roleRepository.GetByPredicate(ExpressionTransformer<BllRole, DalRole>.Transform(expression)).Select(dalRole => dalRole.ToBllRole());
        }
    }
}