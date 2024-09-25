using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases;

namespace TeledokWebAPI.Controllers
{
    [Route("api/founder")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class FounderAPI : ControllerBase
    {
        private readonly IFounderLogic _founderLogic;
        private readonly IClientLogic _clientLogic;

        public FounderAPI(IFounderLogic founderLogic, IClientLogic clientLogic)
        {
            _founderLogic = founderLogic;
            _clientLogic = clientLogic;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Founder>> AddNewFounder(string clientTIN, Founder founder)
        {
            await _clientLogic.AddFounder(clientTIN, founder);
            return Ok(founder);
        }

        [HttpPut("change_name")]
        public async Task<ActionResult<Founder>> ChangeFullNameFounder(string founderTIN, string fullName)
        {
            Founder? updatefounder = await _founderLogic.UpdateFullName(founderTIN, fullName);
            if (updatefounder == null)
            {
                return BadRequest("Founder not found");
            }
            else
            {
                return Ok(updatefounder);
            }
        }

        [HttpPut("change_TIN")]
        public async Task<ActionResult<Founder>> ChangeTINFounder(string oldTIN, string newTIN)
        {
            Founder? updatefounder = await _founderLogic.UpdateTIN(oldTIN, newTIN);
            if(updatefounder == null) 
            {
                return BadRequest("Founder not found");
            }
            else
            {
                return Ok(updatefounder);
            }
        }


        [HttpDelete("delete")]
        public async Task<ActionResult<Founder>> RemoveFounder(string clientTIN, string founderTIN)
        {
            Client? client = await _clientLogic.DeleteFounder(clientTIN, founderTIN);
            if(client != null)
            {
                Founder? founder = (client.Founders.FirstOrDefault(f => f.TIN == founderTIN));
                if(founder != null) 
                { 
                    return Ok(founder);
                }
                else
                {
                    return BadRequest("Founder not found");
                }
            }
            else
            {
                return BadRequest("Client not found");
            }
        }
    }
}
