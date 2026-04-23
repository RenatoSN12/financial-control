using FinanceControl.Domain.ValueObjects;

namespace FinanceControl.Domain.Entities;

public class User : AggregateRoot
{
    #region Properties

    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    
    public string? RefreshTokenHash { get; private set; }
    public DateTime? RefreshTokenExpiresAt { get; private set; }

    private readonly List<Card> _cards = [];
    public IReadOnlyCollection<Card> Cards => _cards;
    
    private readonly List<Category> _categories = [];
    public IReadOnlyCollection<Category> Categories => _categories;

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    #endregion

    #region Constructors
    
    private User(string firstName, string lastName, string email, string passwordHash)
    {
        Name = Name.Create(firstName, lastName);
        Email = Email.Create(email);
        IsActive = true;
        PasswordHash = passwordHash;
    }

    private User(){}

    #endregion

    #region Functions

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash
    )
    {
        return new User(firstName, lastName, email, passwordHash);
    }

    public void UpdateRefreshToken(string refreshTokenHash, DateTime expiresAt)
    {
        RefreshTokenHash = refreshTokenHash;
        RefreshTokenExpiresAt = expiresAt;
    }

    #endregion
}