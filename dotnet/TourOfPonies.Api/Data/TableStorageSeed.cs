using System.Text.Json;
using TourOfPonies.Api.Models;

namespace TourOfPonies.Api.Data;

internal class TableStorageSeed
{
    private readonly TableStorageContext _context;
    private readonly ILogger<TableStorageSeed> _log;
	private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _jsonData;

	public TableStorageSeed(ILogger<TableStorageSeed> log, TableStorageContext context)
    {
        _log = log;
        _context = context;
        
        string jsonFilePath = @"../TourOfPonies.Api/Data/PonyData.json";
        _jsonData = File.ReadAllText(jsonFilePath);;
	
	}
    private class PonyListWrapper
    {
        public List<Pony> Ponies { get; set; }
    }
    internal async Task Seed()
    {
        bool isSuccessful = false;
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        PonyListWrapper ponyList = JsonSerializer.Deserialize<PonyListWrapper>(_jsonData,jsonOptions);

        if (ponyList?.Ponies.Count > 0)
        {
            foreach (var pny in ponyList.Ponies)
            {
                PonyEntity pony = new(pny);
                // allow the context to create row key
                pony.RowKey = null;

				if (!string.IsNullOrEmpty(pny.LargeAvatar))
                    pony.IsHero = true;

                var ponyId = await _context.InsertOrMergeEntityAsync(pony);
                _log.LogInformation($"Pony {pny.Name} with ID: {ponyId} was created");
            }
        }

            }

    
}
