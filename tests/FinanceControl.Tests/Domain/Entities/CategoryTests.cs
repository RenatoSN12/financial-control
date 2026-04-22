using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Exceptions;
using FluentAssertions;

namespace FinanceControl.Tests.Domain.Entities;

[TestFixture]
public class CategoryTests
{
    [Test]
    public void Constructor_WithValidParameters_ShouldCreateCategory()
    {
        var userId = Guid.NewGuid();
        var name = "Food";

        var category = Category.Create(userId, name);

        category.Should().NotBeNull();
        category.UserId.Should().Be(userId);
        category.Name.Should().Be(name);
        category.Id.Should().NotBeEmpty();
    }

    [Test]
    public void Create_WithEmptyName_ShouldThrowInvalidTextException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var name = string.Empty;

        // Act
        Action act = () => Category.Create(userId, name);

        // Assert
        act.Should().Throw<InvalidTextException>();
    }

    [Test]
    public void Constructor_WithEmptyGuid_ShouldCreateCategory()
    {
        // Arrange
        var userId = Guid.Empty;
        var name = "Food";

        // Act
        Action act = () => Category.Create(userId, name);

        // Assert
        act.Should().Throw<InvalidGuidException>();
    }
}
