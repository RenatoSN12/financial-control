using FinanceControl.Application.Interfaces;
using FinanceControl.Application.UseCases.Categories.Queries.GetAllCategoriesByUser;
using FluentAssertions;
using NSubstitute;

namespace FinanceControl.Tests.Application.UseCases.Categories;

[TestFixture]
public class GetAllCategoriesByUserHandlerTests
{
    private ICategoryQueries _categoryQueries = null!;
    private GetAllCategoriesByUserHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _categoryQueries = Substitute.For<ICategoryQueries>();
        _handler = new GetAllCategoriesByUserHandler(_categoryQueries);
    }

    [Test]
    public async Task HandleAsync_WithValidUserId_ShouldReturnCategories()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetAllCategoriesByUserQuery(userId);
        
        var expectedCategories = new List<GetAllCategoriesByUserResponse>
        {
            new(Guid.NewGuid(), "Food"),
            new(Guid.NewGuid(), "Transport"),
            new(Guid.NewGuid(), "Entertainment")
        };

        _categoryQueries
            .GetAllByUserAsync(userId, Arg.Any<CancellationToken>())
            .Returns(expectedCategories);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(3);
        result.Value.Should().BeEquivalentTo(expectedCategories);
    }

    [Test]
    public async Task HandleAsync_WithUserWithoutCategories_ShouldReturnEmptyList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetAllCategoriesByUserQuery(userId);
        
        var emptyCategories = new List<GetAllCategoriesByUserResponse>();

        _categoryQueries
            .GetAllByUserAsync(userId, Arg.Any<CancellationToken>())
            .Returns(emptyCategories);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Test]
    public async Task HandleAsync_ShouldCallCategoryQueriesOnce()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetAllCategoriesByUserQuery(userId);
        
        var categories = new List<GetAllCategoriesByUserResponse>
        {
            new(Guid.NewGuid(), "Food")
        };

        _categoryQueries
            .GetAllByUserAsync(userId, Arg.Any<CancellationToken>())
            .Returns(categories);

        // Act
        await _handler.HandleAsync(query);

        // Assert
        await _categoryQueries.Received(1).GetAllByUserAsync(userId, Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task HandleAsync_ShouldPassCancellationTokenToQuery()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetAllCategoriesByUserQuery(userId);
        var cancellationToken = new CancellationToken();
        
        var categories = new List<GetAllCategoriesByUserResponse>();

        _categoryQueries
            .GetAllByUserAsync(userId, cancellationToken)
            .Returns(categories);

        // Act
        await _handler.HandleAsync(query, cancellationToken);

        // Assert
        await _categoryQueries.Received(1).GetAllByUserAsync(userId, cancellationToken);
    }
}
