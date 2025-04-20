using AgsrTest.Api.Domain.Entities;

namespace AgsrTest.Api.Domain.Interfaces.Repositories;

public interface IPatientRepository
{
    IQueryable<Patient> GetAll();
    IQueryable<Patient> GetById(Guid id);
    Task<Guid> AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Patient patient);
}
