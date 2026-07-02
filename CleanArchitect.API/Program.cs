using CleanArchitect.Application.Interfaces;
using CleanArchitect.Infrastructure.DbContexts;
using CleanArchitect.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EBikeShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EBikeShopConnection")
    )
);

builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryGetAll, CategoryGetAllService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddOpenApi(options =>
//{
//    options.AddDocumentTransformer((document, context, cancellationToken) =>
//    {
//        document.Components ??= new OpenApiComponents();
//        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
//        {
//            Type = SecuritySchemeType.Http,
//            Scheme = "bearer",
//            BearerFormat = "JWT",
//            Description = "Nhập Token JWT của bạn vào đây:"
//        });
//        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
//                },
//                Array.Empty<string>()
//            }
//        });
//        return Task.CompletedTask;
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Step 1: Cài đặt package Scalar.AspNetCore
    // Kích hoạt giao diện Scalar để test dữ liệu
    app.MapScalarApiReference(options => {
        options.WithTitle("Hệ thống API Của Tôi")
               .WithTheme(ScalarTheme.Purple);
    });

    //Step 2: Gọi theo đường dẫn này https://localhost:7007/scalar/v1
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
