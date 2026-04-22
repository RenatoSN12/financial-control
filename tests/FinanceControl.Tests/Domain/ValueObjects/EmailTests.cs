using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.ValueObjects;
using FluentAssertions;

namespace FinanceControl.Tests.Domain.ValueObjects;

[TestFixture]
public class EmailTests
{
    private string _email;
    private static IEnumerable<string> _invalidEmails()
    {
        yield return "";
        yield return " ";
        yield return "invalid-email";
        yield return "invalid-email@";
        yield return new string('a', 256);
        yield return "@example.com.br";
    }

    [SetUp]
    public void Setup()
    {
        _email = "renato@example.com";
    }

    [Test]
    public void Create_WithValidEmail_ShouldReturnEmailInstance()
    {
        var email = Email.Create(_email);

        email.Should().NotBeNull();
        email.Address.Should().Be(_email);
    }

    [Test]
    [TestCaseSource(nameof(_invalidEmails))]
    public void Create_WithInvalidEmail_ShouldThrowInvalidEmailException(string invalidEmail)
    {
        Action act = () => Email.Create(invalidEmail);
        act.Should().Throw<InvalidEmailException>();
    }

    [Test]
    public void Create_WithEmailWithSpaces_ShouldTrimSpaces()
    {
        var emailWithSpaces = "  " + _email + "  ";

        var email = Email.Create(emailWithSpaces);

        email.Address.Should().Be(_email);
    }

    [Test]
    public void Constructor_ShouldNormalizeEmail()
    {
        var wrongEmail = _email.ToUpperInvariant();
        var email = Email.Create(wrongEmail);

        email.Address.Should().Be(_email);
    }

    [Test]
    public void TwoEmailsWithSameAddress_ShouldBeEqual()
    {
        var email1 = Email.Create(_email);
        var email2 = Email.Create(_email);

        email1.Should().Be(email2);
        email1.GetHashCode().Should().Be(email2.GetHashCode());
    }
}
