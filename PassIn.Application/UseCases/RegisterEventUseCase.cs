
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases;
public class RegisterEventUseCase
{
    public ResponseRegisterEventJson Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var entity = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Slug = request.Title.ToLower().Replace(" ", "-"),
            Maximum_Attendees = request.MaximumAttendees
        };

        dbContext.Events.Add(entity);

        dbContext.SaveChanges();

        return new ResponseRegisterEventJson
        {
            Id = entity.Id,
        };
    }

    private void Validate(RequestEventJson request)
    {
        if (request.Title == null || request.Title.Length < 3)
        {
            throw new ErroOnValidationException("Title must be at least 3 characters long.");
        }

        if(request.MaximumAttendees < 1)
        {
            throw new ErroOnValidationException("Maximum attendees must be at least 1.");
        }

        if(string.IsNullOrEmpty(request.Details))
        {
            throw new ErroOnValidationException("Details cannot be empty.");
        }
    }
}
