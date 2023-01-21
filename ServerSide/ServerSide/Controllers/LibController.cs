//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using ClientSide.Commands.Libraries;
//using System.Net;
//using ServerSide.Broker.Requests;
//using ServerSide.Broker.Responses;

//namespace ServerSide.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LibController : ControllerBase
//    {
//        [HttpPost("create")]
//        public CreateLibraryResponse Post(
//            [FromBody] CreateLibraryRequest request)
//        {
//            CreateLibraryCommand command = new();

//            CreateLibraryResponse response = command.Execute(request);

//            HttpContext.Response.StatusCode = response.IsSuccess
//                ? (int)HttpStatusCode.Created
//                : (int)HttpStatusCode.BadRequest;

//            return response;
//        }

//        [HttpPut]
//        public UpdateLibraryResponse Put(
//            [FromBody] UpdateLibraryRequest request)
//        {
//            UpdateLibraryCommand command = new();

//            UpdateLibraryResponse response = command.Execute(request);

//            HttpContext.Response.StatusCode = response.IsSuccess
//                ? (int)HttpStatusCode.OK
//                : (int)HttpStatusCode.BadRequest;

//            return response;
//        }

//        [HttpGet]
//        public ReadLibrariesResponse Get()
//        {
//            ReadLibraryCommand command = new();

//            ReadLibrariesResponse response = command.Execute();

//            HttpContext.Response.StatusCode = !response.Addresses.IsNullOrEmpty()
//                ? (int)HttpStatusCode.OK
//                : (int)HttpStatusCode.NoContent;

//            return response;
//        }

//        [HttpDelete]
//        public DeleteLibraryResponse Delete(
//            [FromBody] DeleteLibraryRequest request)
//        {
//            DeleteLibraryCommand command = new();

//            DeleteLibraryResponse response = command.Execute(request);

//            HttpContext.Response.StatusCode = response.IsSuccess
//                ? (int)HttpStatusCode.OK
//                : (int)HttpStatusCode.BadRequest;

//            return response;
//        }
//    }
//}
