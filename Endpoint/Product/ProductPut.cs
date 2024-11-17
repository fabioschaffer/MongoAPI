using Microsoft.AspNetCore.Mvc;
using MongoAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAPI.Endpoint.Product {
    public class ProductPut {
        public static string Route => "/product";
        public static string[] Method => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action(DataBaseContext db, [FromQuery] int id = 0, [FromQuery] string name = "") {
            if (id ==0 || string.IsNullOrEmpty(name) )
                return Results.BadRequest("Passar os parâmetros: id e name");

            var col = db.GetDatabase().GetCollection<BsonDocument>("Product");

            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);

            var update = Builders<BsonDocument>.Update.Set("Category", "Eletronic");

            //Atualizar multiplos campos. Tentar o método AddToSet
            //var update = Builders<BsonDocument>.Update.AddToSet("Category", 

            await col.UpdateOneAsync(filter, update);

            return Results.Ok();
        }
    }
}