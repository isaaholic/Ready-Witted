using CommunityToolkit.Maui.Storage;
using isaaholic.ReadyWittedLibrary;

namespace ReadyWitted_Client.Views;

public partial class PresentationPage : ContentPage
{
    private string _apiKey;
    private string _theme;
    private List<string> _content;

    public PresentationPage()
    {
        InitializeComponent();
    }

    private async void btnSubmit_Clicked(object sender, EventArgs e)
    {
        txtDown.IsVisible = false;
        btnDown.IsVisible = false;

        ReadyWitted.CreateInstance(_apiKey);
        btnSubmit.IsEnabled = false;

        var textList = await ReadyWitted.GetPresentation(_theme);
        string text = string.Join("\n", textList);

        _content = textList;

        editor.Text = text;
        txtDown.IsVisible = true;
        btnDown.IsVisible = true;
        btnSubmit.IsEnabled = true;
    }

    private void eApiKey_TextChanged(object sender, TextChangedEventArgs e)
    {
        _apiKey = ((Entry)sender).Text;
    }

    private void eTheme_TextChanged(object sender, TextChangedEventArgs e)
    {
        _theme = ((Entry)sender).Text;
    }

    private async void btnDown_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FolderPicker.PickAsync(default);
            using (var writer = File.CreateText(result.Folder.Path+$"/readywitted_{DateTime.UtcNow.ToShortDateString()}_{_theme}.txt"))
            {
                foreach (var line in _content)
                {
                    writer.WriteLine(line);
                }
            }
        }
        catch (Exception)
        {
                
        }

    }
}