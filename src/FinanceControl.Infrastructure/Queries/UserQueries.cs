using FinanceControl.Application.Interfaces;
using FinanceControl.Infrastructure.Data;

namespace FinanceControl.Infrastructure.Queries;

public class UserQueries(AppDbContext context) : IUserQueries { }
