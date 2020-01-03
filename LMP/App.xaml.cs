﻿using LMP.ViewModels;
using LMP.Views;
using Prism.Ioc;
using Prism.Unity;

namespace LMP
{
    public partial class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(LoginView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<RootNavigationPage>();
            containerRegistry.RegisterForNavigation<SurveysView, SurveysViewModel>();
            containerRegistry.RegisterForNavigation<SurveyDetailsView, SurveyDetailsViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
        }
    }
}
