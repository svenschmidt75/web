using Microsoft.EntityFrameworkCore;
using WebAPI;
using WebAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TeacherDbContext>(option => {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseMySQL(connectionString);
});

// Add services to the container.
builder.Services.AddControllers(option => {
        // SS: if the content type doesn't match, return 406, not acceptable
        option.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }