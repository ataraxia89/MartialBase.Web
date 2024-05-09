// <copyright file="OrganisationsPageBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using MartialBase.API.Models.DTOs.Organisations;
using MartialBase.Web.Data.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Pages.Organisations;

public class OrganisationsPageBase : MartialBasePageBase
{
    [BindProperty]
    public List<OrganisationDTO> Organisations { get; set; }

    [Inject]
    public IAuthTokensService AuthTokensService { get; set; }

    [Inject]
    public IOrganisationsDataService OrganisationsDataService { get; set; }

    public async Task LoadOrganisationsAsync()
    {
        var authToken = await AuthTokensService.GetToken();
        var getOrganisationsResult = await OrganisationsDataService.GetOrganisations(authToken);

        if (getOrganisationsResult.IsSuccess)
        {
            Organisations = getOrganisationsResult.Object;
            LoadingMessage = null;
        }
        else
        {
            ErrorMessage = $"{Localizer["FailedToLoadOrganisations"]} {Localizer[getOrganisationsResult.ErrorResponseCode.ToString()]}";
            LoadingMessage = null;
        }
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        HideConfirmationMessage();
        await LoadOrganisationsAsync();
    }
}