using System.Linq;

namespace Fdo.Contato.Vistoria.UITest.Extensions
{
    public static class StringExtensions
    {
        public static string EnsureMaxLength(this string value, int length)
        {
            return string.Join(string.Empty, value.Take(length));
        }
    }
}
