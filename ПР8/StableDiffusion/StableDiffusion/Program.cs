using Microsoft.AspNetCore.Builder;
using StableDiffusion.Pages;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapPost("/api/draw", async (context) =>
{
    var request = context.Request;
    Console.WriteLine("������ �������");
    // �������� �������� ������ json
    var jsonRequest = await request.ReadFromJsonAsync<DrawRequestModel>();
    // # ���������� ��� ������ �� python ������
    // �������������� http ������
    var httpClient = new HttpClient();
    // ������������ Json
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    var json = JsonSerializer.Serialize(jsonRequest, options);
    Console.WriteLine(json.ToString());
    // ����������� � ������
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    // url �������
    var ip = "http://e27f-35-221-61-145.ngrok-free.app";
    var url = $"{ip}/prompt-json";
    // ��� ������
    var python_response = await httpClient.PostAsync(url, content);
    // �������� �����������
    var imageBytes = await python_response.Content.ReadAsByteArrayAsync();
    var imagePath = "./image.png";
    // ��������� �����������
    await File.WriteAllBytesAsync(imagePath, imageBytes);
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();