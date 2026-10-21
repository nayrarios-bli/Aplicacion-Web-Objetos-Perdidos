using FluentValidation;
using System.Globalization;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Validators
{
    public class ActualizarDetalleobjetoDtoValidator : AbstractValidator<DetalleobjetoDto>
    {
        private static readonly string[] Formats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

        public ActualizarDetalleobjetoDtoValidator()
        {
            // Para actualización, el ID es obligatorio
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del detalle es obligatorio y debe ser mayor que cero.");

            // Fecha encontrada (obligatoria)
            RuleFor(x => x.FechaEncontrada)
                .NotEmpty().WithMessage("La fecha encontrada es obligatoria.")
                .Must(BeAValidDate).WithMessage("La fecha debe tener un formato válido (dd-MM-yyyy, dd/MM/yyyy o yyyy-MM-dd).");

            // Fecha recogida (opcional)
            RuleFor(x => x.FechaRecogida)
                .Must(BeAValidDate).When(x => !string.IsNullOrWhiteSpace(x.FechaRecogida))
                .WithMessage("La fecha de recogida debe tener un formato válido (dd-MM-yyyy, dd/MM/yyyy o yyyy-MM-dd).");

            RuleFor(x => x.LugarEncontrado)
                .NotEmpty().WithMessage("El lugar donde se encontró es obligatorio.")
                .MaximumLength(70).WithMessage("El lugar encontrado no puede exceder los 100 caracteres.");

            RuleFor(x => x.LugarAlmacenado)
                .NotEmpty().WithMessage("El lugar donde se almacena es obligatorio.")
                .MaximumLength(75).WithMessage("El lugar almacenado no puede exceder los 100 caracteres.");
        }

        private bool BeAValidDate(string? date)
        {
            return DateOnly.TryParseExact(date?.Trim(), Formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}