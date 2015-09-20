namespace DAL.Interface.DTO
{
    public class DalRole : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public System.Collections.IEnumerable Roles { get; set; }
    }
}
