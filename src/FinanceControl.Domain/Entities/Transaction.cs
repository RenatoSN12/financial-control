using FinanceControl.Domain.Enums;
using FinanceControl.Domain.Exceptions;
using FinanceControl.Domain.ValueObjects;

namespace FinanceControl.Domain.Entities;

public class Transaction : AggregateRoot
{
    #region Properties

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    
    public Money Amount { get; private set; } = null!;
    public ETransactionType Type { get; private set; }
    public DateOnly Date { get; private set; }
    public string Description { get; private set; } = string.Empty;

    #endregion

    #region Functions

    private static void ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new InvalidGuidException("O ID do usuário não pode ser vazio");
    }

    private static void ValidateCategoryId(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new InvalidGuidException("O ID da categoria não pode ser vazio");
    }

    private static void ValidateDescription(string description)
    {
        if (string.IsNullOrEmpty(description))
            throw new InvalidTextException("A descrição da transação não pode ser vazia");
    }

    public static Transaction Create(
        Guid userId,
        Guid categoryId,
        Money amount,
        ETransactionType type,
        DateOnly date,
        string description
    )
    {
        var trimmedDescription = description.Trim();

        ValidateUserId(userId);
        ValidateCategoryId(categoryId);
        ValidateDescription(trimmedDescription);

        return new Transaction(
            userId,
            categoryId,
            amount,
            type,
            date,
            trimmedDescription
        );
    }

    #endregion

    #region Constructors

    private Transaction(){}

    private Transaction(
        Guid userId,
        Guid categoryId,
        Money amount,
        ETransactionType type,
        DateOnly date,
        string description
    )
    {
        UserId = userId;
        CategoryId = categoryId;
        Amount = amount;
        Type = type;
        Date = date;
        Description = description;
    }

    #endregion
}