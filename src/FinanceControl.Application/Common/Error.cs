namespace FinanceControl.Application.Common;

public enum ErrorType {
    NotFound,
    Validation,
    Unauthorized,
    Unexpected
}

public record Error(string Code, ErrorType Type, string Message)
{
    public static Error NotFound(string message = "O recurso não foi encontrado") => 
        new("NOT_FOUND", ErrorType.NotFound, message);
    
    public static Error Validation(string message = "Ocorreu um erro de validação") => 
        new("VALIDATION", ErrorType.Validation, message);
    
    public static Error Unauthorized(string message = "O usuário não está autorizado a executar esta ação") => 
        new("UNAUTHORIZED", ErrorType.Unauthorized, message);
    
    public static Error Unexpected(string message = "Ocorreu um erro inesperado") => 
        new("UNEXPECTED", ErrorType.Unexpected, message);
}