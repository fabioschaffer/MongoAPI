using Microsoft.AspNetCore.Mvc;
using MongoAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAPI.Endpoint.Product;

public class ProductGet {
    public static string Route => "/product";
    public static string[] Method => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    public static async Task<IResult> Action(DataBaseContext db, [FromQuery] string name = "") {
        var col = db.GetDatabase().GetCollection<BsonDocument>("Product");

        var builder = Builders<BsonDocument>.Filter;
        var filter = builder.Empty & builder.Empty;

        if (!string.IsNullOrEmpty(name))
            filter = filter & builder.Regex("Name", @"/" + name + @"/");

        var result = await (await col.FindAsync(filter)).ToListAsync();

        var products = result.Select(x => new {
            Id = x.GetElement("Id").Value.ToString(),
            Name = x.GetElement("Name").Value.ToString(),
            Category = x.TryGetElement("Category", out BsonElement bcategory) ? bcategory.Value.ToString() : ""
        });

        var ret = new { Count = products.Count(), products };
        return Results.Ok(ret);
    }
}