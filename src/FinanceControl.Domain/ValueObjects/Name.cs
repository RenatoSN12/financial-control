using System.Runtime.CompilerServices;
using FinanceControl.Domain.Exceptions;

namespace FinanceControl.Domain.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}".Trim();

    private Name(){}

    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Name Create(string firstName, string lastName)
    {
        var trimmedFirstName = firstName.Trim();
        var trimmedLastName = lastName.Trim();

        if(string.IsNullOrEmpty(trimmedFirstName))
            throw new InvalidTextException("O primeiro nome não pode ser vazio");

        if(trimmedFirstName.Length > 100)
            throw new InvalidTextException("O primeiro nome não pode ter mais de 100 caracteres");

        if(string.IsNullOrEmpty(trimmedLastName))
            throw new InvalidTextException("O sobrenome não pode ser vazio");

        if(trimmedLastName.Length > 150)
            throw new InvalidTextException("O sobrenome não pode ter mais de 150 caracteres");

        return new Name(trimmedFirstName, trimmedLastName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}