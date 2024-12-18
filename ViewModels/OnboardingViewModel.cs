using GreenThumb2._0.Models;
using GreenThumb2._0.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenThumb2._0.ViewModels
{
    public class OnboardingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SlideModel> Slides { get; set; }

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsLastSlide));
                }
            }
        }

        public bool IsLastSlide => CurrentIndex == Slides.Count - 1;

        public ICommand NavigateToAuthenticationCommand { get; }

        public OnboardingViewModel()
        {
            Slides = new ObservableCollection<SlideModel>
            {
                new SlideModel { Title = "Welcome", Description = "Discover our app's features.", Image = "slide1.png" },
                new SlideModel { Title = "Stay Organized", Description = "Keep track of your tasks.", Image = "slide2.png" },
                new SlideModel { Title = "Achieve More", Description = "Reach your goals efficiently.", Image = "slide3.png" }
            };

            NavigateToAuthenticationCommand = new Command(OnNavigateToAuthentication);
        }

        private void OnNavigateToAuthentication()
        {
            Shell.Current.GoToAsync("//AuthPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
