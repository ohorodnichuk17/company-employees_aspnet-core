namespace Entities.Responses;

public abstract class ApiBadRequestResponse : ApiNotFoundResponse
{
    public string Message { get; set; }
    
    public ApiBadRequestResponse(Guid id) 
        : base($"Company with id: {id} is not found in db.")
    {
    }
}