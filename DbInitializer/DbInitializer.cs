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
            var roleAdmin = new Role
            {
                Name = "Administrator",
            };
            context.Roles.Add(roleAdmin);

            var roleModerator = new Role
            {
                Name = "Moderator",
            };
            context.Roles.Add(roleModerator);

            var roleUser = new Role
            {
                Name = "User"
            };
            context.Roles.Add(roleUser);
            //context.SaveChanges();

            context.Users.Add(new User
            {
                Name = "admin",
                Email = "admin@admin.com",
                Password = "12345",
            }).Roles.Add(roleAdmin);

            context.Users.Add(new User
            {
                Name = "moderator",
                Email = "mod@mod.com",
                Password = "12345",
            }).Roles.Add(roleModerator);

            context.Users.Add(new User
            {
                Name = "user1",
                Email = "user1@user1.com",
                Password = "12345",
            }).Roles.Add(roleUser);

            context.Users.Add(new User
            {
                Name = "user2",
                Email = "user2@user2.com",
                Password = "12345",
            }).Roles.Add(roleUser);

            context.Users.Add(new User
            {
                Name = "user3",
                Email = "user3@user3.com",
                Password = "12345",
            }).Roles.Add(roleUser);
            
            
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
            context.SaveChanges();


            var random = new Random((int)DateTime.Now.Ticks);
            
            context.Lots.Add(new Lot
            {
                CategoryId = 1,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "New mobile phone with 2GB RAM",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Sumsung Galaxy 6",
                OwnerId = 3,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 112
            });
            
            context.Lots.Add(new Lot
            {
                CategoryId = 1,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "Simple mobile phone",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Nokia 3310",
                OwnerId = 4,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 30
            });

            context.Lots.Add(new Lot
            {
                CategoryId = 1,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "Smartphone with Adnroid 4.0",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Sony Xperia L",
                OwnerId = 5,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 102
            });

            context.Lots.Add(new Lot
            {
                CategoryId = 2,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "4 Doors - 5 Seats - Car & document will be delivered directly upon payment completion - A/C - ABS - Air Bags - Central Lock - GCC Specifications - Power Steering - Power Windows - Petrol - Power Mirrors ",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Toyota Corolla 2012",
                OwnerId = 3,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 18000
            });

            context.Lots.Add(new Lot
            {
                CategoryId = 2,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "4 Doors - 7 Seats - This car to be sold in it's current condition at it's location ",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Kia Carnival 2003",
                OwnerId = 4,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 5500
            });

            context.Lots.Add(new Lot
            {
                CategoryId = 2,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "4 Doors - 5 Seats - This car to be sold in it's current condition at it's location ",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Nissan Patrol 1997",
                OwnerId = 5,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 4300
            });

            context.Lots.Add(new Lot
            {
                CategoryId = 2,
                FinishDateTime = DateTime.Now.AddDays(15 + random.Next(10)),
                Description = "4 Doors - 5 Seats - This car to be sold in it's current condition at it's location ",
                ModeratorId = 2,
                ModerationDateTime = DateTime.Now,
                ModerationStatusId = 1,
                Name = "Mitsubishi D/C 1.5T 2007",
                OwnerId = 5,
                Sold = false,
                StartDateTime = DateTime.Now,
                StartPrice = 8100
            });
            
            base.Seed(context);
        }

        public static void SetThisInitializer()
        {
            Database.SetInitializer(new DbInitializer());
            EntityModel em = new EntityModel("EntityModel");
            em.Database.Initialize(true);
        }
    }
}
