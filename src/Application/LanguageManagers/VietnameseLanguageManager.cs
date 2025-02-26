using System.Globalization;
using FluentValidation.Resources;

namespace Application.LanguageManagers;
internal class VietnameseLanguageManager : LanguageManager
{
    public VietnameseLanguageManager()
    {
        Culture = new CultureInfo("vi");
    }
}
