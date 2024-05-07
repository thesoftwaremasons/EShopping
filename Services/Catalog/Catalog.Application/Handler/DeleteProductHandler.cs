using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductIdQuery, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductIdQuery request, CancellationToken cancellationToken)
        {
            bool isDeleted = await _productRepository.DeleteProductAsync(request.ProductId);
           
            return isDeleted;
        }
    }
}
