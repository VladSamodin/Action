using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalModerationStatus : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /*
        public static bool operator ==(DalModerationStatus lhs, DalModerationStatus rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (Equals(lhs, null) || Equals(rhs, null))
                return false;
            return lhs.Id == rhs.Id && lhs.Name == rhs.Name;
        }

        public static bool operator !=(DalModerationStatus lhs, DalModerationStatus rhs)
        {
            return !(lhs == rhs);
        }
        */
    }
}
