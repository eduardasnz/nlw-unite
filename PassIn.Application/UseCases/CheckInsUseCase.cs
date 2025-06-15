using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases;
public class CheckInsUseCase
{
  private readonly PassInDbContext _dbContext;
  public CheckInsUseCase()
  {
    _dbContext = new PassInDbContext( );
  }
  public ResponseRegisterJson Execute(Guid attendeeId)
  {
    Validate(attendeeId);

    var entity = new CheckIn
    {
      Attendee_Id = attendeeId,
      Created_at = DateTime.UtcNow,
    };

    _dbContext.CheckIns.Add(entity);
    _dbContext.SaveChanges( );

    return new ResponseRegisterJson { Id = entity.Id };

  }

  private void Validate(Guid attendeeId)
  {
    var ifExistAttendee = _dbContext.Attendees.Any(at => at.Id == attendeeId);
    if (ifExistAttendee == false)
    {
      throw new NotFoundException("Attendee with this Id not found.");
    }

    var ifExistCheckIn = _dbContext.CheckIns.Any(c => c.Attendee_Id == attendeeId);
    if (ifExistCheckIn == true)
    {
      throw new ConflitException("Attendee can not do checking twice in the same event.");
    }
  }
}
