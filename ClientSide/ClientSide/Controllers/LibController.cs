using Broker.Requests;
using Broker.Responses;
using ClientSide.Commands.Libraries;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Commands.Libraries;
using System.Net;

namespace ClientSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<CreateLibraryResponse> Create(
            [FromServices] ICreateLibraryCommand command,
            [FromBody] CreateLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.Created
                : (int)HttpStatusCode.BadRequest;

            return response;
        }

        [HttpPut]
        public async Task<UpdateLibraryResponse> Update(
            [FromServices] IUpdateLibraryCommand command,
            [FromBody] UpdateLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.BadRequest;

            return response;
        }

        [HttpGet]
        public async Task<ReadLibrariesResponse> Read(
            [FromServices] IReadLibraryCommand command,
            [FromBody] ReadLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = !(response.Addresses.Count == 0)
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.NoContent;

            return response;
        }

        [HttpDelete]
        public async Task<DeleteLibraryResponse> Delete(
            [FromServices] IDeleteLibraryCommand command,
            [FromBody] DeleteLibraryRequest request)
        {
            var response = await command.Execute(request);

            HttpContext.Response.StatusCode = response.IsSuccess
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.BadRequest;

            return response;
        }
    }
}
