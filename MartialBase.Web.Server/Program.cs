// <copyright file="Program.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Server
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using Blazored.Modal;

using MartialBase.Web.Data;
using MartialBase.Web.Data.Services;
using MartialBase.Web.Data.Services.Interfaces;
using MartialBase.Web.Data.Utilities;
using MartialBase.Web.MockData.Services;

using Toolbelt.Blazor.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#if DEBUG
var configuration = Configurations.GetConfigurationFromAssembly("appsettings.Development.json");

builder.Configuration.AddConfiguration(configuration);
#endif

builder.Services.AddRazorPages();

builder.Services
    .AddControllersWithViews();

builder.Services
    .AddServerSideBlazor();

// This is a NuGet package to assist with injecting <Title> elements to server pre-rendered pages.
// See here >> https://dev.to/j_sakamoto/yet-another-way-to-changing-the-page-title-in-blazor-and-more-43k
builder.Services.AddHeadElementHelper();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<AppData>();

#if RELEASE
builder.Services.AddScoped<IAuthDataService, AuthDataService>();
builder.Services.AddScoped<IAuthTokensService, AuthTokensService>();
builder.Services.AddScoped<ICountriesDataService, CountriesDataService>();
builder.Services.AddScoped<IMartialBaseUserDataService, MartialBaseUserDataService>();
builder.Services.AddScoped<IOrganisationsDataService, OrganisationsDataService>();
builder.Services.AddScoped<IPeopleDataService, PeopleDataService>();
builder.Services.AddScoped<ISchoolsDataService, SchoolsDataService>();
#else
builder.Services.AddScoped<IAuthDataService, AuthDataServiceMock>();
builder.Services.AddScoped<IAuthTokensService, AuthTokensServiceMock>();
builder.Services.AddScoped<ICountriesDataService, CountriesDataServiceMock>();
builder.Services.AddScoped<IMartialBaseUserDataService, MartialBaseUserDataServiceMock>();
builder.Services.AddScoped<IOrganisationsDataService, OrganisationsDataServiceMock>();
builder.Services.AddScoped<IPeopleDataService, PeopleDataServiceMock>();
builder.Services.AddScoped<ISchoolsDataService, SchoolsDataServiceMock>();
#endif

builder.Services.AddScoped<ILocalizerService, LocalizerService>();

builder.Services.AddOptions();

builder.Services.AddBlazoredModal();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

#if RELEASE
app.UseAuthentication();
#endif
app.UseAuthorization();

app.UseEndpoints(
    endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
    });

app.Run();