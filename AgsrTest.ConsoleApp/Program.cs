using System.Net.Http.Json;

var client = new HttpClient();
client.BaseAddress = new Uri("http://agsr_test_api:8080");

var random = new Random();

for (int i = 0; i < 100; i++)
{
    var patient = new PatientDto()
    {
        Name = new NameDto
        {
            Use = "official",
            Family = $"Family{i}",
            Given = new List<string> { $"Name{i}", $"Patronymic{i}" }
        },
        Gender = i % 2 == 0 ? "male" : "female",
        BirthDate = GetRandomDate(new DateTime(2000, 1, 1), new DateTime(2025, 4, 1)),
        Active = true
    };

    try
    {
        var response = await client.PostAsJsonAsync("/patient", patient);
        if (response.IsSuccessStatusCode)
        {
            var id = await response.Content.ReadFromJsonAsync<Guid>();
            Console.WriteLine($"Created patient with id: {id}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(errorContent);
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Request failed: {ex.Message}");
    }
}

DateTime GetRandomDate(DateTime start, DateTime end)
{
    var range = (end - start).Days;
    var randomDay = random.Next(range);
    var randomTime = TimeSpan.FromHours(random.Next(0, 24)) + TimeSpan.FromMinutes(random.Next(0, 60)) + TimeSpan.FromSeconds(random.Next(0, 60));
    return start.AddDays(randomDay).Add(randomTime);
}
public class NameDto
{
    public Guid Id { get; set; }
    public string Use { get; set; }
    public string Family { get; set; }
    public List<string> Given { get; set; }
}

public class PatientDto
{
    public NameDto Name { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}