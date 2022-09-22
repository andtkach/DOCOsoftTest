using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DOCOsoft.UserManagement.Api;
using DOCOsoft.UserManagement.API.IntegrationTests.Base;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.CreateUser;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.UpdateUser;
using DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUserDetail;
using DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUsersList;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DOCOsoft.UserManagement.API.IntegrationTests.Controllers
{

    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task FindWellKnownUser()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<UserListVm>>(responseString);
            
            Assert.IsType<List<UserListVm>>(result);
            Assert.NotEmpty(result);

            var wellKnownUser = result.FirstOrDefault(u =>
                u.FirstName == "Well" && u.LastName == "Known" && u.Email == "user@email.com");
            Assert.True(wellKnownUser != null);
        }

        [Fact]
        public async Task CreateUser()
        {
            var client = _factory.GetAnonymousClient();

            var newUser = new CreateUserCommand
            {
                FirstName = "New",
                LastName = "User",
                Email = "create@a.a"
            };

            var jsonContent = JsonSerializer.Serialize(newUser);
            using var httpContent = new StringContent
                (jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/users", httpContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            
            string userId = responseString.Replace("\"", String.Empty);

            response = await client.GetAsync("/api/users/" + userId);

            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UserDetailVm>(responseString);

            Assert.IsType<UserDetailVm>(result);
            Assert.False(result == null);

            Assert.True(result.Email == "create@a.a");
        }

        [Fact]
        public async Task UpdateUser()
        {
            var client = _factory.GetAnonymousClient();

            var newUser = new CreateUserCommand
            {
                FirstName = "UserToUpdate",
                LastName = "Test",
                Email = "update@a.a"
            };

            var jsonContent = JsonSerializer.Serialize(newUser);
            using var httpContent = new StringContent
                (jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/users", httpContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            string userId = responseString.Replace("\"", String.Empty);

            var user = new UpdateUserCommand()
            {
                UserId = Guid.Parse(userId),
                FirstName = "Updated",
                LastName = "User",
                Email = "updated.user@email.com"
            };

            jsonContent = JsonSerializer.Serialize(user);
            using var httpUpdateContent = new StringContent
                (jsonContent, Encoding.UTF8, "application/json");

            response = await client.PutAsync("/api/users/", httpUpdateContent);

            response.EnsureSuccessStatusCode();

            response = await client.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<UserListVm>>(responseString);

            Assert.IsType<List<UserListVm>>(result);
            Assert.NotEmpty(result);

            var updatedUser = result.FirstOrDefault(u => u.Email == "updated.user@email.com");
            Assert.True(updatedUser != null);
        }

        [Fact]
        public async Task DeleteUser()
        {
            var client = _factory.GetAnonymousClient();

            var newUser = new CreateUserCommand
            {
                FirstName = "UserToDelete",
                LastName = "Test",
                Email = "delete@a.a"
            };

            var jsonContent = JsonSerializer.Serialize(newUser);
            using var httpContent = new StringContent
                (jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/users", httpContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            string userId = responseString.Replace("\"", String.Empty);

            response = await client.DeleteAsync("/api/users/" + userId);

            response.EnsureSuccessStatusCode();

            response = await client.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<UserListVm>>(responseString);

            Assert.IsType<List<UserListVm>>(result);
            Assert.NotEmpty(result);

            var updatedUser = result.FirstOrDefault(u => u.Email == "delete@a.a");
            Assert.True(updatedUser == null);
        }
    }
}
