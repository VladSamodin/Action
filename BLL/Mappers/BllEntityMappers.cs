using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        #region Bid
        public static DalBid ToDalBid(this BllBid bid)
        {
            return new DalBid
            {
                Id = bid.Id,
                Sum = bid.Sum,
                DateTime = bid.DateTime,
                LotId = bid.LotId,
                UserId = bid.UserId
            };
        }

        public static BllBid ToBllBid(this DalBid bid)
        {
            return new BllBid
            {
                Id = bid.Id,
                Sum = bid.Sum,
                DateTime = bid.DateTime,
                LotId = bid.LotId,
                UserId = bid.UserId
            };
        }
        #endregion

        #region Category
        public static DalCategory ToDalCategory(this BllCategory category)
        {
            return new DalCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static BllCategory ToBllCategory(this DalCategory category)
        {
            return new BllCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        #endregion

        #region Lot
        public static DalLot ToDalLot(this BllLot lot)
        {
            return new DalLot
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                StartPrice = lot.StartPrice,
                StartDateTime = lot.StartDateTime,
                FinishDateTime = lot.FinishDateTime,
                Image = lot.Image,
                CategoryId = lot.CategoryId,
                OwnerId = lot.OwnerId,

                ModeratorId = lot.ModeratorId,
                ModerationDateTime = lot.ModerationDateTime,
                ModerationStatusId = lot.ModerationStatusId,
                ModeratorMessage = lot.ModeratorMessage
            };
        }

        public static BllLot ToBllLot(this DalLot lot)
        {
            return new BllLot
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                StartPrice = lot.StartPrice,
                StartDateTime = lot.StartDateTime,
                FinishDateTime = lot.FinishDateTime,
                Image = lot.Image,
                CategoryId = lot.CategoryId,
                OwnerId = lot.OwnerId,

                Sold = lot.Sold,
                ModeratorId = lot.ModeratorId,
                ModerationDateTime = lot.ModerationDateTime,
                ModerationStatusId = lot.ModerationStatusId,
                ModeratorMessage = lot.ModeratorMessage
            };
        }
        #endregion

        #region ModerationStatus
        public static DalModerationStatus ToDalModerationStatus(this BllModerationStatus moderationStatus)
        {
            return new DalModerationStatus()
            {
                Id = moderationStatus.Id,
                Name = moderationStatus.Name
            };
        }

        public static BllModerationStatus ToBllModerationStatus(this DalModerationStatus moderationStatus)
        {
            return new BllModerationStatus()
            {
                Id = moderationStatus.Id,
                Name = moderationStatus.Name
            };
        }
        #endregion

        #region Role
        public static DalRole ToDalRole(this BllRole role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static BllRole ToBllRole(this DalRole role)
        {
            return new BllRole
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
        #endregion

        #region User
        public static DalUser ToDalUser(this BllUser user)
        {
            return new DalUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }

        public static BllUser ToBllUser(this DalUser user)
        {
            return new BllUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }
        #endregion



        public static DAL.Interface.DTO.IEntity ToDal(this BLL.Interface.Entities.IEntity bllEntity)
        {
            return ToDal((dynamic)bllEntity);
        }

        public static BLL.Interface.Entities.IEntity ToBll(this BLL.Interface.Entities.IEntity dalEntity)
        {
            return ToBll((dynamic)dalEntity);
        }

        #region Bid
        public static DalBid ToDal(this BllBid bid)
        {
            return new DalBid
            {
                Id = bid.Id,
                Sum = bid.Sum,
                DateTime = bid.DateTime,
                LotId = bid.LotId,
                UserId = bid.UserId
            };
        }

        public static BllBid ToBll(this DalBid bid)
        {
            return new BllBid
            {
                Id = bid.Id,
                Sum = bid.Sum,
                DateTime = bid.DateTime,
                LotId = bid.LotId,
                UserId = bid.UserId
            };
        }
        #endregion

        #region Category
        public static DalCategory ToDal(this BllCategory category)
        {
            return new DalCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static BllCategory ToBll(this DalCategory category)
        {
            return new BllCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        #endregion

        #region Lot
        public static DalLot ToDal(this BllLot lot)
        {
            return new DalLot
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                StartPrice = lot.StartPrice,
                StartDateTime = lot.StartDateTime,
                FinishDateTime = lot.FinishDateTime,
                Image = lot.Image,
                CategoryId = lot.CategoryId,
                OwnerId = lot.OwnerId,

                ModeratorId = lot.ModeratorId,
                ModerationDateTime = lot.ModerationDateTime,
                ModerationStatusId = lot.ModerationStatusId,
                ModeratorMessage = lot.ModeratorMessage
            };
        }

        public static BllLot ToBll(this DalLot lot)
        {
            return new BllLot
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                StartPrice = lot.StartPrice,
                StartDateTime = lot.StartDateTime,
                FinishDateTime = lot.FinishDateTime,
                Image = lot.Image,
                CategoryId = lot.CategoryId,
                OwnerId = lot.OwnerId,

                Sold = lot.Sold,
                ModeratorId = lot.ModeratorId,
                ModerationDateTime = lot.ModerationDateTime,
                ModerationStatusId = lot.ModerationStatusId,
                ModeratorMessage = lot.ModeratorMessage
            };
        }
        #endregion

        #region ModerationStatus
        public static DalModerationStatus ToDal(this BllModerationStatus moderationStatus)
        {
            return new DalModerationStatus()
            {
                Id = moderationStatus.Id,
                Name = moderationStatus.Name
            };
        }

        public static BllModerationStatus ToBll(this DalModerationStatus moderationStatus)
        {
            return new BllModerationStatus()
            {
                Id = moderationStatus.Id,
                Name = moderationStatus.Name
            };
        }
        #endregion

        #region Role
        public static DalRole ToDal(this BllRole role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static BllRole ToBll(this DalRole role)
        {
            return new BllRole
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
        #endregion

        #region User
        public static DalUser ToDal(this BllUser user)
        {
            return new DalUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }

        public static BllUser ToBll(this DalUser user)
        {
            return new BllUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }
        #endregion
    }
}
