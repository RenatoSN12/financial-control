using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Repositories;

public class CardTransactionRepository(AppDbContext context)
    : BaseRepository<CardTransaction>(context), ICardTransactionRepository
{
}
