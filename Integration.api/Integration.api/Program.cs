using AutoRepairPro.Business.Service.Implementation;
using AutoRepairPro.Data.Repositories.Interfaces;
using Integration.business.Helpers;
using Integration.business.Services.Implementation;
using Integration.business.Services.Interfaces;
using Integration.data.Data;
using Integration.data.Models;
using Integration.data.Repositories.Implementation;
using Integration.data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("IntegrationDb")));


var AppDbContext =  builder.Services.BuildServiceProvider().GetRequiredService<AppDbContext>();

var SelectedFromDbContext = await AppDbContext.fromDbs.FirstOrDefaultAsync(c => c.IsSelected);
var SelectedToDbContext = await AppDbContext.toDbs.FirstOrDefaultAsync(c => c.IsSelected);


if (SelectedFromDbContext != null)
    builder.Services.AddDbContext<FromDbContext>(options =>
           options.UseSqlServer(SelectedFromDbContext.ConnectionString));

if (SelectedToDbContext != null)
    builder.Services.AddDbContext<ToDbContext>(options =>
            options.UseMySQL(SelectedToDbContext.ConnectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IFromDatabase,FromDatabase>();
builder.Services.AddScoped<IToDataBase, ToDatabase>();
builder.Services.AddScoped<IAuthServices, AuthServices>();




builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = true;
})
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();


var _JWT = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWT>>().Value;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = _JWT.Issuer,
        ValidAudience = _JWT.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();
using (var Scope = app.Services.CreateScope())
{
    var UserManger = Scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var RoleManger = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await new SeedInitialData(RoleManger, UserManger).SeedRoles();
    await new SeedAdminData(RoleManger, UserManger).SeedAdmin();
    await new SeedAdminData(RoleManger, UserManger).SeedAllRolesToAdmin();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
