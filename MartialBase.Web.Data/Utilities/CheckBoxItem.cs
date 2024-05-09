// <copyright file="CheckBoxItem.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

namespace MartialBase.Web.Data.Utilities
{
    public class CheckBoxItem<T>
    {
        public string Id { get; set; }

        public string DisplayValue { get; set; }

        public bool Checked { get; set; }

        public T Object { get; set; }
    }
}