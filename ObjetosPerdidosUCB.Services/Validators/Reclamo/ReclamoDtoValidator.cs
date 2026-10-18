using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Validators.Reclamo
{
    public class ReclamoDtoValidator : AbstractValidator<ReclamoDto>
    {
        public ReclamoDtoValidator()
        {
            // Validación para el ID (solo para actualizaciones)
            RuleFor(x => x.Id)
                .GreaterThan(0).When(x => x.Id > 0) // Solo valida si el ID es mayor que 0 (para updates)
                .WithMessage("El ID del reclamo debe ser mayor que cero.");

            // Validación para IdUsuario
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El ID de usuario es obligatorio y debe ser mayor que cero.")
                .NotEmpty().WithMessage("El ID de usuario no puede estar vacío.");

            // Validación para IdObjeto
            RuleFor(x => x.IdObjeto)
                .GreaterThan(0).WithMessage("El ID de usuario es obligatorio y debe ser mayor que cero.")
                .NotEmpty().WithMessage("El ID de usuario no puede estar vacío.");

            // Validación para FechaEmitida
            RuleFor(x => x.FechaEmitida)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .Must(BeAValidDate).WithMessage("La fecha debe tener un formato válido (yyyy-MM-dd o similar).")
                .Must(BeNotFutureDate).WithMessage("La fecha no puede ser futura.");

            // Validación para FechaRevisada
            RuleFor(x => x.FechaRevisada)
                .Must(BeAValidDate).WithMessage("La fecha debe tener un formato válido (yyyy-MM-dd o similar).")
                .Must(BeNotFutureDate).WithMessage("La fecha no puede ser futura.");
        }
        // Métodos de validación personalizados
        private bool BeAValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }

        private bool BeNotFutureDate(string date)
        {
            if (DateTime.TryParse(date, out var parsedDate))
            {
                return parsedDate <= DateTime.Now;
            }
            return true; // Si no es una fecha válida, la otra validación se encargará
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
