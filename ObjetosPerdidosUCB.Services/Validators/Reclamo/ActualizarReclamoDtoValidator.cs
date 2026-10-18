using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Validators.Reclamo
{
    public class ActualizarReclamoDtoValidator : AbstractValidator<ReclamoDto>
    {
        public ActualizarReclamoDtoValidator()
        {
            // Para actualización, el ID es obligatorio
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del reclamo es obligatorio y debe ser mayor que cero.");


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
                
            // Validación para FechaRevisada
            RuleFor(x => x.FechaRevisada)
                .Must(BeAValidDate).WithMessage("La fecha debe tener un formato válido (yyyy-MM-dd o similar).")
        }

        private bool BeAValidDate(string date) => DateTime.TryParse(date, out _);

        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
                return true;

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
