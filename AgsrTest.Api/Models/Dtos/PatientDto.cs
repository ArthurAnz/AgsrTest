using AgsrTest.Api.Domain.Entities;
using AgsrTest.Api.Domain.Enums;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AgsrTest.Api.Models.Dtos;

public class NameDto : IValidatableObject
{
    public Guid Id { get; set; }
    public string Use { get; set; }
    public string Family { get; set; }
    public List<string> Given { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext vContext)
    {
        if (Given != null && Given.Count > 2)
        {
            yield return new ValidationResult(
                "Given name must have only first name and patronymic",
                new[] { nameof(Given) }
            );
        }
    }
}

public class PatientDto
{
    public NameDto Name { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}

public class PatientDtoProfile : Profile
{
    public PatientDtoProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameDto
            {
                Id = src.Id,
                Use = src.NameUse,
                Family = src.Family,
                Given = new List<string> { src.FirstName, src.Patronymic }
            }))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString().ToLower()));

        CreateMap<PatientDto, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.NameUse, opt => opt.MapFrom(src => src.Name.Use))
            .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Name.Family))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.Given.FirstOrDefault()))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.Name.Given.Skip(1).FirstOrDefault()))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => MapGender(src.Gender)))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));
    }

    private static Gender MapGender(string genderStr)
    {
        return Enum.TryParse<Gender>(genderStr, true, out var gender) ? gender : Gender.Unknown;
    }
}