namespace Questao5.Infrastructure.Database.CommandStore.Requests;

public class AddIdempotenciaRequest
{
    public string Id { get; set; }
    public string Request { get; set; }
    public string Resultado { get; set; }

    public AddIdempotenciaRequest(string id, string request, string resultado)
    {
        Id = id;
        Request = request;
        Resultado = resultado;
    }
}