using FSH.WebApi.Application.ATS.Customer;
using FSH.WebApi.Domain.ATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Notes;
public class GetNoteByIdRequest : IRequest<NotesDto>
{
    public Guid Id { get; set; }
    public GetNoteByIdRequest(Guid id) => Id = id;
    
}
public class NoteByIdSpec : Specification<GetNoteByIdRequest, NotesDto>, ISingleResultSpecification
{
    public NoteByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}
public class GetNoteByIdRequestHandler : IRequestHandler<GetNoteByIdRequest, NotesDto>
{

    private readonly IRepository<NotesModel> _repository;
    private readonly IStringLocalizer _t;

    public GetNoteByIdRequestHandler(IRepository<NotesModel> repository, IStringLocalizer<GetNoteByIdRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<NotesDto> Handle(GetNoteByIdRequest request, CancellationToken cancellationToken) =>

        await _repository.GetBySpecAsync(
               (ISpecification<NotesModel, NotesDto>)new NoteByIdSpec(request.Id), cancellationToken)
           ?? throw new NotFoundException(_t["Customer {0} Not Found.", request.Id]);


}

