using FinanceControl.Application.Interfaces;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Queries;

public class TransactionQueries(AppDbContext context) : ITransactionQueries { }
