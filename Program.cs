using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = CoconaApp.CreateBuilder();

builder.Logging.AddDebug();

builder.Services.AddLogging();

var app = builder.Build();

app.AddCommand("qif", (string input) =>
{
    MainApp.Run(input, new QifOutputFormat());
});

app.AddCommand("csv", (string input) =>
{
    MainApp.Run(input, new CsvOutputFormat());
});

app.Run();