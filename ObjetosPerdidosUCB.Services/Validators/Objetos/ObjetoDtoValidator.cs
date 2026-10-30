using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Validators.Objetos
{
    public class ObjetoDtoValidator : AbstractValidator<ObjetoDto>
    {
        private static readonly string[] ExtensionesImagen = { ".jpg", ".jpeg", ".png", ".webp" };

        public ObjetoDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código es obligatorio.")
                .MaximumLength(30).WithMessage("El código no puede exceder los 30 caracteres.")
                .Matches(@"^OBJ-\d{3,}$").WithMessage("El código debe tener el formato OBJ-001.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MinimumLength(5).WithMessage("La descripción debe tener al menos 5 caracteres.")
                .MaximumLength(1000).WithMessage("La descripción no puede exceder los 1000 caracteres.");

            RuleFor(x => x.Imagen)
                .NotEmpty().WithMessage("La imagen es obligatoria.")
                .MaximumLength(255).WithMessage("La ruta de la imagen no puede exceder los 255 caracteres.")
                .Must(BeAValidImagePath).WithMessage("La imagen debe ser una ruta válida terminada en .jpg, .jpeg, .png o .webp.");

            RuleFor(x => x.LugarEncontrado)
                .NotEmpty().WithMessage("El lugar donde se encontró es obligatorio.")
                .MaximumLength(100).WithMessage("El lugar encontrado no puede exceder los 100 caracteres.");

            RuleFor(x => x.FechaEncontrada)
                .NotEmpty().WithMessage("La fecha en que se encontró es obligatoria.")
                .Must(f => FechaParser.TryParseDateOnly(f, out _))
                    .WithMessage("La fecha debe tener un formato válido (dd-MM-yyyy o yyyy-MM-dd).")
                .Must(BeNotFutureDate).WithMessage("La fecha no puede ser futura.");

            // Opcional: dato reservado al administrador para la entrega del objeto.
            RuleFor(x => x.FechaEntrega)
                .Must(f => FechaParser.TryParseDateOnly(f, out _))
                    .WithMessage("La fecha debe tener un formato válido (dd-MM-yyyy o yyyy-MM-dd).")
                .Must(BeNotFutureDate).WithMessage("La fecha no puede ser futura.")
                .When(x => !string.IsNullOrWhiteSpace(x.FechaEntrega));

            // Opcional: dato reservado al administrador para validar al dueño.
            RuleFor(x => x.DiasGuardados)
                .GreaterThan(0).When(x => x.DiasGuardados > 0) // Solo valida si los dias son mayores que 0
                .WithMessage("Los dias no pueden ser negativos.");

            RuleFor(x => x.IdCategoria)
                .GreaterThan(0u).WithMessage("La categoría es obligatoria.");

            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("La categoria a la que pertenece el objeto es obligatorio.")
                .MaximumLength(30).WithMessage("La categoria no puede exceder los 30 caracteres.");

            RuleFor(x => x.IdLugarAlmacenamiento)
                .GreaterThan(0u).WithMessage("El lugar de almacenamiento es obligatorio.");

            RuleFor(x => x.LugarAlmacenamiento)
                .NotEmpty().WithMessage("El lugar donde se almacena es obligatorio.")
                .MaximumLength(100).WithMessage("El lugar de almacenamiento no puede exceder los 100 caracteres.");


            RuleFor(x => x.IdUsuarioRegistro)
                .GreaterThan(0u).WithMessage("El ID del usuario que registra es obligatorio y debe ser mayor que cero.");
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