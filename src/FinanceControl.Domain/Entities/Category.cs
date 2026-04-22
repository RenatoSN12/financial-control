using FinanceControl.Domain.Exceptions;

namespace FinanceControl.Domain.Entities;

public class Category : AggregateRoot
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    #region Constructors

    public static Category Create(Guid userId, string name)
    {
        if (userId == Guid.Empty)
            throw new InvalidGuidException("O ID do usuário não pode ser vazio");

        if (string.IsNullOrEmpty(name))
            throw new InvalidTextException("O nome da categoria não pode ser vazio");

        if (name.Length > 100)
            throw new InvalidTextException("O nome da categoria não pode ter mais de 100 caracteres");

        return new Category(userId, name);
    }

    private Category(){}
    
    private Category(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }

    #endregion
}