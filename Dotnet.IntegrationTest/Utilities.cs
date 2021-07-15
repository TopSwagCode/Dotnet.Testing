using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Dotnet.Testing.Web.Data;
using Dotnet.Testing.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.IntegrationTest
{
    public class HtmlHelpers
    {
        public static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var document = await BrowsingContext.New()
                .OpenAsync(ResponseFactory, CancellationToken.None);
            return (IHtmlDocument)document;

            void ResponseFactory(VirtualResponse htmlResponse)
            {
                htmlResponse
                    .Address(response.RequestMessage.RequestUri)
                    .Status(response.StatusCode);

                MapHeaders(response.Headers);
                MapHeaders(response.Content.Headers);

                htmlResponse.Content(content);

                void MapHeaders(HttpHeaders headers)
                {
                    foreach (var header in headers)
                    {
                        foreach (var value in header.Value)
                        {
                            htmlResponse.Header(header.Key, value);
                        }
                    }
                }
            }
        }
    }
    
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

        public static List<Message> GetSeedingMessages()
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