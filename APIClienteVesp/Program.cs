using APIClienteVesp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<ClienteDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte para endpoints da API
builder.Services.AddEndpointsApiExplorer();

// app é o objeto que representa a API
var app = builder.Build();

app.MapGet("/clientes", async (ClienteDbContext db) =>
    await db.ClienteTabela.ToListAsync()
    
);

// listar um registro pelo id
app.MapGet("/clientes/{id}", async(int id, ClienteDbContext db) =>
    await db.ClienteTabela.FindAsync(id) is Cliente cliente ? Results.Ok(cliente) : Results.NotFound("O id não foi encotrado!")
);

app.MapPost("/clientes", async(Cliente c,  ClienteDbContext db) => {
    db.ClienteTabela.Add(c);
    await db.SaveChangesAsync();
}
);

app.MapPut("/clientes/{id}", async(int id, Cliente cn, ClienteDbContext db) => {
    var c = await db.ClienteTabela.FindAsync(id);
    if (c is null) {
        return Results.NotFound();
    } else {
        c.Nome = cn.Nome;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.MapDelete("/clientes/{id}", async(int id, ClienteDbContext db) => {
    var c = await db.ClienteTabela.FindAsync(id);
    if (c is null) {
        return Results.NotFound();
    } else {
        db.ClienteTabela.Remove(c);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.Run();