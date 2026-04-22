namespace FinanceControl.Application.Abstractions;

public interface ICommand { }

public interface ICommand<TResult> : ICommand;