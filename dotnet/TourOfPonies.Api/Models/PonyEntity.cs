using System.Runtime.CompilerServices;
using System.Text.Json;
using Azure;
using Azure.Data.Tables;

namespace TourOfPonies.Api.Models;

internal class PonyEntity : ITableEntity
{
    public PonyEntity()
    {
    }

    public PonyEntity(Pony pny)
    {
        RowKey = pny.Id.ToString();
        Name = pny.Name;
        Alias = pny.Alias;
        LargeAvatar = pny.LargeAvatar;
        Url = pny.Url;
        Sex = pny.Sex;
        Residence = pny.Residence;
        Occupation = pny.Occupation;
        Kind = string.Join(", ", pny.Kind);
        Images = string.Join(", ", pny.Images);
        IsHero = pny.IsHero;
    }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string Name { get; set; }
    public string LargeAvatar { get; set; }
    public string Alias { get; set; }
    public string Url { get; set; }
    public string Sex { get; set; }
    public string Residence { get; set; }
    public string Occupation { get; set; }
    public string Kind { get; set; }
    public string Images { get; set; }
    public bool IsHero { get; set; }

}