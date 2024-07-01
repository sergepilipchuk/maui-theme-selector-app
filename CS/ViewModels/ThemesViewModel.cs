using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.Core;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ThemeSelectorApp;

public partial class ThemesViewModel : ObservableObject {
    [ObservableProperty]
    ObservableCollection<ColorModel> items;

    [ObservableProperty]
    ColorModel selectedColor;

    [ObservableProperty]
    Color previewColor;

    [ObservableProperty]
    string previewColorName;

    [ObservableProperty]
    public List<string> themeTypes = new List<string>() { "System", "Dark", "Light" };

    [ObservableProperty]
    public string selectedThemeType;

    partial void OnSelectedThemeTypeChanged(string value) {
        switch (value) {
            case "Light": {
                Application.Current.UserAppTheme = AppTheme.Light;
                break;
            }
            case "Dark": {
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;
            }
            case "System": {
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;
            }
        }
    }

    partial void OnSelectedColorChanged(ColorModel colorModel) {
        if (colorModel == null)
            return;

        PreviewColorName = colorModel.DisplayName;
        if (colorModel.IsSystemColor) {
            ThemeManager.UseAndroidSystemColor = true;
            return;
        }
        
        ThemeManager.UseAndroidSystemColor = false;
        ThemeManager.Theme = new Theme(colorModel.Color);
    }

    public ThemesViewModel() {
        Items = new ObservableCollection<ColorModel>() {
#if ANDROID
            new ColorModel(Colors.Black, "System Color", true),
#endif
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.Purple), ThemeSeedColor.Purple.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.Violet), ThemeSeedColor.Violet.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.Red), ThemeSeedColor.Red.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.Brown), ThemeSeedColor.Brown.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.TealGreen), ThemeSeedColor.TealGreen.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.Green), ThemeSeedColor.Green.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.DarkGreen), ThemeSeedColor.DarkGreen.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.DarkCyan), ThemeSeedColor.DarkCyan.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.DeepSeaBlue), ThemeSeedColor.DeepSeaBlue.ToString()),
            new ColorModel(ThemeManager.GetSeedColor(ThemeSeedColor.Blue), ThemeSeedColor.Blue.ToString()),
        };
        SelectedColor = Items[0];
        SelectedThemeType = ThemeTypes[0];
    }
}

public class ColorModel {
    public Color Color { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool IsSystemColor { get; set; }

    public ColorModel(Color color, string displayName, bool isSystemColor = false) {
        Color = color;
        DisplayName = displayName;
        IsSystemColor = isSystemColor;
        Name = isSystemColor ? "System" : string.Empty;
    }
}
