using Microsoft.EntityFrameworkCore;
using Ostatnie_punktowe.Models;
using OstatniePunktowe.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Doctors.Any())
    {
        db.Doctors.AddRange(
            new Doctor { FirstName = "Maja", LastName = "Wyspa", Email = "maja.wyspa@buzi.wp"},
            new Doctor { FirstName = "Andrzej", LastName = "Jak Ci na imie?"},
            new Doctor {FirstName = "Bogdan", LastName = "Kurty Ci nie oddam?", Email = "bogdan.kurty.ci@buzi.wp"}
            );
    }

    if (!db.Medicaments.Any())
    {
        db.Medicaments.AddRange(
            new Medicament {Name = "Na Bol Dupy", Type = "Masc", Description = "Uzyc w przypadku gdy somsiad ma lepiej"},
            new Medicament {Name = "Wyciag z paly", Type = "Sprej", Description = "Jak walisz mulem"}
                );
    }
    
    db.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();