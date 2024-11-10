using webpessoa.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddHttpClient<apiPessoaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7039/");
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pessoa}/{action=Index}/{id?}");

app.Run();
