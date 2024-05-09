// <copyright file="BlazoredModalBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace MartialBase.Web.App.Components;

public class BlazoredModalBase : ComponentBase
{
    [CascadingParameter]
    public BlazoredModalInstance BlazoredModal { get; set; }

    public void Cancel()
    {
        BlazoredModal.Close(ModalResult.Cancel());
    }
}