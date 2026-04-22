using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.ValueObjects;
using FluentAssertions;

namespace FinanceControl.Tests.Domain.ValueObjects;

[TestFixture]
public class NameTests
{
    private string _firstName;
    private string _lastName;

    private static IEnumerable<TestCaseData> InvalidNames()
    {
        yield return new TestCaseData("", "Nascimento").SetName("EmptyFirstName");
        yield return new TestCaseData("   ", "Nascimento").SetName("WhitespaceFirstName");
        yield return new TestCaseData(new string('a', 101), "Nascimento").SetName("FirstNameTooLong");
        yield return new TestCaseData("Renato", "").SetName("EmptyLastName");
        yield return new TestCaseData("Renato", "   ").SetName("WhitespaceLastName");
        yield return new TestCaseData("Renato", new string('a', 151)).SetName("LastNameTooLong");
    }

    [SetUp]
    public void Setup()
    {
        _firstName = "Renato";
        _lastName = "Nascimento";
    }

    [Test]
    public void Create_WithValidNames_ShouldReturnNameInstance()
    {
        var name = Name.Create(_firstName, _lastName);

        name.Should().NotBeNull();
        name.FirstName.Should().Be(_firstName);
        name.LastName.Should().Be(_lastName);
        name.FullName.Should().Be($"{_firstName} {_lastName}");
    }

    [Test]
    [TestCaseSource(nameof(InvalidNames))]
    public void Create_WithInvalidNames_ShouldThrowInvalidTextException(string firstName, string lastName)
    {
        Action act = () => Name.Create(firstName, lastName);

        act.Should().Throw<InvalidTextException>();
    }

    [Test]
    public void Create_WithNamesWithSpaces_ShouldTrimSpaces()
    {
        var firstNameWithSpaces = "  " + _firstName + "  ";
        var lastNameWithSpaces = "  " + _lastName + "  ";

        var name = Name.Create(firstNameWithSpaces, lastNameWithSpaces);

        name.FirstName.Should().Be(_firstName);
        name.LastName.Should().Be(_lastName);
    }

    [Test]
    public void TwoNamesWithSameValues_ShouldBeEqual()
    {
        var name1 = Name.Create(_firstName, _lastName);
        var name2 = Name.Create(_firstName, _lastName);

        name1.Should().Be(name2);
        name1.GetHashCode().Should().Be(name2.GetHashCode());
    }
}
