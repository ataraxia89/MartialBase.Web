// <copyright file="Arts.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.DataGenerator
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.Collections.Generic;

using MartialBase.Web.MockData.Tools;

namespace MartialBase.Web.MockData.DataGenerators
{
    public static class Arts
    {
        private static readonly List<string> ArtNames = new()
        {
            "Aikido",
            "Bojutsu",
            "Boxing",
            "Brazilian Jiu-Jitsu",
            "Capoeira",
            "Haidong Gumdo",
            "Hapkido",
            "Hwa Rang Do",
            "Iaido",
            "Jeet Kune Do",
            "Judo",
            "Jujutsu",
            "Karate",
            "Kendo",
            "Kenjutsu",
            "Kenpo",
            "Kickboxing",
            "Krav Maga",
            "Kung Fu",
            "MMA",
            "Muay Thai",
            "Ninjutsu",
            "Sumo",
            "Taekkyeon",
            "Taekwon-Do",
            "Tai Chi",
            "Tang Soo Do",
            "Wing Chun",
            "Won Hwa Do",
            "Wushu"
        };
        
        public static string GetRandomArtName()
        {
            return ArtNames[RandomData.GetRandomNumber(0, ArtNames.Count - 1)];
        }
    }
}