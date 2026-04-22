using FinanceControl.Application.Interfaces;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Queries;

public class InvoiceQueries(AppDbContext context) : IInvoiceQueries { }
