
using FSH.WebApi.Application.ATS.Customer;
using FSH.WebApi.Application.ATS.Notes;
using FSH.WebApi.Domain.ATS;

namespace FSH.WebApi.Host.Controllers.ATS;
public class CustomerController : VersionedApiController
{
    [HttpPost]
    [OpenApiOperation("Create a new Customer","")]
    public Task<Guid> CreateAsync(CreateCustomerRequest request)
    {
        return Mediator.Send(request);

    }

    [HttpGet]
    [OpenApiOperation("Get All Customers","")]
    public async Task<List<CustomerModel>> GetAllCustomersAsync()
    {
        var customerModels = new List<CustomerModel>();
        customerModels = await Mediator.Send(new GetAllCustomersRequest());
        return customerModels;
    }
    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get Customer By Id details.", "")]
    public Task<CustomerDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCustomerByIdRequest(id));
    }
    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a Customer", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCustomerRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a Customer", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCustomerRequest(id));
    }

}
