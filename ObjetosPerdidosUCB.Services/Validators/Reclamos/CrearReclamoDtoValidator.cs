using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Validators.Reclamos
{
    public class CrearReclamoDtoValidator : AbstractValidator<CrearReclamoDto>
    {
        public CrearReclamoDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.IdObjeto)
                .GreaterThan(0u).WithMessage("El ID del objeto es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0u).WithMessage("El ID de usuario es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.Justificacion)
                .NotEmpty().WithMessage("La justificación es obligatoria.")
                .MinimumLength(10).WithMessage("Describe con más detalle por qué el objeto es tuyo (mínimo 10 caracteres).")
                .MaximumLength(1000).WithMessage("La justificación no puede exceder los 1000 caracteres.");
        }
    }
}
