using FSH.WebApi.Domain.ATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Notes;
public class CreateNotesRequest : IRequest<Guid>
{
    public Guid NoteOwnerId { get; set; }
    public string? NoteTitle { get; set; }
    public string? NoteContent { get; set; }
    public Guid ParentId { get; set; }
    public string? RelatedTo { get; set; }
}

public class CreateRequestHandler : IRequestHandler<CreateNotesRequest, Guid>
{
    private readonly IRepositoryWithEvents<NotesModel> _repository;
    public CreateRequestHandler(IRepositoryWithEvents<NotesModel> repository) => _repository = repository;
   
    public async Task<Guid> Handle(CreateNotesRequest request, CancellationToken cancellationToken)
    {
        NotesModel note = new NotesModel(request.NoteOwnerId, request.NoteTitle, request.NoteContent, request.ParentId, request.RelatedTo);
        await _repository.AddAsync(note, cancellationToken);
        return note.Id;
    }
}
