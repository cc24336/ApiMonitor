using System.Threading;
using Microsoft.EntityFrameworkCore;
using MonitorApi;

// Cria um WebApplicationBuilder, que é usado para //configurar a aplicação
var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<MonitorDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte para endpoints da API
builder.Services.AddEndpointsApiExplorer();

// app é o objeto que representa a API
var app = builder.Build();

app.MapGet("/monitor", async (MonitorDbContext db) =>
{
    return await db.MonitorTabela.ToListAsync();
}
);

// listar um registro pelo apelido
app.MapGet("/monitor/{apelido}", async (string apelido, MonitorDbContext db) =>
{
    var monitor = await db.MonitorTabela
        .Include(m => m.Horarios)
        .FirstOrDefaultAsync(m => m.Apelido == apelido); // FindAsync não pois apelido não é chave primaria

    return monitor is not null ? Results.Ok(monitor) : Results.NotFound("Monitor não encontrado");
}
);

app.MapGet("/horario", async (MonitorDbContext db) =>
{

    return await db.HorarioTabela.ToListAsync();
}
);

app.MapGet("/horario/{id}", async (int id, MonitorDbContext db) =>
{
    var horario = await db.HorarioTabela
        .FirstOrDefaultAsync(h => h.IdMonitor == id);

    return horario is not null ? Results.Ok(horario) : Results.NotFound("Não encontrado");
});

app.MapPost("/monitor", async (MonitorApi.Monitor m, MonitorDbContext db) =>
{
    db.MonitorTabela.Add(m);
    await db.SaveChangesAsync();
}
);

app.MapPost("/horario", async (MonitorApi.Horario h, MonitorDbContext db) =>
{
    db.HorarioTabela.Add(h);
    await db.SaveChangesAsync();
});


app.MapPut("/monitor/{id}", async (int id, MonitorApi.Monitor monitor, MonitorDbContext db) => {
    var monitorIdM = await db.MonitorTabela.FindAsync(id);
    if (monitorIdM is null) {
        return Results.NotFound();
    } else {
        monitorIdM.Nome = monitor.Nome;
        monitorIdM.RA = monitor.RA;
        monitorIdM.Apelido = monitor.Apelido;

        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.MapPut("/horario/{id}", async(int id, Horario horario, MonitorDbContext db) => {
    var horarioId = await db.HorarioTabela.FindAsync(id);
    if (horarioId is null) {
        return Results.NotFound();
    } else {
        horarioId.DiaSemana = horario.DiaSemana;
        horarioId.HorarioAtendimento = horario.HorarioAtendimento;

        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});


app.MapDelete("/monitor/{id}", async (int id, MonitorDbContext db) => {

    var monitorDelete = await db.MonitorTabela
            .Include(h => h.Horarios)
            .FirstOrDefaultAsync(h => h.IdMonitor == id);

    if (monitorDelete is null)
    {
        return Results.NotFound();
    }
    else
    {
        if (monitorDelete.Horarios.Any()) return Results.BadRequest("Não foi possivel excluir monitores com horarios");

        db.MonitorTabela.Remove(monitorDelete);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.MapDelete("/horario/{id}", async (int id, MonitorDbContext db) =>
{
    var horarioDelete = await db.HorarioTabela.FindAsync(id);
    if (horarioDelete == null)
    {
        return Results.NotFound();
    }
    else
    {
        db.HorarioTabela.Remove(horarioDelete);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

});

app.Run();