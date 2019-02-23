
namespace Imposto.Infra.CrossCutting.Util
{
    /// <summary>
    /// Classe customizada para o retorno de erros para a camada de apresentação.
    /// </summary>
    public class CustomValidationResult
    {
        public CustomValidationResult()
        {
        }

        public CustomValidationResult(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
