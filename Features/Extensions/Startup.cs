using System.ComponentModel;
using System.Diagnostics;
using MeuProjetoApi.Features.Extensions;
using MeuProjetoApi.Features.Produtos.Repositories;
using MeuProjetoApi.Features.Produtos.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Scalar.AspNetCore;

namespace MeuProjetoApi.Features;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseConnection(configuration);
        services.AddControllers();
        services.AddOpenApi();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
    }

    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.Lifetime.ApplicationStarted.Register(() => OpenDocumentationInBrowser(app));
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();
        app.MapControllers();
    }

    private static void OpenDocumentationInBrowser(WebApplication app)
    {
        var addresses = app.Services.GetRequiredService<IServer>()
            .Features.Get<IServerAddressesFeature>()?.Addresses;
        var baseAddress = addresses?.FirstOrDefault(
                              address => address.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                          ?? addresses?.FirstOrDefault(
                              address => address.StartsWith("https://", StringComparison.OrdinalIgnoreCase));

        if (baseAddress is null)
        {
            app.Logger.LogWarning("Não foi possível determinar o endereço para abrir a documentação da API.");
            return;
        }

        var documentationUrl = $"{baseAddress.TrimEnd('/')}/scalar/v1";

        try
        {
            Process.Start(new ProcessStartInfo(documentationUrl)
            {
                UseShellExecute = true
            });
        }
        catch (Win32Exception exception)
        {
            app.Logger.LogWarning(
                exception,
                "Não foi possível abrir o navegador automaticamente. Acesse {DocumentationUrl}.",
                documentationUrl);
        }
    }
}
