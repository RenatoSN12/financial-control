
using FinanceControl.Domain.Exceptions;

namespace FinanceControl.Domain.ValueObjects;

public class Password : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private Password(){}

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        if(string.IsNullOrEmpty(value))
            throw new InvalidPasswordException("A senha não pode ser vazia");

        if(value.Length < 8)
            throw new InvalidPasswordException("A senha deve conter no mínimo 8 caracteres");

        if(value.Length > 100)
            throw new InvalidPasswordException("A senha deve conter no máximo 100 caracteres");

        if(!value.Any(char.IsUpper))
            throw new InvalidPasswordException("A senha deve conter pelo menos uma letra maiúscula");

        if(!value.Any(char.IsLower))
            throw new InvalidPasswordException("A senha deve conter pelo menos uma letra minúscula");

        if(!value.Any(char.IsDigit))
            throw new InvalidPasswordException("A senha deve conter pelo menos um número");

        if(!value.Any(c => !char.IsLetterOrDigit(c)))
            throw new InvalidPasswordException("A senha deve conter pelo menos um caractere especial");

        return new Password(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}