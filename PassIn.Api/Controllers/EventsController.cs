﻿using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public IActionResult Register([FromBody] RequestEventJson request)
  {
    var useCase = new RegisterEventUseCase( );

    var response = useCase.Execute(request);

    return Created(string.Empty, response);
  }

  [HttpGet]
  [Route("{eventId}")]
  [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult GetById([FromRoute] Guid eventId)
  {
    var useCase = new GetEventByIdUseCase( );

    var response = useCase.Execute(eventId);

    return Ok(response);
  }

}
