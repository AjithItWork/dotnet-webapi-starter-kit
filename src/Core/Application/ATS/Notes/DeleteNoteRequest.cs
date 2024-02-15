using FSH.WebApi.Application.ATS.Customer;
using FSH.WebApi.Domain.ATS;
using FSH.WebApi.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Notes;
public class DeleteNoteRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteNoteRequest(Guid id)
    {
        Id = id;
    }
}
public class DeleteNoteRequestHandler : IRequestHandler<DeleteNoteRequest, Guid>
{
    private readonly IRepository<NotesModel> _repository;
    private readonly IStringLocalizer _t;

    public DeleteNoteRequestHandler(IRepository<NotesModel> repository, IStringLocalizer<DeleteNoteRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        var Note = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = Note ?? throw new NotFoundException(_t["customer {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        Note.DomainEvents.Add(EntityDeletedEvent.WithEntity(Note));

        await _repository.DeleteAsync(Note, cancellationToken);

        return request.Id;
    }
}
