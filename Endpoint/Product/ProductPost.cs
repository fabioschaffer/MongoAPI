using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAPI.Services;
using MongoDB.Bson;
using System.Security.Claims;
namespace MongoAPI.Endpoint.Product;
public class ProductPost
{
    public static string Route => "/product";
    public static string[] Method => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(DataBaseContext db, [FromQuery] int id, [FromQuery] string name)
    {

        var col = db.GetDatabase().GetCollection<BsonDocument>("Product");

        var document = new BsonDocument
            {
                { "Id", id },
                { "Name", name }
            };

        await col.InsertOneAsync(document);

        return Results.Created($"/product/{id}", id);
    }
}