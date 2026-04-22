using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Exceptions;
using FluentAssertions;

namespace FinanceControl.Tests.Domain.Entities;

[TestFixture]
public class UserTests
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _passwordHash;

    [SetUp]
    public void Setup()
    {
        _firstName = "Renato";
        _lastName = "Nascimento";
        _email = "renato@example.com";
        _passwordHash = "$2a$12$hashedPasswordExample";
    }

    [Test]
    public void Create_WithValidData_ShouldCreateUser()
    {
        var user = User.Create(_firstName, _lastName, _email, _passwordHash);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.IsActive.Should().BeTrue();
        user.PasswordHash.Should().Be(_passwordHash);
    }
}
