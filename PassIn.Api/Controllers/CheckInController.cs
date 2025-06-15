using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
  [HttpPost]
  [Route("{attendeeId}")]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status201Created)]
  public IActionResult ChekIn([FromRoute] Guid attendeeId)
  {
    var useCase = new CheckInsUseCase( );
    useCase.Execute(attendeeId);
    return Created( );
  }
}
