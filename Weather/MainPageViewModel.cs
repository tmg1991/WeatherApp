using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using Weather.Interfaces;
using Weather.Models;
using Weather.Resources;

namespace Weather
{
    public class MainPageViewModel : NotifyBase
    {
        private readonly City _fakeCity = new City(AppRes.Add_new_city, new GeoLocation(0, 0));
        private readonly IDialogService _dialogService;
        private readonly Serializer _serializer;
        public ICommand RemoveCityCommand { get; init; }
        private City selectedCity;

        public ObservableCollection<City> Cities { get; } = new ObservableCollection<City>();
        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                NotifyPropertyChanged();
                OnSelectedCity();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; NotifyPropertyChanged(); }
        }

        public MainPageViewModel(IDialogService dialogService, Serializer serializer)
        {
            _dialogService = dialogService;
            _serializer = serializer;
            RemoveCityCommand = new Command(RemoveCity);
            foreach (var city in LoadCities())
            {
                Cities.Add(city);
            }
            Cities.Add(_fakeCity);
            InitSelection();
        }

        private void RemoveCity() 
        {
            if(SelectedCity == null || SelectedCity == _fakeCity)
            {
                return;
            }

            var cityToBeRemoved = SelectedCity;
            SelectedCity = null;
            Cities.Remove(cityToBeRemoved);
            SaveCities();
            InitSelection();
        
        }

        private void InitSelection()
        {
            var selectedCityName = Preferences.Get("SelectedCity", string.Empty);
            if (!string.IsNullOrEmpty(selectedCityName)) 
            {
                if(Cities.Any(c => c.Name == selectedCityName))
                {
                    SelectedCity = Cities.First(c => selectedCityName == c.Name);
                }
            }

            SelectedCity = Cities.Where(c => c != _fakeCity)?.FirstOrDefault();

        }

        private void OnSelectedCity()
        {
            if(SelectedCity == null)
            { 
                return;
            }

            if(SelectedCity != _fakeCity)
            {
                Preferences.Set("SelectedCity", SelectedCity.Name);
                return;
            }

            var t = AddNewCity();
        }

        private async Task AddNewCity()
        {
            var t = Task.Run(async () =>
            {
                var cityNameTypedIn = await _dialogService.ShowPromptAsync(AppRes.NewCityPromptTitle, AppRes.AddNewCityPromptMessage);
                IsBusy = true;
                try
                {
                    var newCity = await GetCityAsync(cityNameTypedIn);
                    IsBusy = false;
                    if (newCity == null)
                    {
                        await _dialogService.ShowAlertAsync(AppRes.ErrorPromptTitle, AppRes.ErrorPromptMessage, AppRes.ErrorPromptOk);
                        SelectedCity = null;
                        return;
                    }
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Cities.Add(newCity);
                        OrderCites();
                        SelectedCity = newCity;
                        SaveCities();
                    });
                }
                catch (TaskCanceledException)
                {
                    IsBusy = false;
                    await _dialogService.ShowAlertAsync(AppRes.TimeOutTitle, AppRes.TimeOutMessage, AppRes.ErrorPromptOk);
                    SelectedCity = null;
                    return;
                }
            });
        }

        private void SaveCities()
        {
            var toBeSaved = new ObservableCollection<City>();
            foreach (var city in Cities)
            {
                if(city == _fakeCity)
                {
                    continue;
                }
                toBeSaved.Add(city); 
            }


            var json = _serializer.Serialize(toBeSaved);
            Preferences.Set("UserData", json);
        }

        private ObservableCollection<City> LoadCities()
        {
            var loadedCities = new ObservableCollection<City>();
            string savedJson = Preferences.Get("UserData", string.Empty);
            if (!string.IsNullOrEmpty(savedJson))
            {
                loadedCities = JsonSerializer.Deserialize<ObservableCollection<City>>(savedJson);
            }

            return loadedCities;
        }


        private void OrderCites()
        {
            var temp = new List<City>();
            foreach(var city in Cities.OrderBy(c => c.Name))
            {
                temp.Add(city);
            }
            temp.Remove(_fakeCity);
            temp.Add(_fakeCity);

            Cities.Clear();
            foreach (var city in temp) 
            {
                Cities.Add(city);
            }
        }


        public async Task<City> GetCityAsync(string city)
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("User-Agent", "MyApp"); // required by Nominatim

            var url = $"https://nominatim.openstreetmap.org/search" +
                      $"?q={Uri.EscapeDataString(city)}&format=json&limit=1";

            var response = await client.GetStringAsync(url);

            var json = JsonDocument.Parse(response);
            if (json.RootElement.GetArrayLength() == 0)
            {
                return null;
            }
            var item = json.RootElement[0];

            var latValue = item.GetProperty("lat");
            var latAsString = latValue.GetString();

            if(!double.TryParse(latAsString, CultureInfo.InvariantCulture, out var lat))
            {

            }

            var lonValue = item.GetProperty("lon");
            var lonAsString = lonValue.GetString();

            if (!double.TryParse(lonAsString, CultureInfo.InvariantCulture, out var lon))
            {

            }
            var location = new GeoLocation(lon, lat);
            var cityName = item.GetProperty("name").GetString();
            return new City(cityName, location);
        }
    }
}
