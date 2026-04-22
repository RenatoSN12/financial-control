using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Enums;
using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.ValueObjects;
using FluentAssertions;

namespace FinanceControl.Tests.Domain.Entities;

[TestFixture]
public class TransactionTests
{
    // private Guid _userId;
    // private Guid _categoryId;
    // private Money _amount;
    // private DateOnly _date;
    // private string _description;

    // [SetUp]
    // public void Setup()
    // {
    //     _userId = Guid.NewGuid();
    //     _categoryId = Guid.NewGuid();
    //     _amount = new Money(100m);
    //     _date = DateOnly.FromDateTime(DateTime.Today);
    //     _description = "Descrição da transação";
    // }

    // #region Creation Tests

    // [Test]
    // public void Create_WithValidParameters_ShouldCreateTransactionWithAllProperties()
    // {
    //     var transaction = Transaction.Create(_userId, _categoryId, _amount, ETransactionType.Income, _date, _description);

    //     transaction.Should().NotBeNull();
    //     transaction.Id.Should().NotBeEmpty();
    //     transaction.UserId.Should().Be(_userId);
    //     transaction.CategoryId.Should().Be(_categoryId);
    //     transaction.Amount.Should().Be(_amount);
    //     transaction.Type.Should().Be(ETransactionType.Income);
    //     transaction.Date.Should().Be(_date);
    //     transaction.Description.Should().Be(_description);
    // }

    // [Test]
    // public void Create_WithMaximumValidDescription_ShouldAccept255Characters()
    // {
    //     var description = new string('a', 255);

    //     var transaction = Transaction.Create(_userId, _categoryId, _amount, ETransactionType.Income, _date, description);

    //     transaction.Description.Should().HaveLength(255);
    // }

    // #endregion

    // #region Validation Tests

    // [Test]
    // public void Create_WithEmptyUserId_ShouldThrowInvalidGuidException()
    // {
    //     var act = () => Transaction.Create(Guid.Empty, _categoryId, _amount, ETransactionType.Income, _date, _description);

    //     act.Should().Throw<InvalidGuidException>()
    //         .WithMessage("O ID do usuário não pode ser vazio");
    // }

    // [Test]
    // public void Create_WithEmptyCategoryId_ShouldThrowInvalidGuidException()
    // {
    //     var act = () => Transaction.Create(_userId, Guid.Empty, _amount, ETransactionType.Income, _date, _description);

    //     act.Should().Throw<InvalidGuidException>()
    //         .WithMessage("O ID da categoria não pode ser vazio");
    // }

    // [Test]
    // public void Create_WithEmptyDescription_ShouldThrowInvalidTextException()
    // {
    //     var act = () => Transaction.Create(_userId, _categoryId, _amount, ETransactionType.Income, _date, string.Empty);

    //     act.Should().Throw<InvalidTextException>()
    //         .WithMessage("A descrição da transação não pode ser vazia");
    // }

    // [Test]
    // public void Create_WithNullDescription_ShouldThrowInvalidTextException()
    // {
    //     var act = () => Transaction.Create(_userId, _categoryId, _amount, ETransactionType.Income, _date, null!);

    //     act.Should().Throw<InvalidTextException>()
    //         .WithMessage("A descrição da transação não pode ser vazia");
    // }

    // [Test]
    // public void Create_WithDescriptionExceeding255Characters_ShouldThrowInvalidTextException()
    // {
    //     var description = new string('a', 256);

    //     var act = () => Transaction.Create(_userId, _categoryId, _amount, ETransactionType.Income, _date, description);

    //     act.Should().Throw<InvalidTextException>()
    //         .WithMessage("A descrição da transação não pode ter mais de 255 caracteres");
    // }

    // #endregion

}
