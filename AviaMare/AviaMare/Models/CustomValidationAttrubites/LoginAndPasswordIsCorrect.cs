using AviaMare.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AviaMare.Models.CustomValidationAttrubites
{
    public class LoginAndPasswordIsCorrect : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value, 
            ValidationContext validationContext)
        {
            var login = value as string;
            var password = value as string;

            if (login == null || password == null)
            {
                return new ValidationResult("Поля не должны быть пустыми");
            }

            var repository = validationContext.GetRequiredService<IUserRepositryReal>();
            if (repository.IsLoginAndPasswordIsCorrect(login, password))
            {
                return new ValidationResult("Неверный логин или пароль");
            }

            return ValidationResult.Success;
        }
    }
}
