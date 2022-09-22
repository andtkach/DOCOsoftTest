using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.CreateUser;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.DeleteUser;
using DOCOsoft.UserManagement.Application.Features.Users.Commands.UpdateUser;
using DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUserDetail;
using DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUsersList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOCOsoft.UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<UserListVm>>> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetUsersListQuery()));
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserDetailVm>> GetUserById(Guid id)
        {
            return Ok(await _mediator.Send(new GetUserDetailQuery() { Id = id }));
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserCommand createUserCommand)
        {
            return Ok(await _mediator.Send(createUserCommand));
        }

        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand() { UserId = id });
            return NoContent();
        }
    }
}
