using AgsrTest.Api.Models.Dtos;

namespace AgsrTest.Api.Domain.Interfaces.Services;

public interface IPatientService
{
    Task<PatientDto[]> GetAllAsync();
    Task<PatientDto> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, PatientDto dto);
    Task<Guid> CreateAsync(PatientDto dto);
    Task<PatientDto[]> SearchPatientsByBirthDate(List<string>? birthDates);
}
