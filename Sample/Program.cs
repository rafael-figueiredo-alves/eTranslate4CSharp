using eTranslate.Interfaces;
using eTranslate;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sample;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IeTranslate>(provider =>
{
    var TranslationFile = (provider.GetService<HttpClient>()!).BaseAddress!.ToString();
    TranslationFile += "teste.json";
    return new eTranslate4CSharp(TranslationFile, "es-ES");
});

await builder.Build().RunAsync();
