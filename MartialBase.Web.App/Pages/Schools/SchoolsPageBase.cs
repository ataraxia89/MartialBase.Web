// <copyright file="SchoolsPageBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;

using MartialBase.API.Models.DTOs.Schools;
using MartialBase.Web.Data.Services.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Pages.Schools;

public class SchoolsPageBase : MartialBasePageBase
{
    [BindProperty]
    public List<SchoolDTO> Schools { get; set; }

    [Inject]
    public IAuthTokensService AuthTokensService { get; set; }

    [Inject]
    public ISchoolsDataService SchoolsDataService { get; set; }

    public async Task LoadSchoolsAsync()
    {
        var authToken = await AuthTokensService.GetToken();
        var getSchoolsResult = await SchoolsDataService.GetSchools(authToken);

        if (getSchoolsResult.IsSuccess)
        {
            Schools = getSchoolsResult.Object;
            LoadingMessage = null;
        }
        else
        {
            ErrorMessage = $"{Localizer["FailedToLoadSchools"]} {Localizer[getSchoolsResult.ErrorResponseCode.ToString()]}";
            LoadingMessage = null;
        }
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        HideConfirmationMessage();
        await LoadSchoolsAsync();
    }
}