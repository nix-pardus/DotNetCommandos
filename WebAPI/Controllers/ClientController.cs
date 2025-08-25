using Application.Interfaces;
using Domain.DTO.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // POST api/<ClientController>
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] ClientDto client)
        {
            await _clientService.CreateAsync(client);
            return Ok();
        }
    }
}
