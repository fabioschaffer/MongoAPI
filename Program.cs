using MongoAPI.Endpoint.Movie;
using MongoAPI.Endpoint.Product;
using MongoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DataBaseContext>(); 

var app = builder.Build();

app.MapMethods(MovieGet.Route, MovieGet.Method, MovieGet.Handle);
app.MapMethods(ProductPost.Route, ProductPost.Method, ProductPost.Handle);
app.MapMethods(ProductGet.Route, ProductGet.Method, ProductGet.Handle);
app.MapMethods(ProductPut.Route, ProductPut.Method, ProductPut.Handle);
app.MapMethods(ProductDelete.Route, ProductDelete.Method, ProductDelete.Handle);

app.Run();