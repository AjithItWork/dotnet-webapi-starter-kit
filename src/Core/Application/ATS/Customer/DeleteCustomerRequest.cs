using FSH.WebApi.Domain.ATS;
using FSH.WebApi.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Customer;
public class DeleteCustomerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteCustomerRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, Guid>
{
    private readonly IRepository<CustomerModel> _repository;
    private readonly IStringLocalizer _t;

    public DeleteCustomerRequestHandler(IRepository<CustomerModel> repository, IStringLocalizer<DeleteCustomerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var Customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = Customer ?? throw new NotFoundException(_t["customer {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        Customer.DomainEvents.Add(EntityDeletedEvent.WithEntity(Customer));

        await _repository.DeleteAsync(Customer, cancellationToken);

        return request.Id;
    }
}

