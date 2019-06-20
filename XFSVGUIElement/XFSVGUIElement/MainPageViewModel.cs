using System.Windows.Input;
using Xamarin.Forms;

namespace XFSVGUIElement
{
    public class MainPageViewModel
    {
        public ICommand ButtonClicked { get; private set; }

        public MainPageViewModel(INavigation navigation)
        {
            ButtonClicked = new Command<string>(commandParameter => NavigateToPage(commandParameter, navigation));
        }

        private void NavigateToPage(string commandParameter, INavigation navigation)
        {
            switch (commandParameter)
            {
                case "FewExamplesDemo":
                    navigation.PushAsync(new FewExamplesDemo());
                    break;

                case "ScaleDemo":
                    navigation.PushAsync(new ScalingSvgDemo());
                    break;

                case "DropShadowDemo":
                    navigation.PushAsync(new DropShadowDemo());
                    break;
            }
        }
    }
}
