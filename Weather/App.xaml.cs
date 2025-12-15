using Microsoft.Extensions.DependencyInjection;

namespace Weather
{
    public partial class App : Application
    {
        public static event Action? AppResumed;
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnResume()
        {
            base.OnResume();
            AppResumed?.Invoke();
        }
    }
}