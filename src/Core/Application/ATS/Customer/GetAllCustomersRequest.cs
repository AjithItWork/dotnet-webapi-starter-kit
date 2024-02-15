using FSH.WebApi.Domain.ATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Customer;
public class GetAllCustomersRequest : IRequest<List<CustomerModel>>
{
    public GetAllCustomersRequest()
    {

    }

    public class GetAllCustomerRequestHandler : IRequestHandler<GetAllCustomersRequest, List<CustomerModel>>
    {
        private readonly IRepositoryWithEvents<CustomerModel> _repository;

        public GetAllCustomerRequestHandler(IRepositoryWithEvents<CustomerModel> repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerModel>> Handle(GetAllCustomersRequest request, CancellationToken cancellationToken)
        {
            List<CustomerModel> Customers = new List<CustomerModel>();
            Customers = await _repository.ListAsync(cancellationToken);

            return Customers;
        }
    }

}
