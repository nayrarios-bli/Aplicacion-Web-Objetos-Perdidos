using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Validators.Reclamo
{
    public class CrearReclamoDtoValidator : AbstractValidator<ReclamoDto>
    {
        public CrearReclamoDtoValidator()
        {
            // Para creación, el ID no debería enviarse o debería ser 0
            RuleFor(x => x.Id)
                .Equal(0).When(x => x.Id != 0)
                .WithMessage("El ID debe ser 0 o no enviarse para la creación de un nuevo reclamo.");

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

        private bool BeAValidDate(string date) => DateTime.TryParse(date, out _);

        private bool BeNotFutureDate(string date)
        {
            if (DateTime.TryParse(date, out var parsedDate))
                return parsedDate <= DateTime.Now;
            return true;
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
