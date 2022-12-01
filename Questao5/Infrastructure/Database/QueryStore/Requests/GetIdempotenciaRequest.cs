namespace Questao5.Infrastructure.Database.QueryStore.Requests;

public class GetIdempotenciaRequest
{
    public string Id { get; set; }

    public GetIdempotenciaRequest(string id)
    {
        Id = id;
    }
    
}