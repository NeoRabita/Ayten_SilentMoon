using Microsoft.AspNetCore.Http;

namespace OnionArchitecture.WebApi.Services;

public interface IProblemResultFactory
{
    public IResult CreateProblem(Result result);
}
