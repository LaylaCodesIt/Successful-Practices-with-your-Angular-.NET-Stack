using System.Net;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using TourOfPonies.Api.Models;

namespace TourOfPonies.Api.Data;

internal class TableStorageContext
{
    private readonly StorageSettings _settings;

    private readonly ILogger _log;

    private TableServiceClient _tableServiceClient;
    private TableItem _table;
    private TableClient _tableClient;
    private string _partitionKey;



	public TableStorageContext(ILogger<TableStorageContext> logger, StorageSettings settings)
    {
        _log = logger;
        _settings = settings;
        _partitionKey = "Ponies";

	}


    internal async Task CreateTableAsync()
    {
        try
        {
            _tableServiceClient = new TableServiceClient(
                new Uri(_settings.StorageUri),
                new TableSharedKeyCredential(_settings.AccountName, _settings.StorageAccountKey));

            _table = await _tableServiceClient.CreateTableIfNotExistsAsync(_settings.TableName);

            _log.LogDebug($"The created table's name is {_table.Name}.");

            _tableClient = new TableClient(
                new Uri(_settings.StorageUri),
                _settings.TableName,
                new TableSharedKeyCredential(_settings.AccountName, _settings.StorageAccountKey));

        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message);
        }
    }
    
    public async Task<(bool responseSuccess, string rowKey)> InsertOrMergeEntityAsync(PonyEntity entity)
    {
        bool responseSuccess = false;
        string newRowKey = string.Empty;
        try
        {
            if (_table is null)
            {
                await CreateTableAsync();
            }
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
			entity.RowKey = entity.RowKey ?? Guid.NewGuid().ToString();
			entity.PartitionKey = _partitionKey;

            var response = await _tableClient.UpsertEntityAsync(entity);
    
            // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
            if (response.IsError)
            {
                _log.LogError($"Inserting new pony entity failed.");
                responseSuccess = false;
            }
            else
            {
                responseSuccess = true;
                newRowKey = entity.RowKey;

			}
    
    
        }
        catch (Exception e)
        {
            _log.LogInformation(e.Message);
            responseSuccess = false;
    
        }
        return (responseSuccess, newRowKey);
    }

    public async Task<PonyEntity> GetPonyByNameAsync(string name)
    {

        try
        {
            if (_table is null)
            {
                _log.LogInformation("table was null in GetEntity");
                await CreateTableAsync();
            }

            Pageable<PonyEntity> ponyEntities = _tableClient.Query<PonyEntity>(pny => pny.Name == name);
            
            return ponyEntities.FirstOrDefault();
        }
        catch (Exception e) 
        {
            _log.LogError(e.Message);
            return null;
        }

    }

	public async Task<PonyEntity> GetPonyByIdAsync(string id)
	{

		try
		{
			if (_table is null)
			{
				_log.LogInformation("table was null in GetEntity");
				await CreateTableAsync();
			}

			Pageable<PonyEntity> ponyEntities = _tableClient.Query<PonyEntity>(pny => pny.RowKey == id);

			return ponyEntities.FirstOrDefault();
		}
		catch (Exception e)
		{
			_log.LogError(e.Message);
			return null;
		}

	}

	public async Task<AsyncPageable<PonyEntity>> GetAllPoniesAsync()
    {

        try
        {
            if (_table is null)
            {
                _log.LogInformation("table was null in GetEntity");
                await CreateTableAsync();
            }

			AsyncPageable<PonyEntity> ponyEntities =  _tableClient.QueryAsync<PonyEntity>();
            
            return ponyEntities;
        }
        catch (Exception e) 
        {
            _log.LogError(e.Message);
            return null;
        }

    }

	public async Task<bool> DeletePonyAsync(string rowKey)
	{
        bool isSuccessful;
		try
		{
			if (_table is null)
			{
				await CreateTableAsync();
			}
			await _tableClient.DeleteEntityAsync(_partitionKey, rowKey);
            isSuccessful = true;
		}
		catch (RequestFailedException ex)
		{
			Console.WriteLine($"Error deleting entity: {ex.Message}");
            isSuccessful = false;
		}
        return isSuccessful;
	}
}