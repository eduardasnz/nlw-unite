using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttendeeController : ControllerBase
{
  [HttpPost]
  [Route("{eventId}")]
  [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
  public IActionResult RegisterPerson([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
  {
    var useCase = new RegisterAttendeeUseCaseUseCase( );
    var response = useCase.Execute(eventId, request);
    return Created(string.Empty, response);
  }

  [HttpGet]
  [Route("{eventId}")]
  [ProducesResponseType(typeof(ResponseAllAttendeesjson), StatusCodes.Status200OK)]
  public IActionResult GetAll([FromRoute] Guid eventId)
  {
    var useCase = new GetAllAttendeesByEventIdUseCase( );
    var response = useCase.Execute(eventId);
    return Ok(response);
  }
}
