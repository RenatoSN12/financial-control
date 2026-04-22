using FinanceControl.Domain.Entities;

namespace FinanceControl.Domain.Repositories;

public interface ITransactionRepository : IWriteRepository<Transaction>
{
}
