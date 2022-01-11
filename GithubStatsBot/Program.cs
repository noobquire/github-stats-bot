using GithubStatsBot;
using GithubStatsBot.Services;
using Octokit;
using Octokit.Webhooks;
using Octokit.Webhooks.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebhookEventProcessor, IssuesEventProcessor>();
builder.Services.AddScoped<StatisticsService>();
builder.Services.AddScoped<NotificationCenter>();
var githubClient = new GitHubClient(new ProductHeaderValue("noobquire"));
githubClient.Credentials = new Credentials("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");
builder.Services.AddSingleton(githubClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGitHubWebhooks();
});

app.Run();
