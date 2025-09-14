using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace XVerify.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _accountService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Account account)
        {
            var id = await _accountService.AddAsync(account);
            account.Id = id;
            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }

    }
}
