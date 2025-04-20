using FluentValidation;
using TestApiSep.DTOs;

namespace TestApiSep.Validators
{
    public class RegistroInsertValidator : AbstractValidator<RegistroInsertDto>
    {
        public RegistroInsertValidator()
        {
            // Validación para Compañía
            RuleFor(registro => registro.Compania)
                .NotEmpty().WithMessage("Nombre de compañía es requerido")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Nombre de compañía contiene caracteres inválidos");

            // Validación para Cédula
            RuleFor(registro => registro.Cedula)
                .NotEmpty().WithMessage("Cédula es requerida")
                .Must(cedula => cedula.All(char.IsDigit)).WithMessage("Cédula inválida, solo números permitidos");

            // Validación para Nombre
            RuleFor(registro => registro.Nombre)
                .NotEmpty().WithMessage("Nombre es requerido")
                .Must(nombre => nombre.Replace(" ", "").All(char.IsLetter))
                .WithMessage("Nombre inválido, solo letras permitidas");

            // Validación para Título
            RuleFor(registro => registro.Titulo)
                .NotEmpty().WithMessage("Título es requerido")
                .Must(titulo => titulo.Replace(" ", "").All(char.IsLetter))
                .WithMessage("Titulo inválido, solo letras permitidas");

            // Validación para Email
            RuleFor(registro => registro.Email)
                .NotEmpty().WithMessage("Correo electrónico es requerido")
                .EmailAddress().WithMessage("Correo electrónico inválido");

            // Validación para Teléfono
            RuleFor(registro => registro.Telefono)
                .NotEmpty().WithMessage("Teléfono es requerido")
                .Length(10).WithMessage("El teléfono debe tener exactamente 10 dígitos")
                .Must(telefono => telefono.All(char.IsDigit)).WithMessage("Teléfono inválido, solo números permitidos");

            // Validación para Términos y condiciones
            RuleFor(registro => registro.Terminos)
                .Equal(true).WithMessage("Debe aceptar los términos y condiciones");
        }
    }
}
