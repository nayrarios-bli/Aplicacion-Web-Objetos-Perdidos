using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Validators
{
    public class CrearObjetoDtoValidator : AbstractValidator<ObjetoDto>
    {
        public CrearObjetoDtoValidator()
        {
            // Para creación, el ID no debería enviarse o debería ser 0
            RuleFor(x => x.Id)
                .Equal(0u).When(x => x.Id != 0)
                .WithMessage("El ID debe ser 0 o no enviarse para la creación de un nuevo objeto.");

            RuleFor(x => x.IddetalleObjeto)
                .GreaterThan(0).WithMessage("El ID del detalle del objeto es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código es obligatorio.")
                .MaximumLength(30).WithMessage("El código no puede exceder los 20 caracteres.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MinimumLength(5).WithMessage("La descripción debe tener al menos 5 caracteres.")
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0u).WithMessage("El ID de usuario es obligatorio y debe ser mayor que cero.");

            // Imagen (opcional)
            RuleFor(x => x.Imagen)
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Imagen))
                .WithMessage("La URL de la imagen no es válida. Debe comenzar con http:// o https://");
        }

        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
                return true;

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}