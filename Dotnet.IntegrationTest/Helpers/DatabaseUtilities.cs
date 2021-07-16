using System.Collections.Generic;
using Dotnet.Testing.Web.Data;
using Dotnet.Testing.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.IntegrationTest.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Messages.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Messages.RemoveRange(db.Messages);
            InitializeDbForTests(db);
        }

        private static List<Message> GetSeedingMessages()
        {
            return new List<Message>()
            {
                new Message(){ Text = "TEST RECORD: You're standing on my scarf." },
                new Message(){ Text = "TEST RECORD: Would you like a jelly baby?" },
                new Message(){ Text = "TEST RECORD: To the rational mind, " +
                                      "nothing is inexplicable; only unexplained." }
            };
        }

        public static void RunDbMigrationsForTests(ApplicationDbContext db)
        {
            db.Database.Migrate();
        }
    }
}