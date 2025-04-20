using AgsrTest.Api.Domain.Entities;
using AgsrTest.Api.Domain.Interfaces.Services;
using AgsrTest.Api.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AgsrTest.Api.Services;

public class StoredProcedureService : IStoredProcedureService
{
    private readonly AgsrTestDbContext _context;

    public StoredProcedureService(AgsrTestDbContext context)
    {
        _context = context;
    }

    public async Task<Patient[]> SearchPatientsByBirthDateFhir(
        string? modifier1, 
        DateTime? dateTime1, 
        string? modifier2, 
        DateTime? dateTime2)
    {
        var mod1 = new SqlParameter("@Modifier1", modifier1 ?? (object)DBNull.Value);
        var date1Param = new SqlParameter("@DateTime1", dateTime1 ?? (object)DBNull.Value);
        var mod2 = new SqlParameter("@Modifier2", modifier2 ?? (object)DBNull.Value);
        var date2Param = new SqlParameter("@DateTime2", dateTime2 ?? (object)DBNull.Value);

        return await _context.Patients
            .FromSqlRaw("EXEC SearchPatientsByBirthDateFhir @Modifier1, @DateTime1, @Modifier2, @DateTime2",
                mod1, date1Param, mod2, date2Param)
            .ToArrayAsync();
    }
}
