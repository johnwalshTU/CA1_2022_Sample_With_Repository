using CA1_2022_Sample_With_Repo.Repository;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//--- Dependency Injection
//Lets say you were using the repository Pattern for you DB 
//First have a look at MatchesController code
//On the on MatchesController  constructor I added a IMatchRepo parameter (so the Repo could be injected into the controller)
//When we use dependency injection in the contoller, MVC needs to know what to pass in for any interface parameter it sees!
//(i.e the constructor has a IMatchRepo parameter and we want to tell MVC to pass in a  MockMatchRepo object )
//The following line tell MVC that when it sees IMatchRepo it should create a MockMatchRepo class and pass that 
builder.Services.AddScoped<IMatchRepo, MockMatchRepo>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
