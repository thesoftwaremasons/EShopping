using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypeQuery, IList<TypeResponse>>
    {
       private readonly ITypesRepository _typesRepository;

        public GetAllTypesHandler(ITypesRepository typesRepository)
        {
            _typesRepository = typesRepository;
        }

    
        public async Task<IList<TypeResponse>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
        {
            var types = await _typesRepository.GetAllTypesAsync();
            var response = ProductMapper.Mapper.Map < IList<TypeResponse>>(types);
            return response;
        }
    }
}
