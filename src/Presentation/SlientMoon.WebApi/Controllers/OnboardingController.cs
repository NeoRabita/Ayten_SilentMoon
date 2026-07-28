using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Features.Queries.Topics.GetTopics;
using SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateTopics;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers
{
    
    public class OnboardingController : BaseController
    {
        [HttpGet("topics")]
        public async Task<IResult> GetTopics()
        {
            var result = await Dispatcher.Send(new GetTopicsQuery());

            return HandleResult(result);
        }

        [HttpPut("topics")]
        public async Task<IResult> UpdateTopics([FromBody] UpdateTopicsCommand command)
        {
            var result = await Dispatcher.Send(command);

            return HandleResult(result);
        }
    }
}
