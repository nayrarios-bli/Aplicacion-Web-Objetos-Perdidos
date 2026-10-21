using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Validators.Usuarios
{
    public class ActualizarUsuarioDtoValidator : AbstractValidator<UsuarioDto>
    {
        private static readonly string[] RolesValidos = { "Administrador", "Docente", "Estudiante", "Personal" };
        private const string DominioInstitucional = "@ucb.edu.bo";

        public ActualizarUsuarioDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
                .GreaterThan(0u).WithMessage("El ID del usuario es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .MaximumLength(150).WithMessage("El correo no puede exceder los 150 caracteres.")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.")
                .Must(c => c.EndsWith(DominioInstitucional, StringComparison.OrdinalIgnoreCase))
                    .WithMessage($"Solo se permiten correos institucionales ({DominioInstitucional}).");

            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .Must(r => RolesValidos.Contains(r))
                    .WithMessage("El rol debe ser: Administrador, Docente, Estudiante o Personal.");
        }
    }
}
