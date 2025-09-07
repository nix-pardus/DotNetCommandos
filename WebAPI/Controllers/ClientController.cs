using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    // POST api/<ClientController>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ClientDto client)
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
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteById([FromBody] Guid id)
        {
            try
            {
                await _clientService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: сделать отдельный Exception, пока - так
                return BadRequest(ex);
            }
            
        }
    }
}
