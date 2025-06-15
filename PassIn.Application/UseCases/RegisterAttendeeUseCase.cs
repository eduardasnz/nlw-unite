using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Net.Mail;

namespace PassIn.Application.UseCases;
public class RegisterAttendeeUseCaseUseCase
{
  private readonly PassInDbContext _dbContext;
  public RegisterAttendeeUseCaseUseCase()
  {
    _dbContext = new PassInDbContext( );
  }

  public ResponseAttendeeJson Execute(Guid eventId, RequestRegisterEventJson request)
  {
    Validate(eventId, request);

    var entity = new Attendee
    {
      Name = request.Name,
      Email = request.Email,
      Event_Id = eventId
    };

    _dbContext.Attendees.Add(entity);
    _dbContext.SaveChanges( );

    return new ResponseAttendeeJson
    {
      Id = entity.Id,
      Name = request.Name,
      Email = request.Email,
      CheckedInAt = DateTime.UtcNow
    };
  }

  private void Validate(Guid eventId, RequestRegisterEventJson request)
  {
    var ifEventExist = _dbContext.Events.Find(eventId);
    if (ifEventExist == null)
      throw new ErroOnValidationException("Event does not exist.");

    if (string.IsNullOrEmpty(request.Name) || request.Name.Length < 3)
    {
      throw new ErroOnValidationException("Name must be at least 3 characters long.");
    }

    if (IsValidEmail(request.Email) == false)
    {
      throw new ErroOnValidationException("Email is invalid.");
    }

    var ifAttendeeAlreadyExist = _dbContext.Attendees.Any(a => a.Email == request.Email && a.Event_Id == eventId);

    if (ifAttendeeAlreadyExist)
    {
      throw new ErroOnValidationException("Attendee with this email already registered for this event.");
    }

    var attendeesForEvent = _dbContext.Attendees.Count(a => a.Event_Id == eventId);
    if (attendeesForEvent > ifEventExist.Maximum_Attendees)
    {
      throw new ConflitException("there is no room for this event");
    }
  }

  private bool IsValidEmail(string email)
  {
    try
    {
      new MailAddress(email);

      return true;
    }
    catch
    {
      return false;
    }
  }
}
