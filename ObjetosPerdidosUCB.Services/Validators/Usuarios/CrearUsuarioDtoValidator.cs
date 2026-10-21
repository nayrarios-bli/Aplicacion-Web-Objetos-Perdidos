using FluentValidation;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Validators.Usuarios
{
    public class CrearUsuarioDtoValidator : AbstractValidator<CrearUsuarioDto>
    {
        private static readonly string[] RolesValidos = { "Administrador", "Docente", "Estudiante", "Personal" };
        private const string DominioInstitucional = "@ucb.edu.bo";

        public CrearUsuarioDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

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

            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .MaximumLength(72).WithMessage("La contraseña no puede exceder los 72 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe incluir al menos una mayúscula.")
                .Matches("[a-z]").WithMessage("La contraseña debe incluir al menos una minúscula.")
                .Matches("[0-9]").WithMessage("La contraseña debe incluir al menos un número.");

            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .Must(r => RolesValidos.Contains(r))
                    .WithMessage("El rol debe ser: Administrador, Docente, Estudiante o Personal.");
        }
    }
}
