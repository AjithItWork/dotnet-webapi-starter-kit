using DN.WebApi.Application.Common.Filters;
using DN.WebApi.Application.Common.Persistance;
using DN.WebApi.Application.Common.Specifications;
using DN.WebApi.Application.Wrapper;
using DN.WebApi.Domain.Catalog.Brands;
using MediatR;

namespace DN.WebApi.Application.Catalog.Brands;

public class SearchBrandsRequest : PaginationFilter, IRequest<PaginatedResult<BrandDto>>
{
}

public class SearchBrandsRequestHandler : IRequestHandler<SearchBrandsRequest, PaginatedResult<BrandDto>>
{
    private readonly IRepositoryAsync _repository;

    public SearchBrandsRequestHandler(IRepositoryAsync repository) => _repository = repository;

    public Task<PaginatedResult<BrandDto>> Handle(SearchBrandsRequest request, CancellationToken cancellationToken)
    {
        var specification = new PaginationSpecification<Brand>
        {
            AdvancedSearch = request.AdvancedSearch,
            Keyword = request.Keyword,
            OrderBy = x => x.OrderBy(b => b.Name),
            OrderByStrings = request.OrderBy,
            PageIndex = request.PageNumber,
            PageSize = request.PageSize
        };

        return _repository.GetListAsync<Brand, BrandDto>(specification, cancellationToken);
    }
}