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
builder.Services.AddSingleton<StatisticsService>();
builder.Services.AddSingleton<NotificationCenter>();

var owner = builder.Configuration.GetSection("MySettings:Github:owner").Value;
var credentials = builder.Configuration.GetSection("MySettings:Github:secret").Value;

var githubClient = new GitHubClient(new ProductHeaderValue("owner"));
githubClient.Credentials = new Credentials(credentials);

builder.Services.AddSingleton(githubClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGitHubWebhooks();
});

app.Run();
