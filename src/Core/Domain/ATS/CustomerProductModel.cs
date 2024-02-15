using System.ComponentModel.DataAnnotations.Schema;
namespace FSH.WebApi.Domain.ATS;
public class CustomerProductModel : AuditableEntity, IAggregateRoot
{
    public string? ProductName { get; set; }
    public string? Issue { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public DateTime? ReturnGivenDate { get; set; }
    public string? ProductBefore { get; set; }
    public string? ProductAfter { get; set; }
    public string? Description { get; set; }
}

public CustomerProductModel(string? productName, string? issue, DateTime? receivedDate, DateTime? returnGivenDate, string? productAfter, string? productBefore, string? description )
{
    ProductName = productName;
    Issue = issue;
    ReceivedDate = receivedDate;
    ReturnGivenDate = returnGivenDate;
    productAfter = productAfter;
    ProductBefore = productBefore;
    Description = description;
}

public CustomerProductModel Update(string? productName, string? issue, DateTime? receivedDate, DateTime? returnGivenDate, string? productAfter, string? productBefore, string? description )
{
    if (productName is not null && ProductName?.Equals(productName) is not true) ProductName = productName;
    if (issue is not null && Issue?.Equals(issue) is not true) Issue = issue;
    if (receivedDate is not null && ReceivedDate?.Equals(receivedDate) is not true) ReceivedDate = receivedDate;
    if (returnGivenDate is not null && ReturnGivenDate?.Equals(returnGivenDate) is not true) ReturnGivenDate = returnGivenDate;
    if (productAfter is not null && ProductAfter?.Equals(productAfter) is not true) ProductAfter = productAfter;
    if (productBefore is not null && ProductBefore?.Equals(productBefore) is not true) ProductBefore = productBefore;
    if (description is not null && Description?.Equals(description) is not true) Description = description;
    return this;
}