using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wepsys.DianaHr.Core.Entities;
using Wepsys.DianaHr.Core.Services.Contracts;

namespace Wepsys.DianaHr.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            ArgumentNullException.ThrowIfNull(userService, nameof(userService));
            _userService = userService;
        }
        
        // GET SINGLE CARD
        [HttpGet]
        public async Task<IActionResult> RetrieveAsync()   
        {
           ISet<User> users = await _userService.RetrieveAsync();
            return Ok(users);


        } 


        
        // GET: api/Inspection/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> RetrieveAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            ISet<User> user = await _userService.RetrieveByIdAsync(id);
            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> PersistAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            user.Id = Guid.NewGuid();
            await _userService.PersistAsync(user);
            return Created("", user);
        }

        // PUT: api/Inspection/5
        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            await _userService.UpdateAsync(user);
            return Created("", user);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            await _userService.DeleteAsync(id);
            return Ok();
        }

    }
}