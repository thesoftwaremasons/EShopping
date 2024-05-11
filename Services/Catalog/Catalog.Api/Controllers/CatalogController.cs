using Catalog.Application.Commands;
using Catalog.Application.Handler;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}",Name="GetProductId")]
        [ProducesResponseType(typeof(ProductResponse),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result=await _mediator.Send(query);
            return Ok(result); 
        }
        /// <summary>
        /// Get Product Name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route( "GetAllProducts")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProduct()
        {
            var query = new GetAllProductQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// Get All Pro
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(TypeResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypeResponse>>> GetAllTypes()
        {
            var query = new GetAllTypeQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Product By Brand Name
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByBrandName")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByBrandName(string brandName)
        {
            var query = new GetProductByBrandQuery(brandName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="createProductCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
         
            var result = await _mediator.Send(createProductCommand);
            return Ok(result);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="updateProductCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {

            var result = await _mediator.Send(updateProductCommand);
            return Ok(result);
        }
        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var query = new DeleteProductIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
