using Backend.Data;
using Backend.Repository;
using Backend.Repository.Interface;
using Backend.Service;
using Backend.Service.Interface;
using Backend.Service.LogicaController;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy
            .AllowAnyOrigin()    
            .AllowAnyHeader()   
            .AllowAnyMethod();  
    });
});


// Add services to the container.
builder.Services.AddDbContext<MyBuildingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Criptografia
builder.Services.AddScoped<IPasswordHasher, Argon2PAsswordHasher>();

// Services (usuário)
builder.Services.AddScoped<IVerificarEmail, VerificarEmail>();
builder.Services.AddScoped<IExisteUsuario, ExisteUsuario>();

// Repositórios (usuário)
builder.Services.AddScoped<ICreateUser, UserRepository>();
builder.Services.AddScoped<IGetbyEmail, UserRepository>();
builder.Services.AddScoped<UserRepository>();



// Lógica Auth
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthCadastro, AuthService>();
builder.Services.AddScoped<IAuthLogin, AuthService>();

// Repositórios (building)
builder.Services.AddScoped<ICreateBuilding, BuildingRepository>();
builder.Services.AddScoped<IDeleteBuilding, BuildingRepository>();
builder.Services.AddScoped<IPatchMoradores, BuildingRepository>();
builder.Services.AddScoped<IGetById, BuildingRepository>();

// Services (building)
builder.Services.AddScoped<ItransformeFileEmBytes, TransformeFileEmBytes>();
builder.Services.AddScoped<IBuildCreate, BuildingService>(); 
builder.Services.AddScoped<BuildingService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("PermitirTudo");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
