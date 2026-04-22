using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Repositories;

public class CardRepository(AppDbContext context)
    : BaseRepository<Card>(context), ICardRepository
{
}
