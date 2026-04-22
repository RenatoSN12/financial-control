using FinanceControl.Application.Interfaces;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Queries;

public class CardQueries(AppDbContext context) : ICardQueries { }
