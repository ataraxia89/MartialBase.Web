// <copyright file="ConfirmationYesNoBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace MartialBase.Web.App.Components.Common
{
    public class ConfirmationYesNoBase : BlazoredModalBase
    {
        [Parameter]
        public string ConfirmationMessage { get; set; }

        public void Yes()
        {
            BlazoredModal.Close(ModalResult.Ok(true));
        }

        public void No()
        {
            BlazoredModal.Close(ModalResult.Cancel());
        }
    }
}