﻿using System.IO;
using System.Text.RegularExpressions;

namespace Fdo.Contato.Vistoria.Extensions
{
    public static class StringExtensions
    {
        private const string PLATE_REGEX = @"[A-Z]{3}[0-9][0-9A-Z][0-9]{2}";

        public static bool IsPlate(this string value)
        {
            if (value is null)
            {
                return false;
            }
            return Regex.IsMatch(value, PLATE_REGEX);
        }

        public static string GetFileName(this string imagePath)
        {
            return Path.GetFileName(imagePath);
        }
    }
}
