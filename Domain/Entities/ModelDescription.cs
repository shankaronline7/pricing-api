using Pricing.Domain.Common;
using System.ComponentModel.DataAnnotations;

public class ModelDescription : BaseAuditableEntity
{
    [Key]
    public long Id { get; set; }
    public string ModelDescriptionName { get; set; }
    public string? Description { get; set; }
}
