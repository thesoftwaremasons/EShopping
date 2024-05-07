using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class DeleteProductIdQuery:IRequest<bool>
    {
        public string ProductId { get; set; }
        public DeleteProductIdQuery(string productId)
        {
            ProductId = productId;
        }
    }
}
