#nullable disable
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VendorMachine.Api.Controllers;
using VendorMachine.ApiTests.Helpers;
using VendorMachine.Core.Services.Interfaces;
using Xunit;

namespace VendorMachine.ApiTests.Controllers
{
    public class RoleControllerTests
    {
        [Fact()]
        public async Task GetRoles_GetsAllRoles_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<IRoleService>();
            todoService.Setup(x => x.GetRoles()).ReturnsAsync(RoleMockData.GetRoles());
            var sut = new RoleController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetRoles();


            // /// Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact()]
        public async Task GetRole_GetsSingleRoleById_ShouldReturn200Status()
        {
            /// Arrange
            string roleId = "efdb929d-2998-443a-b15c-e27b9715b09f";
            var todoService = new Mock<IRoleService>();
            todoService.Setup(x => x.GetRole(roleId)).ReturnsAsync(RoleMockData.GetRole(roleId));
            var sut = new RoleController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetRole(roleId);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact()]
        public async Task AddRole_AddsNewRole_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<IRoleService>();
            todoService.Setup(x => x.AddRole(RoleMockData._role)).ReturnsAsync(RoleMockData.AddRole(RoleMockData._role));
            var sut = new RoleController(todoService.Object);

            /// Act
            var result = (ObjectResult)await sut.PostRole(RoleMockData._role);


            // /// Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should().NotBeNull();

        }
        [Fact()]
        public async Task UpdateRole_UpdatesRole_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<IRoleService>();
            RoleMockData._role.RoleName = "UpdatedTest";
            todoService.Setup(x => x.UpdateRole(RoleMockData._role.RoleId, RoleMockData._role)).ReturnsAsync(RoleMockData.UpdateRole(RoleMockData._role.RoleId, RoleMockData._role));
            var sut = new RoleController(todoService.Object);

            /// Act
            var result = (NoContentResult)await sut.PutRole(RoleMockData._role.RoleId, RoleMockData._role);


            // /// Assert
            result.StatusCode.Should().Be(204);
        }
        [Fact()]
        public async Task DeleteRole_DeleteRoleById_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<IRoleService>();
            todoService.Setup(x => x.DeleteRole(RoleMockData._role.RoleId)).ReturnsAsync(RoleMockData.DeleteRole(RoleMockData._role.RoleId));
            var sut = new RoleController(todoService.Object);

            /// Act
            var result = (NoContentResult)await sut.DeleteRole(RoleMockData._role.RoleId);


            // /// Assert
            result.StatusCode.Should().Be(204);
        }
    }
}