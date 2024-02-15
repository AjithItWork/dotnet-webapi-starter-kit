using FSH.WebApi.Domain.ATS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static FSH.WebApi.Domain.ATS.CustomerModel;

namespace FSH.WebApi.Application.ATS.Customer;
public class CreateCustomerRequest : IRequest<Guid>
{
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

public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Guid>
{
    private readonly IRepositoryWithEvents<CustomerModel> _repository;

    public CreateCustomerRequestHandler(IRepositoryWithEvents<CustomerModel> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {

        //string? fullPath = " ";
        // if (request.ProductItemList.FirstOrDefault().ProductBefore != null)
        // {
        //     var storageDirectory = @"G:\Ats mobies man\ATC_Project\ATC_Project\ATS_API\src\Host\Files\UploadedFiles\";
        //     var uniqueFileName = request.QRRefference + request.Extentions;
        //     fullPath = Path.Combine(storageDirectory, uniqueFileName);
        //     byte[] imageBytes = Convert.FromBase64String(request.ProductItemList.FirstOrDefault().ProductBefore);
        //     File.WriteAllBytes(fullPath, imageBytes);
        // }

        // request.ProductItemList.FirstOrDefault().ProductBefore = fullPath;
        var customer = new CustomerModel(request.FirstName, request.LastName, request.Description, request.PhoneNumber, request.SecondaryPhoneNumber, request.Street, request.Village, request.District, request.State, request.Gender, request.Email, request.QRRefference, request.Extentions, request.ProductItems);

        await _repository.AddAsync(customer, cancellationToken);
        return customer.Id;
    }
}
