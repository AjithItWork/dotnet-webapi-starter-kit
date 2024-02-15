using FSH.WebApi.Application.ATS.Customer;
using FSH.WebApi.Application.ATS.Notes;
using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Domain.ATS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.ATS;
public class NoteController : VersionedApiController
{
    [HttpPost]
    [OpenApiOperation("Create a new Note", "")]
    public Task<Guid> CreateAsync(CreateNotesRequest request)
    {
        return Mediator.Send(request);

    }

    [HttpGet]
    [OpenApiOperation("Get All Customers", "")]
    public async Task<List<NotesModel>> GetAllCustomersAsync()
    {
        var customerModels = new List<NotesModel>();
        customerModels = await Mediator.Send(new GetAllNotesRequest());
        return customerModels;
    }
    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get Note By Id details.", "")]
    public Task<NotesDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetNoteByIdRequest(id));
    }
    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a Note", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateNotesRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a Note", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteNoteRequest(id));
    }

}
