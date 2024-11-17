using Microsoft.AspNetCore.Mvc;
using MongoAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAPI.Endpoint.Product {
    public class ProductDelete {
        public static string Route => "/product";
        public static string[] Method => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action(DataBaseContext db, [FromQuery] int id = 0) {
            if (id == 0)
                return Results.BadRequest("Parâmetro obrigatório: id");

            var col = db.GetDatabase().GetCollection<BsonDocument>("Product");

            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);

            await col.DeleteOneAsync(filter);

            return Results.Ok();
        }
    }
}