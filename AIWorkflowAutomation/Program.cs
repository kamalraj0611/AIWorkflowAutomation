using AIWorkflowAutomation.Services; // Ensure this namespace is correct
using Microsoft.AspNetCore.Builder; // Ensure necessary namespaces are included
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add support for controllers

// Register your custom services
builder.Services.AddSingleton<TicketService>();  // Add the TicketService as a singleton
builder.Services.AddSingleton<ChatGPTService>(); // Add the ChatGPTService as a singleton
builder.Services.AddRazorPages(); // Add support for Razor Pages

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Developer exception page for debugging
}
else
{
    app.UseExceptionHandler("/Error"); // Handle errors gracefully in production
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Map API controllers
app.MapRazorPages();  // Map Razor Pages

app.Run(); // Run the application
