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
    string TranslationFile = builder.HostEnvironment.BaseAddress + "translate.json";
    return new eTranslate4CSharp(TranslationFile, "en-US", provider.GetService<HttpClient>());
});

await builder.Build().RunAsync();
