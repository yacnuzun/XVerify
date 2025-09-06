using Application.Services;
using WebUI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<XmlProcessingService>();
builder.Services.AddScoped<XsdValidatorService>();
builder.Services.AddScoped<SchematronValidatorService>();
builder.Services.AddScoped<XsltTransformerService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
