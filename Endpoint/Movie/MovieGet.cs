using Microsoft.AspNetCore.Mvc;
using MongoAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAPI.Endpoint.Movie;

public class MovieGet {
    public static string Route => "/movie";
    public static string[] Method => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    public Delegate Handle2 => Action2;
    public IResult Action2() {
        return Results.Ok();
    }
    public static async Task<IResult> Action(DataBaseContext db, [FromQuery] string title = "", [FromQuery] int year = 0) {
        if (string.IsNullOrEmpty(title) && year == 0)
            return Results.BadRequest("Passar ao menos 1 parâmetro: title ou year");

        var col = db.GetDatabase().GetCollection<BsonDocument>("movies");

        //Exemplo
        //var filter = Builders<BsonDocument>.Filter.Eq("year", 1914);
        //var studentDocument = col.Find(filter).ToList();
        //studentDocument.GetHashCode();

        //Exemplo
        //var filter = Builders<BsonDocument>.Filter.Eq("title", "Gertie the Dinosaur");
        //var studentDocument = col.Find(filter).ToList();
        //studentDocument.GetHashCode();

        //Exemplo
        //var filter = Builders<BsonDocument>.Filter.Regex("title", new BsonRegularExpression(".*neVer.*", "i" ));
        //var studentDocument = col.Find(filter).ToList();
        //studentDocument.GetHashCode();

        //Exemplo
        //var filter = Builders<BsonDocument>.Filter.Regex("plot", "/Based/");
        //var studentDocument = col.Find(filter).CountDocuments();
        //studentDocument.GetHashCode();

        //Exemplo
        //var filter = Builders<BsonDocument>.Filter.Regex("plot", "/Based/");
        //var studentDocument = col.Find(filter).ToEnumerable().FirstOrDefault();
        //studentDocument.GetHashCode();

        //Exemplo
        //var filter = Builders<BsonDocument>.Filter.Regex("plot", "/Based/");
        //var movies = col.Find(filter).ToEnumerable().Select(x => new {
        //    Id = x.GetElement("_id").Value.ToString(),
        //    Title = x.GetElement("title").Value.ToString()
        //});

        //Exemplo
        //var f0 = Builders<BsonDocument>.Filter.Empty & Builders<BsonDocument>.Filter.Empty;
        //if (true)
        //    f0 = f0 & Builders<BsonDocument>.Filter.Regex("title", "/the/");
        //f0 = f0 & Builders<BsonDocument>.Filter.Regex("plot", "/Based/");
        //var movies = col.Find(f0).ToEnumerable().Select(x => new {
        //    Id = x.GetElement("_id").Value.ToString(),
        //    Title = x.GetElement("title").Value.ToString(),
        //    Plot = x.GetElement("plot").Value.ToString()
        //});
        //var ret = new { Count = movies.Count(), movies };
        //return Results.Ok(ret);

        var builder = Builders<BsonDocument>.Filter;
        var filter = builder.Empty & builder.Empty;

        if (!string.IsNullOrEmpty(title))
            filter = filter & builder.Regex("title", @"/" + title + @"/");
        if (year != 0)
            filter = filter & builder.Eq("year", year);

        //Exemplo síncrono
        //var movies = col.Find(filter).ToEnumerable().Select(x => new {
        //    Id = x.GetElement("_id").Value.ToString(),
        //    Title = x.GetElement("title").Value.ToString(),
        //    Year = x.GetElement("year").Value.ToString()
        //});

        //var ret = new { Count = movies.Count(), movies };
        //return Results.Ok(ret);
        //var list = await col.Find(filter);

        var result = await (await col.FindAsync(filter)).ToListAsync();

        var movies = result.Select(x => new {
            Id = x.GetElement("_id").Value.ToString(),
            Title = x.GetElement("title").Value.ToString(),
            Year = x.GetElement("year").Value.ToString()
        });

        var ret = new { Count = movies.Count(), movies };
        return Results.Ok(ret);
    }
}