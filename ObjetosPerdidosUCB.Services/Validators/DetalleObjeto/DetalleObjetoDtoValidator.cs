using FluentValidation;
using System.Globalization;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Validators
{
    public class DetalleobjetoDtoValidator : AbstractValidator<DetalleobjetoDto>
    {
        private static readonly string[] Formats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

        public DetalleobjetoDtoValidator()
        {
            // Validación para el ID (solo para actualizaciones)
            RuleFor(x => x.Id)
                .GreaterThan(0).When(x => x.Id > 0)
                .WithMessage("El ID del detalle debe ser mayor que cero.");

            // Fecha encontrada
            RuleFor(x => x.FechaEncontrada)
                .NotEmpty().WithMessage("La fecha encontrada es obligatoria.")
                .Must(BeAValidDate).WithMessage("La fecha debe tener un formato válido (dd-MM-yyyy, dd/MM/yyyy o yyyy-MM-dd).")
                .Must(BeNotFutureDate).WithMessage("La fecha encontrada no puede ser futura.");

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

        // Métodos de validación personalizados
        private bool BeAValidDate(string? date)
        {
            return DateOnly.TryParseExact(date?.Trim(), Formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool BeNotFutureDate(string date)
        {
            if (DateOnly.TryParseExact(date?.Trim(), Formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return parsedDate <= DateOnly.FromDateTime(DateTime.Today);
            }
            return true; // Si no es válida, la otra regla se encarga
        }
    }
}