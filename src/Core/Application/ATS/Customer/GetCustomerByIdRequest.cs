using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.ATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Customer;
public class GetCustomerByIdRequest : IRequest<CustomerDto>
{
    public Guid Id { get; set; }

    public GetCustomerByIdRequest(Guid id) => Id = id;
}
public class CustomerByIdSpec : Specification<GetCustomerByIdRequest, CustomerDto>, ISingleResultSpecification
{
    public CustomerByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}
public class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, CustomerDto>
{

    private readonly IRepository<CustomerModel> _repository;
    private readonly IStringLocalizer _t;

    public GetCustomerByIdRequestHandler(IRepository<CustomerModel> repository, IStringLocalizer<GetCustomerByIdRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<CustomerDto> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken) =>

        await _repository.GetBySpecAsync(
               (ISpecification<CustomerModel,CustomerDto>)new CustomerByIdSpec(request.Id), cancellationToken)
           ?? throw new NotFoundException(_t["Customer {0} Not Found.", request.Id]);
}
