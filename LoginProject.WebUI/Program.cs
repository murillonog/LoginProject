using LoginProject.Domain.Interfaces;
using LoginProject.Infra.IoC;
using LoginProject.WebUI.Mappings;
using LoginProject.WebUI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddAutoMapper(typeof(DtoToViewModelMappingProfile));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ErrorHandlingMiddleware>();

SeedUserRoles(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

static void SeedUserRoles(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.CreateScope();
    var seed = serviceScope.ServiceProvider
                           .GetService<ISeedUserRoleInitial>();
    seed?.SeedRoles();
    seed?.SeedUsers();
}
