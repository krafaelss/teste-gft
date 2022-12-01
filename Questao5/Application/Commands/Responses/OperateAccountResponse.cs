using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.Responses
{

    public class OperateAccountResponse
    {
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string OperationId { get; set; }

        [JsonIgnore]
        public bool Succeeded => ErrorType == null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorType { get;protected set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; protected set; }


        public static OperateAccountResponse Success(string value) => new OperateAccountResponse { OperationId = value };

        public static OperateAccountResponse Failed(string errorType, string message) => new OperateAccountResponse { ErrorType = errorType, Message = message};



    }
}