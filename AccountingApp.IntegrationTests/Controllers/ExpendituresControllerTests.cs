using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

using Microsoft.VisualStudio.TestPlatform.TestHost;
using Assert = Xunit.Assert;

namespace AccountingApp.Tests.IntegrationTests
{
    public class ExpendituresControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ExpendituresControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetExpenditures_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Expenditures");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetExpenditureDetails_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expenditureId = 1;

            // Act
            var response = await client.GetAsync($"/Expenditures/Details/{expenditureId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateExpenditure_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expenditure = new Expenditure
            {
                Title = "Test Expenditure",
                Amount = 100,
                CreatedTime = DateTime.Now,
                ExpenditureType = ExpenditureType.食
            };
            var json = JsonConvert.SerializeObject(expenditure);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/Expenditures/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task EditExpenditure_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expenditureId = 1;
            var expenditure = new Expenditure
            {
                Id = expenditureId,
                Title = "Updated Expenditure",
                Amount = 200,
                CreatedTime = DateTime.Now,
                ExpenditureType = ExpenditureType.食
            };
            var json = JsonConvert.SerializeObject(expenditure);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync($"/Expenditures/Edit/{expenditureId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task DeleteExpenditure_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expenditureId = 1;

            // Act
            var response = await client.PostAsync($"/Expenditures/DeleteConfirmed/{expenditureId}", null);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
