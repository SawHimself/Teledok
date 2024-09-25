using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases;

namespace TeledokWebAPI.Controllers
{
    [Route("api/client")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public class ClientAPI : ControllerBase
    {
        private readonly IClientLogic _clientLogic;

        public ClientAPI(IClientLogic clientLogic)
        {
            _clientLogic = clientLogic;
        }

        [HttpGet("get")]
        public IQueryable<Client> GetSoftFilterPage(int position, int pageSize, string? name)
        {
            return _clientLogic.GetClients(position, pageSize, name);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Client>> AddNewClient(Client client)
        {
            Client? newclient = await _clientLogic.Create(client);
            return Ok(client);
        }

        [HttpPut("change_name")]
        public async Task<ActionResult<Client>> ChangeNameClient(string TIN, string name)
        {
            Client? updateClient = await _clientLogic.UpdateName(TIN, name);
            return Ok(updateClient);
        }

        [HttpPut("change_TIN")]
        public async Task<ActionResult<Client>> ChangeTINClient(string oldTIN, string newTIN, ClientType clietnType)
        {
            Client? updateClient = await _clientLogic.UpdateTIN(oldTIN, newTIN, clietnType);
            return Ok(updateClient);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Client>> RemoveClient(string ClientTIN)
        {

            Client? newclient = await _clientLogic.Remove(ClientTIN);
            return Ok(ClientTIN);
        }
    }
}
