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
    public class ModerationStatusService : IService<BllModerationStatus>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalModerationStatus> moderationStatusRepository;

        public ModerationStatusService(IUnitOfWork uow, IRepository<DalModerationStatus> repository)
        {
            this.uow = uow;
            this.moderationStatusRepository = repository;
        }

        public BllModerationStatus Create(BllModerationStatus bllModerationStatus)
        {
            BllModerationStatus newModerationStatus = moderationStatusRepository.Create(bllModerationStatus.ToDalModerationStatus()).ToBllModerationStatus();
            uow.Commit();
            return newModerationStatus;
        }

        public void Delete(BllModerationStatus bllModerationStatus)
        {
            moderationStatusRepository.Delete(bllModerationStatus.ToDalModerationStatus());
            uow.Commit();
        }

        public BllModerationStatus Update(BllModerationStatus bllModerationStatus)
        {
            DalModerationStatus oldModerationStatus = moderationStatusRepository.Update(bllModerationStatus.ToDalModerationStatus());
            uow.Commit();
            return oldModerationStatus == null ? null : oldModerationStatus.ToBll();
        }

        public int Count()
        {
            return moderationStatusRepository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<BllModerationStatus, bool>> expression)
        {
            return moderationStatusRepository.Count(ExpressionTransformer<BllModerationStatus, DalModerationStatus>.Transform(expression));
        }

        public IEnumerable<BllModerationStatus> GetAll()
        {
            return moderationStatusRepository.GetAll().Select(dalModerationStatus => dalModerationStatus.ToBllModerationStatus());
        }

        public BllModerationStatus GetById(int id)
        {
            DalModerationStatus dalModerationStatus = moderationStatusRepository.GetById(id);
            return dalModerationStatus != null ? dalModerationStatus.ToBllModerationStatus() : null;
        }

        public IEnumerable<BllModerationStatus> GetByPredicate(System.Linq.Expressions.Expression<System.Func<BllModerationStatus, bool>> expression)
        {
            return moderationStatusRepository.GetByPredicate(ExpressionTransformer<BllModerationStatus, DalModerationStatus>.Transform(expression)).Select(dalModerationStatus => dalModerationStatus.ToBllModerationStatus());
        }
    }
}
