using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FSH.WebApi.Domain.ATS.CustomerModel;

namespace FSH.WebApi.Application.ATS.Customer;
public class CustomerDto
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
