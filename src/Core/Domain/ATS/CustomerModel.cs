using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.ATS;
public class CustomerModel : AuditableEntity, IAggregateRoot
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Description { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? SecondaryPhoneNumber { get; private set; }
    public string? Street { get; private set; }
    public string? Village { get; private set; }
    public string? District { get; private set; }
    public string? State { get; private set; }
    public string? Gender { get; private set; }
    public string? Email { get; private set; }
    public string? QRRefference { get; private set; }
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

    public CustomerModel(string? firstName, string? lastName, string? description, string? phoneNumber, string? secondaryPhoneNumber, string? street, string? village, string? district, string? state, string? gender, string? email, string? qRRefference, string? extentions, string? productItems)
    {
        FirstName = firstName;
        LastName = lastName;
        Description = description;
        PhoneNumber = phoneNumber;
        SecondaryPhoneNumber = secondaryPhoneNumber;
        Street = street;
        Village = village;
        State = state;
        Gender = gender;
        Email = email;
        QRRefference = qRRefference;
        Extentions = extentions;
        ProductItemList = new List<ProductItemModel>();
        ProductItems = productItems;
    }

    public CustomerModel Update(string? firstName, string? lastName, string? description, string? phoneNumber, string? secondaryPhoneNumber, string? street, string? village, string? district, string? state, string? gender, string? email, string? qrRefference,string? extentions, List<ProductItemModel> productItemList, string? productItems)
    {
        if (firstName is not null && FirstName?.Equals(firstName) is not true) FirstName = firstName;
        if (lastName is not null && LastName?.Equals(lastName) is not true) LastName = lastName;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (phoneNumber is not null && PhoneNumber?.Equals(phoneNumber) is not true) PhoneNumber = phoneNumber;
        SecondaryPhoneNumber = secondaryPhoneNumber;
        State = state;
        Gender = gender;
        Email = email;
        District = district;
        Village = village;
        Street = street;
        QRRefference = qrRefference;
        Extentions = extentions;
        if (productItems is not null && ProductItems?.Equals(productItems) is not true) ProductItems = productItems;
        if (productItemList is not null && ProductItemList?.Equals(productItemList) is not true) ProductItemList = productItemList;
        return this;
    }

    [NotMapped]
    public class ProductItemModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Issue { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ReturnGivenDate { get; set; }
        public string? ProductBefore { get; set; }
        public string? ProductAfter { get; set; }
        public string? Description { get; set; }
    }
}
