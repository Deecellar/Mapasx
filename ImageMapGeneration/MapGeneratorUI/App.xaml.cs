using Avalonia;
using Avalonia.Markup.Xaml;

namespace MapGeneratorUI
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}