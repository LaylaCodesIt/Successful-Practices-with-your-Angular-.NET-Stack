using Azure;
using TourOfPonies.Api.Models;

namespace TourOfPonies.Api.Data;

internal class PonyService
{
	private readonly TableStorageContext _tableStorageContext;
	public PonyService(TableStorageContext ctx)
	{
		_tableStorageContext = ctx;

	}

	//get all

	public async Task<List<Pony>> GetAll()
	{
		AsyncPageable<PonyEntity> ponyEntities = await _tableStorageContext.GetAllPoniesAsync();

		if (ponyEntities is null)
			return null;

		List<Pony> ponies = new List<Pony>();

		await foreach (PonyEntity pe in ponyEntities)
		{
			ponies.Add(new Pony(pe));
		}

		return ponies;
	}


	// get by id

	public async Task<Pony> Get(string id)
	{
		PonyEntity pe = await _tableStorageContext.GetPonyByIdAsync(id);
		if (pe is null)
			return null;
		return new Pony(pe);
	}

	// get avatar by id

	public async Task<string> GetAvatarById(string id)
	{
		Pony pony = await Get(id);

		return pony is not null ? pony.LargeAvatar : null;
	}

	public async Task<List<Pony>> GetAllHeroes()
	{
		var ponies = await GetAll();
		return ponies.Where(pony => pony.IsHero is true).ToList();
	}

	// get by name - wildcard

	public async Task<List<Pony>> GetByPartialName(string partialName)
	{
		List<Pony> ponies = await GetAll();

		//return ponies.Where(Pony => Pony.Name.ToLower().Contains(partialName.ToLower()))
		//		.Select(Pony => Pony.Name)
		//		.FirstOrDefault() ?? string.Empty;

		return ponies.Where(Pony => Pony.Name.ToLower().Contains(partialName.ToLower()))
				.ToList() ?? new List<Pony>();
	}

	// add/update pony
	public async Task<string> AddOrUpdatePony(Pony pony)
	{
		var ponyEntity = await _tableStorageContext.InsertOrMergeEntityAsync(new PonyEntity(pony));
		return ponyEntity.rowKey;
	}

	//delete pony

	public async Task<bool> DeletePony(string id)
	{
		return await _tableStorageContext.DeletePonyAsync(id);
	}




}
