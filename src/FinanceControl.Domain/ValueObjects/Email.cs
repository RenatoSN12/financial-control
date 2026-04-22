using System.Net.Mail;
using FinanceControl.Domain.Exceptions;

namespace FinanceControl.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; } = string.Empty;

    private Email(){}

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string address)
    {
        if(string.IsNullOrEmpty(address))
            throw new InvalidEmailException("O e-mail não pode ser vazio");

        address = address.Trim().ToLowerInvariant();

        if(address.Length > 255)
            throw new InvalidEmailException("O e-mail não pode ter mais de 255 caracteres");

        if(!IsValid(address))
            throw new InvalidEmailException("O e-mail não está em um formato válido");

        return new Email(address);
    }

    private static bool IsValid(string address)
    {
        try
        {
            var addr = new MailAddress(address);
            return addr.Address == address;
        }
        catch
        {
            return false;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}