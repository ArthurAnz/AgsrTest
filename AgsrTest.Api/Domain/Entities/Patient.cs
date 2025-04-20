using AgsrTest.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AgsrTest.Api.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }

    public string? NameUse { get; set; }

    [Required]
    public string Family { get; set; }

    public string? FirstName { get; set; }

    public string? Patronymic { get; set; }

    [Required]
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public bool? Active { get; set; }
}
