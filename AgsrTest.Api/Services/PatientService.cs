using AgsrTest.Api.Domain.Entities;
using AgsrTest.Api.Domain.Exceptions;
using AgsrTest.Api.Domain.Interfaces.Repositories;
using AgsrTest.Api.Domain.Interfaces.Services;
using AgsrTest.Api.Models.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgsrTest.Api.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IStoredProcedureService _procedureService;
    private readonly IMapper _mapper;

    public PatientService(
        IPatientRepository patientRepository, 
        IStoredProcedureService procedureService, 
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _procedureService = procedureService;
        _mapper = mapper;
    }

    public async Task<PatientDto[]> GetAllAsync()
    {
        Patient[] patients = await _patientRepository.GetAll().ToArrayAsync();

        return _mapper.Map<PatientDto[]>(patients);
    }

    public async Task<PatientDto> GetByIdAsync(Guid id)
    {
        var patient = await _patientRepository.GetById(id).FirstOrDefaultAsync();

        if (patient == null)
        {
            throw new EntityNotFoundException($"Patient with Id {id} wasn't found");
        }

        return _mapper.Map<PatientDto>(patient);
    }

    public async Task DeleteAsync(Guid id)
    {
        var patient = await _patientRepository.GetById(id).FirstOrDefaultAsync();

        if (patient == null)
        {
            throw new EntityNotFoundException($"Patient with Id {id} wasn't found");
        }

        await _patientRepository.DeleteAsync(patient);
    }

    public async Task UpdateAsync(Guid id, PatientDto dto)
    {
        var patient = await _patientRepository.GetById(id).FirstOrDefaultAsync();

        if (patient == null)
        {
            throw new EntityNotFoundException($"Patient with Id {id} wasn't found");
        }

        var updated = _mapper.Map(dto, patient);
        await _patientRepository.UpdateAsync(updated);
    }

    public async Task<Guid> CreateAsync(PatientDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name.Family) || dto.BirthDate == null)
        {
            throw new BadRequestException("Required fields are missing");
        }

        Patient patient = _mapper.Map<Patient>(dto);
        Guid id = await _patientRepository.AddAsync(patient);

        return id;
    }

    public async Task<PatientDto[]> SearchPatientsByBirthDate(List<string>? birthDates)
    {
        if (birthDates == null)
        {
            return _mapper.Map<PatientDto[]>(await _patientRepository.GetAll().ToArrayAsync());
        }

        string? mod1 = null;
        DateTime? dateTime1 = null;
        string? mod2 = null;
        DateTime? dateTime2 = null;

        for (int i = 0; i < birthDates.Count && i < 2; i++)
        {
            var date = birthDates[i];
            if (date.Length < 3) continue;

            var prefix = date[..2];
            var datePart = date[2..];

            if (!DateTime.TryParse(datePart, out var parsedDate)) continue;

            if (i == 0)
            {
                mod1 = prefix;
                dateTime1 = parsedDate;
            }
            else if (i == 1)
            {
                mod2 = prefix;
                dateTime2 = parsedDate;
            }
        }

        var result = await _procedureService.SearchPatientsByBirthDateFhir(mod1, dateTime1, mod2, dateTime2);

        return _mapper.Map<PatientDto[]>(result);
    }
}
