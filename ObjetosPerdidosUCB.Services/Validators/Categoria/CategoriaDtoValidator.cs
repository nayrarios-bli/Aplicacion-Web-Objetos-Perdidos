using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Validators.Categorias
{
    public class CategoriaDtoValidator : AbstractValidator<CategoriaDto>
    {
        private static readonly string[] ExtensionesImagen = { ".jpg", ".jpeg", ".png", ".webp" };

        public CategoriaDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
                .GreaterThan(0u).WithMessage("El Id de la categoria es obligatoria y debe ser mayor que cero.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MinimumLength(5).WithMessage("La descripción debe tener al menos 5 caracteres.")
                .MaximumLength(1000).WithMessage("La descripción no puede exceder los 1000 caracteres.");

        }

        private static bool BeAValidImagePath(string ruta)
        {
            if (ruta.Contains("..")) return false;
            var ext = Path.GetExtension(ruta);
            return ExtensionesImagen.Contains(ext, StringComparer.OrdinalIgnoreCase);
        }

        private static bool BeNotFutureDate(string fecha) =>
            !FechaParser.TryParseDateOnly(fecha, out var f) || f <= DateOnly.FromDateTime(DateTime.Today);
    }
}