using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ORM;
using ORM.Entities;

namespace DbInitializers
{
    //DropCreateDatabaseAlways
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EntityModel>
    {
        protected override void Seed(EntityModel context)
        {
            
            context.Roles.Add(new Role
            {
                Name = "Administrator",
            });

            context.Roles.Add(new Role
            {
                Name = "Moderator",
            });

            var roleUser = new Role
            {
                Name = "User"
            };
            context.Roles.Add(roleUser);


            
            context.Users.Add(new User
            {
                Name = "admin",
                Email = "aaa@aaa.com",
                Password = "12345",
                RoleId = 1
            });

            context.Users.Add(new User
            {
                Name = "moderator",
                Email = "mmm@mmm.com",
                Password = "12345",
                RoleId = 2
            });

            context.Users.Add(new User
            {
                Name = "user",
                Email = "uuu@uuu.com",
                Password = "12345",
                RoleId = 3
            });


            
            context.Categories.Add(new Category
            {
                Name = "Телефоны"
            });

            context.Categories.Add(new Category
            {
                Name = "Автомобили"
            });



            context.ModerationStatuses.Add(new ModerationStatus 
            {
                Name = "Не проверен",
            });

            context.ModerationStatuses.Add(new ModerationStatus
            {
                Name = "Проверен",
            });

            context.ModerationStatuses.Add(new ModerationStatus
            {
                Name = "Нуждается в правке",
            });

            
            base.Seed(context);
        }

        public static void SetThisInitializer()
        {
            Database.SetInitializer(new DbInitializer());
            EntityModel em = new EntityModel();
            em.Database.Initialize(false);
        }
    }
}
