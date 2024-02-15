using FSH.WebApi.Domain.ATS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FSH.WebApi.Domain.ATS.CustomerModel;
using static System.Collections.Specialized.BitVector32;

namespace FSH.WebApi.Application.ATS.Customer;
public class UpdateCustomerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? SecondaryPhoneNumber { get; set; }
    public string? Street { get; set; }
    public string? Village { get; set; }
    public string? District { get; set; }
    public string? State { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? QRRefference { get; set; }
    public string? Extentions { get; set; }
    [NotMapped]
    public List<ProductItemModel>? ProductItemList { get; set; }

    [Column(TypeName = "jsonb")]
    public string? ProductItems
    {
        get
        {
            return JsonConvert.SerializeObject(ProductItemList);
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                ProductItemList = JsonConvert.DeserializeObject<List<ProductItemModel>>(value);
            }
            else
            {
                ProductItemList = new List<ProductItemModel>();
            }
        }
    }

}
public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CustomerModel> _repository;
    private readonly IStringLocalizer _t;

    public UpdateCustomerRequestHandler(IRepositoryWithEvents<CustomerModel> repository, IStringLocalizer<UpdateCustomerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
          _ = customer
        ?? throw new NotFoundException(_t["Account {0} Not Found.", request.Id]);
        string? fullPath = " ";
        // if (request.ProductItemList.FirstOrDefault().ProductAfter != null)
        // {
        //     var storageDirectory = @"G:\Ats mobies man\ATC_Project\ATC_Project\ATS_API\src\Host\Files\UploadedFiles\";
        //     var uniqueFileName = "update_"+ request.QRRefference + request.Extentions;
        //     fullPath = Path.Combine(storageDirectory, uniqueFileName);
        //     byte[] imageBytes = Convert.FromBase64String(request.ProductItemList.FirstOrDefault().ProductAfter);
        //     File.WriteAllBytes(fullPath, imageBytes);
        // }

        // request.ProductItemList.FirstOrDefault().ProductAfter = fullPath;
        customer.Update(request.FirstName, request.LastName, request.Description, request.PhoneNumber, request.SecondaryPhoneNumber, request.Street, request.Village, request.District, request.State, request.Gender, request.Email, request.QRRefference, request.Extentions, request.ProductItemList, request.ProductItems);



        await _repository.UpdateAsync(customer, cancellationToken);
        return customer.Id;
    }
}