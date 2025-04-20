using AgsrTest.Api.Domain.Exceptions;
using AgsrTest.Api.Domain.Interfaces.Services;
using AgsrTest.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AgsrTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Get all patients
    /// </summary>
    /// <returns>Array of patients</returns>
    /// <responce code="200"></responce>
    /// <responce code="404">Not found</responce>
    /// <responce code="500">Internal server error</responce>
    [ProducesResponseType(200, Type = typeof(PatientDto[]))]
    [ProducesResponseType(404, Type = typeof(NotFoundError))]
    [ProducesResponseType(500, Type = typeof(InternalServerError))]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        PatientDto[] result = await _patientService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get patient by id
    /// </summary>
    /// <returns>Id of the patient</returns>
    /// <responce code="200"></responce>
    /// <responce code="404">Not found</responce>
    /// <responce code="500">Internal server error</responce>
    [ProducesResponseType(200, Type = typeof(PatientDto))]
    [ProducesResponseType(404, Type = typeof(NotFoundError))]
    [ProducesResponseType(500, Type = typeof(InternalServerError))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        PatientDto result = await _patientService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Add patient
    /// </summary>
    /// <remarks>
    /// Notice that given name must have only first name and patronymic
    /// Any provided id will be ignored
    /// </remarks>
    /// <returns>Id of the new patient</returns>
    /// <responce code="200"></responce>
    /// <responce code="400">Bad request</responce>
    /// <responce code="500">Internal server error</responce>
    [ProducesResponseType(200, Type = typeof(Guid))]
    [ProducesResponseType(400, Type = typeof(BadRequestError))]
    [ProducesResponseType(500, Type = typeof(InternalServerError))]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PatientDto dto)
    {
        Guid createdId = await _patientService.CreateAsync(dto);
        return Ok(createdId);
    }

    /// <summary>
    /// Update patient by id
    /// </summary>
    /// <remarks>
    /// Notice that given name must have only first name and patronymic
    /// Any provided id will be ignored
    /// </remarks>
    /// <responce code="200"></responce>
    /// <responce code="404">Not found</responce>
    /// <responce code="500">Internal server error</responce>
    [ProducesResponseType(200, Type = typeof(void))]
    [ProducesResponseType(404, Type = typeof(NotFoundError))]
    [ProducesResponseType(500, Type = typeof(InternalServerError))]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PatientDto dto)
    {
        await _patientService.UpdateAsync(id, dto);
        return Ok();
    }

    /// <summary>
    /// Delete patient by id
    /// </summary>
    /// <responce code="200"></responce>
    /// <responce code="404">Not found</responce>
    /// <responce code="500">Internal server error</responce>
    [ProducesResponseType(200, Type = typeof(void))]
    [ProducesResponseType(404, Type = typeof(NotFoundError))]
    [ProducesResponseType(500, Type = typeof(InternalServerError))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);
        return Ok();
    }

    /// <summary>
    /// Get patient by birth date according to FHIR spec
    /// </summary>
    /// <responce code="200"></responce>
    /// <responce code="400">Bad request</responce>
    /// <responce code="500">Internal server error</responce>
    [ProducesResponseType(200, Type = typeof(PatientDto[]))]
    [ProducesResponseType(400, Type = typeof(BadRequestError))]
    [ProducesResponseType(500, Type = typeof(InternalServerError))]
    [HttpGet("fhir")]
    public async Task<IActionResult> GetByDate([FromQuery(Name = "birthdate")] List<string>? birthDates)

    {
        var result = await _patientService.SearchPatientsByBirthDate(birthDates);
        return Ok(result);
    }
}
