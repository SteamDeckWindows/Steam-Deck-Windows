using System.Windows.Input;

namespace SteamDeckWindows.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand MakeSandwichCommand { get; }

        public MainViewModel(ICommand makeSandwichCommand)
        {
            MakeSandwichCommand = makeSandwichCommand;
        }
    }
}
