using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using ORM.Entities;

namespace TestORMConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory())));
            ORM.EntityModel em = new ORM.EntityModel("name=EntityModel");
            ORM.Entities.User user = em.Set<User>().Add(new ORM.Entities.User
            {
                Name = "test",
                Password = "12345",
                Email = "sdfsdf@sfsf.com",
            });
            /*
            em.Lots.Add(new ORM.Entities.Lot
            {
                Name = "sdfasf",
                Category = new ORM.Entities.Category { Name = "New category"},
                FinishDateTime = DateTime.Now,
                StartDateTime = DateTime.Now,
                Sold = false,
                StartPrice = 0,
                OwnerId = em.Users.First().Id,
                ModerationStatus = new ORM.Entities.ModerationStatus { Name = "First status" }
                
            });
            */
            int a = 1;
            a += a;
        }
    }
}
