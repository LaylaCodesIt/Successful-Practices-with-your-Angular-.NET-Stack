using Azure.Data.Tables;
using Azure;
using System.Security.Principal;

namespace TourOfPonies.Api.Models;

internal class Pony
{
	public Pony()
	{

	}
    public Pony(PonyEntity pe)
    {
		Id = string.IsNullOrEmpty(pe.RowKey) ? "" : pe.RowKey;
		Name = pe.Name;
		LargeAvatar = pe.LargeAvatar;
		Alias = pe.Alias;
		Url = pe.Url;
		Sex = pe.Sex;
		Residence = pe.Residence;
		Occupation = pe.Occupation;
		Kind = pe.Kind.Split(", ").ToList();
		Images = pe.Images.Split(", ").ToList();
		IsHero = pe.IsHero;
	}
    public string Id { get; set; }
	public string Name { get; set; }
	public string LargeAvatar { get; set; }
	public string Alias { get; set; }
	public string Url { get; set; }
	public string Sex { get; set; }
	public string Residence { get; set; }
	public string Occupation { get; set; }
	public List<string> Kind { get; set; } = new();
	public List<string> Images { get; set; } = new();
	public bool IsHero { get; set; }

}
