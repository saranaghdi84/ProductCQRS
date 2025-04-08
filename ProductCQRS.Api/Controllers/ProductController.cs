using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCQRS.Application.UseCases.Product.Queries.GetAllProducts;

namespace ProductCQRS.Api.Controllers
{
    public class ProductController(IMediator mediator) : BaseController
    {
        #region Constructor

        private readonly IMediator _mediator = mediator;

        #endregion Constructor
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery getAllProductsQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getAllProductsQuery, cancellationToken);
            return Ok(result);
        }
    }
}
