using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using NotesApp;
using NotesApp.Data;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding Serilog to write logs to ElasticSearch cluster
builder.Host.UseSerilog((hostBuilder, loggerConfiguration) =>
{
    var elasticsearchSettings = hostBuilder.Configuration.GetSection(nameof(ElasticsearchSettings)).Get<ElasticsearchSettings>();

    var envName = builder.Environment.EnvironmentName.ToLower().Replace(".", "-");
    var appName = "notes-app";
    var templateName = "notes-template";

    loggerConfiguration
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchSettings.Url))
        {
            IndexFormat = $"{appName}-{envName}-{DateTimeOffset.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            OverwriteTemplate = true,
            TemplateName = templateName,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            TypeName = null,
            BatchAction = ElasticOpType.Create
        });
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Configuring DbContext to use PostgreSQL database
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("NotesApp")));

builder.Services.AddScoped<AppDbContext>();

//Injecting service for pages to work with DbContext
builder.Services.AddScoped<INotesService, NotesService>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();

entities.Database.EnsureCreated();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
