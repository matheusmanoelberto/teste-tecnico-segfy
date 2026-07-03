using Microsoft.EntityFrameworkCore;
using SegfyInsurance.Api.Middlewares;
using SegfyInsurance.Application.Extensions;
using SegfyInsurance.Infrastructure.Data;
using SegfyInsurance.Infrastructure.Extensions;

var construtor = WebApplication.CreateBuilder(args);

construtor.Services.AddControllers();
construtor.Services.AddEndpointsApiExplorer();
construtor.Services.AddSwaggerGen();
construtor.Services.AddCors(opcoes =>
{
    var origensPermitidas = construtor.Configuration
        .GetSection("Cors:OrigensPermitidas")
        .Get<string[]>()?
        .Select(origem => origem.Trim().TrimEnd('/'))
        .Where(origem => !string.IsNullOrWhiteSpace(origem))
        .Distinct()
        .ToArray() ?? [];

    opcoes.AddPolicy("FrontendReact", politica =>
    {
        politica
            .WithOrigins(origensPermitidas)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
construtor.Services.AdicionarAplicacao();
construtor.Services.AdicionarInfraestrutura(construtor.Configuration);

var aplicacao = construtor.Build();

using (var escopo = aplicacao.Services.CreateScope())
{
    var banco = escopo.ServiceProvider.GetRequiredService<SegfyInsuranceDbContext>();
    banco.Database.Migrate();
}

if (aplicacao.Environment.IsDevelopment())
{
    aplicacao.UseSwagger();
    aplicacao.UseSwaggerUI();
}

aplicacao.UseMiddleware<MiddlewareExcecao>();
aplicacao.UseCors("FrontendReact");
if (!aplicacao.Environment.IsDevelopment())
{
    aplicacao.UseHttpsRedirection();
}
aplicacao.UseAuthorization();
aplicacao.MapControllers();

aplicacao.Run();
