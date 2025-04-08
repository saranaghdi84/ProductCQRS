using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Api.Controllers
{
    [ApiController]
    [Route("api/V1/[controller]/[action]")]
    public class BaseController() : ControllerBase
    {
        //        protected readonly ISender MediatR = sender;

        //        protected IActionResult OK<T>(Result<T> response)
        //            => response.IsSuccess || response.Error == Error.None ? Ok(response) : (IActionResult)Ok(response.));
        //    }
    }
}
