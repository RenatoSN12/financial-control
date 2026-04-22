using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Repositories;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Repositories;

public class InvoiceRepository(AppDbContext context)
    : BaseRepository<Invoice>(context), IInvoiceRepository
{
}
