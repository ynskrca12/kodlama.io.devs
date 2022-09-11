using Application.Features.ProgrammingLanguages.Commands.CreatePL;
using Application.Features.ProgrammingLanguages.Commands.DeletePL;
using Application.Features.ProgrammingLanguages.Commands.UpdatePL;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Teknologies.Commands.CreateTeknology;
using Application.Features.Teknologies.Commands.DeleteTeknology;
using Application.Features.Teknologies.Commands.UpdateTeknology;
using Application.Features.Teknologies.Dtos;
using Application.Features.Teknologies.Models;
using Application.Features.Teknologies.Queries.GetListTeknology;
using Application.Features.Teknologies.Queries.GetListTeknologyByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeknologiesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTeknologyQuery getListTeknologyQuery = new GetListTeknologyQuery { PageRequest = pageRequest};
            TeknologyListModel result = await Mediator.Send(getListTeknologyQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListTeknologyByDynamicQuery getListByDynamicTeknologyQuery = new GetListTeknologyByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
            TeknologyListModel result = await Mediator.Send(getListByDynamicTeknologyQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTeknologyCommand createTeknologyCommand)
        {
            CreatedTeknologyDto result = await Mediator.Send(createTeknologyCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTeknologyCommand deleteTeknologyCommand)
        {
            DeletedTeknologyDto result = await Mediator.Send(deleteTeknologyCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTeknologyCommand updateTeknologyCommand)
        {
            UpdatedTeknologyDto result = await Mediator.Send(updateTeknologyCommand);
            return Created("", result);
        }
    }
}
