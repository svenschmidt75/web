using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TeacherDbContext>(option => {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseMySQL(connectionString);
});

// Add services to the container.
builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();