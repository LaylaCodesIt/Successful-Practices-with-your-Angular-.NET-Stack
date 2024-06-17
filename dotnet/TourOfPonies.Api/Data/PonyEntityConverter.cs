using System.Text.Json;
using System.Text.Json.Serialization;
using TourOfPonies.Api.Models;

namespace TourOfPonies.Api.Data;

//internal class PonyEntityConverter : JsonConverter<Pony>
//{
//    public override Pony Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var entity = JsonSerializer.Deserialize<PonyEntity>(ref reader, options);
//        if (entity != null && reader.TokenType == JsonTokenType.StartObject)
//        {
//            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
//            {
//                if (jsonDocument.RootElement.TryGetProperty("id", out var id))
//                {
//                    entity.RowKey = id.GetString();
//                }
//			}
//        }
//        return entity;
//    }

//    public override void Write(Utf8JsonWriter writer, PonyEntity value, JsonSerializerOptions options)
//    {
//        throw new NotSupportedException("Serialization is not supported.");
//    }
//}