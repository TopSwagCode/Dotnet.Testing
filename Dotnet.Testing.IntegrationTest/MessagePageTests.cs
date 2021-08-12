using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dotnet.Testing.Web.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using AngleSharp.Html.Dom;
using Dotnet.IntegrationTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.IntegrationTest
{
    public class MessagePageTests : IntegrationTestBase
    {
        public MessagePageTests(CustomWebApplicationFactory<Dotnet.Testing.Web.Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            var client = GetHttpClient();
            var defaultPage = await client.GetAsync("/message");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            var response = await client.SendAsync(
                (IHtmlFormElement) content.QuerySelector("form[id='messages']"),
                (IHtmlButtonElement) content.QuerySelector("button[id='deleteAllBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Message", response.Headers.Location.OriginalString);
        }

        [Fact]
        public async Task Post_DeleteMessageHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            var client = GetHttpClient();
            var defaultPage = await client.GetAsync("/message");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            var response = await client.SendAsync((IHtmlFormElement) content.QuerySelector("form[id='messages']")
                ,(IHtmlButtonElement) content.QuerySelector("form[id='messages']")
                    .QuerySelector("div[class='panel-body']")
                    .QuerySelector("button"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Message", response.Headers.Location.OriginalString);
            
            using var dbContext = GetDbContext();
            var messages = dbContext.Messages.ToList();
            Assert.Equal(2,messages.Count);
        }

        [Fact]
        public async Task Post_AddMessageHandler_ReturnsSuccess_WhenMissingMessageText()
        {
            // Arrange
            var client = GetHttpClient();
            var defaultPage = await client.GetAsync("/message");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            var messageText = string.Empty;

            // Act
            var response = await client.SendAsync(
                (IHtmlFormElement) content.QuerySelector("form[id='addMessage']"),
                (IHtmlButtonElement) content.QuerySelector("button[id='addMessageBtn']"),
                new Dictionary<string, string>
                {
                    ["Message.Text"] = messageText
                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            // A ModelState failure returns to Page (200-OK) and doesn't redirect.
            response.EnsureSuccessStatusCode();
            Assert.Null(response.Headers.Location?.OriginalString);

            using var dbContext = GetDbContext();
            var messages = dbContext.Messages.ToList();
            Assert.Equal(3,messages.Count);
        }

        [Fact]
        public async Task Post_AddMessageHandler_ReturnsSuccess_WhenMessageTextTooLong()
        {
            // Arrange
            var client = GetHttpClient();
            var defaultPage = await client.GetAsync("/message");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            var messageText = new string('X', 201);

            // Act
            var response = await client.SendAsync(
                (IHtmlFormElement) content.QuerySelector("form[id='addMessage']"),
                (IHtmlButtonElement) content.QuerySelector("button[id='addMessageBtn']"),
                new Dictionary<string, string>
                {
                    ["Message.Text"] = messageText
                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            // A ModelState failure returns to Page (200-OK) and doesn't redirect.
            response.EnsureSuccessStatusCode();
            Assert.Null(response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Post_AnalyzeMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            var client = GetHttpClient();
            var defaultPage = await client.GetAsync("/message");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            var response = await client.SendAsync(
                (IHtmlFormElement) content.QuerySelector("form[id='analyze']"),
                (IHtmlButtonElement) content.QuerySelector("button[id='analyzeBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Message", response.Headers.Location.OriginalString);
        }


    }


}