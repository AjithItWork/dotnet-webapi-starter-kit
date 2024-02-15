using FSH.WebApi.Application.ATS.Customer;
using FSH.WebApi.Domain.ATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.ATS.Notes;
public class GetAllNotesRequest : IRequest<List<NotesModel>>
{
    public GetAllNotesRequest()
    {
            
    }
    public class GetAllNotesRequestHandler : IRequestHandler<GetAllNotesRequest, List<NotesModel>>
    {
        private readonly IRepositoryWithEvents<NotesModel> _repository;

        public GetAllNotesRequestHandler(IRepositoryWithEvents<NotesModel> repository)
        {
            _repository = repository;
        }

        public async Task<List<NotesModel>> Handle(GetAllNotesRequest request, CancellationToken cancellationToken)
        {
            List<NotesModel> Notes = new List<NotesModel>();
            Notes = await _repository.ListAsync(cancellationToken);


            return Notes;
        }
    }
}
