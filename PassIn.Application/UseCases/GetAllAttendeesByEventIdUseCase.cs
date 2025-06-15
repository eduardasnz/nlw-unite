using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases;
public class GetAllAttendeesByEventIdUseCase
{
  private readonly PassInDbContext _dbContext;

  public GetAllAttendeesByEventIdUseCase()
  {
    _dbContext = new PassInDbContext( );
  }

  public ResponseAllAttendeesjson Execute(Guid eventId)
  {
    var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(at => at.CheckIn).FirstOrDefault(ev => ev.Id == eventId);
    if (entity == null)
      throw new NotFoundException("an event with this id does not exist");

    return new ResponseAllAttendeesjson
    {
      Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
      {
        Id = attendee.Id,
        Name = attendee.Name,
        Email = attendee.Email,
        CreatedAt = attendee.Created_At,
        CheckedInAt = attendee.CheckIn?.Created_at
      }).ToList( ),
    };
  }
}
