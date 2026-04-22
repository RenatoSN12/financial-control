namespace FinanceControl.Domain.Repositories;

// Separa a responsabilidade de persistir do repositório
// O Command Handler chama CommitAsync() ao final da operação
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
