// <copyright file="MartialBasePageBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using MartialBase.Web.Data.Services.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MartialBase.Web.App.Pages;

public class MartialBasePageBase : ComponentBase
{
    public string ErrorMessage { get; set; }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    public bool IsLoading => !string.IsNullOrEmpty(LoadingMessage);

    [BindProperty]
    public string LoadingMessage { get; set; }

    public string StatusClass { get; set; }

    public string ConfirmationMessage { get; set; }

    public bool ConfirmationMessageVisible { get; set; }

    [Inject]
    public ILocalizerService Localizer { get; set; }

    protected internal void ShowConfirmationMessage(string confirmationMessage, bool isWarning = false)
    {
        ConfirmationMessage = confirmationMessage;
        StatusClass = !isWarning ? "alert-success" : "alert-danger";
        ConfirmationMessageVisible = true;
        StateHasChanged();
    }

    protected internal void HideConfirmationMessage()
    {
        ConfirmationMessage = string.Empty;
        ConfirmationMessageVisible = false;
        StateHasChanged();
    }
}