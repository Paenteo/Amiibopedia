using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amiibopedia.ViewModels;
using Xamarin.Forms;

namespace Amiibopedia
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel  ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ViewModel = new MainPageViewModel();

            await ViewModel.LoadCharactrs();

            this.BindingContext = ViewModel;

          
        }

        private async void Cell_OnAppearing(object sender, EventArgs e)
        {
            var cell = sender as ViewCell;

            var view = cell.View;


            view.TranslationX = -100;
            view.Opacity = 0;

            await Task.WhenAny<bool>
                (
                     view.TranslateTo(0, 0, 250, Easing.SinIn),
                     view.FadeTo(1,500,Easing.BounceIn)


                );
            
        }
    }
}
