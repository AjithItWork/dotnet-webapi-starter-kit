using FSH.WebApi.Application.ATS.Customer;
using FSH.WebApi.Domain.ATS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.Catalog;
public class NaveenController : VersionedApiController
{
    [HttpPost]
    [OpenApiOperation("Create a new Prospect.", "")]
    public Task<Guid> CreateAsync(CreateCustomerRequest request)
    {
        return Mediator.Send(request);

    }

    [HttpGet]
    [OpenApiOperation("Get All Customers", "")]
    public async Task<List<CustomerModel>> GetAllCustomersAsync()
    {
        var customerModels = new List<CustomerModel>();
        return await Mediator.Send(new GetAllCustomersRequest());
    }
}
