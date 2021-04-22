using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amiibopedia.Helpers;
using Amiibopedia.Models;
using Xamarin.Forms;


namespace Amiibopedia.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Character> _characters;
        private ObservableCollection<Amiibo> _amiibos;
        private ICommand _searchCommand;

        public ObservableCollection<Character> Characters
        {
            get => _characters;
            set => _characters = value;
        }

        public ObservableCollection<Amiibo> Amiibos
        {
            get => _amiibos;
            set
            {
                _amiibos = value;

                OnPropertyChanged();
            } 
        }

        public ICommand SearchCommand
        {
            get => _searchCommand;
            set => _searchCommand = value;
        }

        public MainPageViewModel()
        {
            SearchCommand = new Command(async (param) =>
            {
                var character = param as Character;

                if (character != null)
                {
                    IsBusy = true;
                    string url = $"https://amiiboapi.com/api/amiibo/?character={character.name}";
                    var service =
                        new HttpHelper<Amiibos>();

                    var amiibos = await service.GetRestServiceDataAsync(url);

                    Amiibos = new ObservableCollection<Amiibo>(amiibos.amiibo);

                    IsBusy = false;
                }
            });
        }

        public async Task LoadCharactrs()
        {
            IsBusy = true;
            var url = "https://amiiboapi.com/api/character/";
            var service =
                new HttpHelper<Characters>();

            var characters = await service.GetRestServiceDataAsync(url);

            Characters = new ObservableCollection<Character>(characters.amiibo);
            IsBusy =false;
        }

    }
}
