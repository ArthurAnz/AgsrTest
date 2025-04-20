using AgsrTest.Api.Domain.Entities;

namespace AgsrTest.Api.Domain.Interfaces.Services;

public interface IStoredProcedureService
{
    Task<Patient[]> SearchPatientsByBirthDateFhir(string? modifier1, DateTime? dateTime1, string? modifier2, DateTime? dateTime2);
}
