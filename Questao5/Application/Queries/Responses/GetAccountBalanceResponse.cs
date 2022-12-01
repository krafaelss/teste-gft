using System.Text.Json.Serialization;

namespace Questao5.Application.Queries.Responses;

public class GetAccountBalanceResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int AccountNumber { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string AccountOwner { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string QueryDate { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? AccountBalance { get; set; }
    

    [JsonIgnore]
    public bool Succeeded => ErrorType == null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ErrorType { get; protected set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; protected set; }


    public static GetAccountBalanceResponse Success(int accountNumber, string accountOwner, string queryDate, double accountBalance) 
        => new(){AccountNumber = accountNumber, AccountOwner = accountOwner, QueryDate = queryDate, AccountBalance = accountBalance};

    public static GetAccountBalanceResponse Failed(string errorType, string message) => new GetAccountBalanceResponse { ErrorType = errorType, Message = message };
}