using System.Text;
using ApiClient.api;
using Data;
using Data.dao.authentication;
using Data.dao.movies;
using Data.dao.reviews;
using Data.dao.wishList;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.authentication;
using Services.movie;
using Services.reviews;
using Services.wishlist;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure()));

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IWIshListsService, WishListsService>();
builder.Services.AddScoped<IReviewsService, ReviewsService>();
builder.Services.AddMemoryCache();


// DAOs
builder.Services.AddScoped<IAuthDao, AuthDao>();
builder.Services.AddScoped<IWishListsDao, WishListsDao>();
builder.Services.AddScoped<IMoviesDao, MoviesDao>();
builder.Services.AddScoped<IReviewsDao, ReviewsDao>();

// HttpClients
builder.Services.AddScoped<IMoviesClient, MoviesHttpClient>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!))
    };
});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() {Title = "WebApi", Version = "v1"});
    c.AddSecurityDefinition("Bearer", new() {
        Description = "Please issue Bearer token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new() {
        {
            new() {
                Reference = new() {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(policyBuilder => policyBuilder.AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();