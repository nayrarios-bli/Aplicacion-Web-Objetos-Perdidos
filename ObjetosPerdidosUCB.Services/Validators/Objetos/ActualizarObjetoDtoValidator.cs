using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Validators.Objetos
{
    public class ActualizarObjetoValidator : AbstractValidator<ActualizarObjetoDto>
    {
        public ActualizarObjetoValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código es obligatorio.")
                .MaximumLength(30).WithMessage("El código no puede exceder los 30 caracteres.")
                .Matches(@"^OBJ-\d{3,}$").WithMessage("El código debe tener el formato OBJ-001.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.IdCategoria)
                .GreaterThan(0u).WithMessage("La categoría es obligatoria.");

            RuleFor(x => x.IdLugarAlmacenamiento)
                .GreaterThan(0u).WithMessage("El lugar de almacenamiento es obligatorio.");

        }
    }
}
