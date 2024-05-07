﻿
using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler
{
    public class GetAllBrandHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandHandler(IBrandRepository brandRepository)
        {
            _brandRepository= brandRepository;
      
        }
        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetAllBrandsAsync();
            var response=ProductMapper.Mapper.Map<IList<BrandResponse>>(brandList);
            return response;
        }
    }
}
