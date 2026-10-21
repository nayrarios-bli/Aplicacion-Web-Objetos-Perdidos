using FluentValidation.Results;

namespace ObjetosPerdidosUCB.Api.Controllers.Response
{
    public static class ValidationResponse
    {
        /// <summary>Cuerpo estándar de los 400 por validación (mismo formato que en clase).</summary>
        public static object From(ValidationResult result) => new
        {
            message = "Error de validación",
            errors = result.Errors.Select(e => new
            {
                field = e.PropertyName,
                error = e.ErrorMessage
            })
        };
    }
}
