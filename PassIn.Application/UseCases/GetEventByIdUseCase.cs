using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases;
public class GetEventByIdUseCase
{
  private readonly PassInDbContext _dbContext;

  public GetEventByIdUseCase()
  {
    _dbContext = new PassInDbContext();
  }

  public ResponseEventJson Execute(Guid eventId)
  {
    var eventEntity = _dbContext.Events
        .FirstOrDefault(e => e.Id == eventId);

    if (eventEntity == null)
    {
      throw new NotFoundException("Event not found.");
    }

    return new ResponseEventJson
    {
      Id = eventEntity.Id,
      Title = eventEntity.Title,
      Details = eventEntity.Details,
      MaximumAttendees = eventEntity.Maximum_Attendees,
      AttendeesAmount = 0 // Assuming we don't have attendee count in the current context
    };
  }
}
