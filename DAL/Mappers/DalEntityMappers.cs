using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL
{
    public static class DalEntityMappers
    {
        #region Bid
        public static Bid ToOrmBid(this DalBid bid)
        {
            return new Bid
            {
                Id = bid.Id,
                Sum = bid.Sum,
                DateTime = bid.DateTime,
                LotId = bid.LotId,
                UserId = bid.UserId
            };
        }

        public static DalBid ToDalBid(this Bid bid)
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
        #endregion

        #region Category
        public static Category ToOrmCategory(this DalCategory category)
        {
            return new Category()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static DalCategory ToDalCategory(this Category category)
        {
            return new DalCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        #endregion

        #region Lot
        public static Lot ToOrmLot(this DalLot lot)
        {
            return new Lot
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
                ModerationStatusId = lot.ModerationStatus.Id,
                ModeratorMessage = lot.ModeratorMessage
            };
        }

        public static DalLot ToDalLot(this Lot lot)
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

                Sold = lot.Sold,
                ModeratorId = lot.ModeratorId,
                ModerationDateTime = lot.ModerationDateTime,
                //ModerationStatusId = lot.ModerationStatusId,
                ModerationStatus = lot.ModerationStatus.ToDalModerationStatus(),
                ModeratorMessage = lot.ModeratorMessage
            };
        }
        #endregion

        #region ModerationStatus
        public static ModerationStatus ToOrmModerationStatus(this DalModerationStatus moderationStatus)
        {
            return new ModerationStatus()
            {
                Id = moderationStatus.Id,
                Name = moderationStatus.Name
            };
        }

        public static DalModerationStatus ToDalModerationStatus(this ModerationStatus moderationStatus)
        {
            return new DalModerationStatus()
            {
                Id = moderationStatus.Id,
                Name = moderationStatus.Name
            };
        }
        #endregion

        #region Role
        public static Role ToOrmRole(this DalRole role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
        #endregion

        #region User
        public static User ToOrmUser(this DalUser user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }

        public static DalUser ToDalUser(this User user)
        {
            return new DalUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }
        #endregion
    }
}
