using System.Net.Http.Json;

const string SerilogEndpoint = "https://localhost:7125/log";
const string SerilogEndpoint2 = "https://localhost:7104/log";
const string NlogEndpoint = "https://localhost:7193/log";
const string NlogEndpoint2 = "https://localhost:7015/log";

var endpoints = new[] { SerilogEndpoint, SerilogEndpoint, SerilogEndpoint2, SerilogEndpoint2, SerilogEndpoint, SerilogEndpoint, SerilogEndpoint2, SerilogEndpoint2 };

var workers = Enumerable.Range(0, 50);
ParallelOptions parallelOptions = new()
{
    MaxDegreeOfParallelism = 4
};

using HttpClient client = new()
{
};

await Parallel.ForEachAsync(endpoints, parallelOptions, async(url, token) =>
{
    var result = await client.GetFromJsonAsync<dynamic>(url, token);
    Console.WriteLine($"{url}: {result}");
});
Console.WriteLine("End");
Console.ReadKey();