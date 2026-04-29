using FinanceControl.Application.Common;

namespace FinanceControl.Api;

public static class ApiResponseHandler
{
    
    public static IResult ToHttpResponse(this Result result)
    {
        if(result.IsSuccess)
            return Results.Ok(result);

        return MapErrorToHttpResponse(result.Error!);
    }

    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        if(result.IsSuccess)
            return Results.Ok(result.Value);

        return MapErrorToHttpResponse(result.Error!);
    }

    public static IResult ToCreatedResponse<T>(this Result<T> result, string uri)
    {
        if(result.IsSuccess)
            return Results.Created(uri, result.Value);

        return MapErrorToHttpResponse(result.Error!);
    }

    public static IResult ToNoContentResponse(this Result result)
    {
        if(result.IsSuccess)
            return Results.NoContent();

        return MapErrorToHttpResponse(result.Error!);
    }

    private static IResult MapErrorToHttpResponse(this Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => Results.NotFound(error),
            ErrorType.Validation => Results.BadRequest(error),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Unexpected => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: error.Code,
                detail: error.Message
            ),
            _ => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Erro inesperado",
                detail: "Ocorreu um erro inesperado ao processar a requisição"
            )
        };  
    }
}