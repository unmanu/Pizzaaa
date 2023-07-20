using MudBlazor;

namespace Pizzaaa.UI.Blazor.Data.Theme;

public class ThemeService
{
    public const string DEFAULT_THEME = "default";
    private Dictionary<string, MudTheme> _themes;

    public ThemeService()
    {
        MudTheme defaultTheme = new()
        {

            PaletteDark = new PaletteDark()
            {
            }
        };
        _themes = new()
        {
            { DEFAULT_THEME, defaultTheme }
        };
    }

    public MudTheme GetTheme(string themeName = DEFAULT_THEME)
    {
        if (_themes.TryGetValue(themeName, out MudTheme? theme))
        {
            return theme;
        }
        else
        {
            return new();
        }
    }
}
