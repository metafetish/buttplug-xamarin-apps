﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Disposables;
using Intiface.ViewModels;

namespace Intiface.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ReactiveMasterDetailPage<NavigationViewModel>
    {
        IScreen HomeScreen { get; }

        public MainPage()
        {
            InitializeComponent();
            IsPresented = false;

            HomeScreen = App.Current as IScreen;

            ViewModel = NavigationViewModel.Default;

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm, v => v.Navigation.ViewModel)
                    .DisposeWith(disposables);

                // Dismiss the navigation draw when the main screen changes
                this.HomeScreen.Router.NavigationStack
                    .Changed.Subscribe(_ => IsPresented = false)
                    .DisposeWith(disposables);

            });         
        }
    }
}