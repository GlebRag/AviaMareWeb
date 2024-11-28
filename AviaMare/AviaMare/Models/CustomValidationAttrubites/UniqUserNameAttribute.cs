using AviaMare.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AviaMare.Models.CustomValidationAttrubites
{
    public class UniqUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value, 
            ValidationContext validationContext)
        {
            var name = value as string;
            if (name == null)
            {
                return new ValidationResult("Поля не должны быть пустыми");
            }

            var repository = validationContext.GetRequiredService<IUserRepositryReal>();
            if (!repository.IsLoginUniq(name))
            {
                return new ValidationResult("Такое имя пользователя уже существует");
            }

            return ValidationResult.Success;
        }
    }
}
