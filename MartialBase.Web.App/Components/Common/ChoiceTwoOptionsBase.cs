// <copyright file="ChoiceTwoOptionsBase.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.App
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace MartialBase.Web.App.Components.Common
{
    public class ChoiceTwoOptionsBase : BlazoredModalBase
    {
        [Parameter]
        public string ConfirmationMessage { get; set; }

        [Parameter]
        public string Option1Text { get; set; }

        [Parameter]
        public string Option2Text { get; set; }

        public void Option1()
        {
            BlazoredModal.Close(ModalResult.Ok(1));
        }

        public void Option2()
        {
            BlazoredModal.Close(ModalResult.Ok(2));
        }
    }
}