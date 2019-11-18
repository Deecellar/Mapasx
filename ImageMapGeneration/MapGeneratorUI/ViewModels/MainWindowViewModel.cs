using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Media.Imaging;
using MapGen;
using ReactiveUI;

namespace MapGeneratorUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Hello World!";

        private Bitmap _bitmap;
        private Single _modifier;
        private Single _zoom;
        
        public Bitmap Bitmap
        {
            get => _bitmap;
            set => this.RaiseAndSetIfChanged(ref _bitmap, value);
        }

        public Single Modifier
        {
            get => _modifier;
            set
            {
                CreateBitmap();this.RaiseAndSetIfChanged(ref _modifier, value); }
        }

        public Single Zoom
        {
            get => _zoom;
            set
            {
                CreateBitmap();this.RaiseAndSetIfChanged(ref _zoom, value); }
        }

        public MainWindowViewModel()
        {
            Modifier = 0.24f;
            Zoom = 1;
        }
        private void CreateBitmap()
        {
            Generate generate = new Generate();
            Bitmap = new Bitmap(generate.DrawMap(640,640,Modifier,Zoom));
        }
    }
}