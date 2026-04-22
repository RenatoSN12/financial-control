using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.ValueObjects;
using FluentAssertions;

namespace FinanceControl.Tests.Domain.ValueObjects;

[TestFixture]
public class PasswordTests
{
    private string _validPassword;

    private static IEnumerable<string> InvalidPasswords()
    {
        yield return "";
        yield return "123";
        yield return "1234567";
        yield return "123456789";
        yield return "1234567890Re";
        yield return "renato123*";
        yield return "Renato*";
        yield return "Renato123";
        yield return "Rto123@";
        yield return new string('a', 101);
    }

    [SetUp]
    public void Setup()
    {
        _validPassword = "SenhaForte@123";
    }

    [Test]
    public void Create_WithValidPassword_ShouldReturnPasswordInstance()
    {
        var password = Password.Create(_validPassword);

        password.Should().NotBeNull();
        password.Value.Should().Be(_validPassword);
    }

    [Test]
    [TestCaseSource(nameof(InvalidPasswords))]
    public void Create_WithInvalidPassword_ShouldThrowInvalidPasswordException(string invalidPassword)
    {
        Action act = () => Password.Create(invalidPassword);

        act.Should().Throw<InvalidPasswordException>();
    }

    [Test]
    public void TwoPasswordsWithSameValue_ShouldBeEqual()
    {
        var password1 = Password.Create(_validPassword);
        var password2 = Password.Create(_validPassword);

        password1.Should().Be(password2);
        password1.GetHashCode().Should().Be(password2.GetHashCode());
    }
}
