using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext context)
    : BaseRepository<Transaction>(context), ITransactionRepository
{
}
