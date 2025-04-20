using AgsrTest.Api.Domain.Entities;
using AgsrTest.Api.Domain.Interfaces.Repositories;

namespace AgsrTest.Api.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly AgsrTestDbContext _context;

    public PatientRepository(AgsrTestDbContext context)
    {
        _context = context;
    }

    public IQueryable<Patient> GetAll() => _context.Patients;

    public IQueryable<Patient> GetById(Guid id) => _context.Patients.Where(p => p.Id == id);

    public async Task<Guid> AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return patient.Id;
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
}
