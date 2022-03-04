using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace Ledger.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isPaneOpen = false;

        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
        }

        private int _menuSelectedIndex = 0;

        public int MenuSelectedIndex
        {
            get => _menuSelectedIndex;
            set
            {
                CurrentViewModel = value switch
                {
                    0 => new HomeViewModel(),
                    1 => new PlannedViewModel(),
                    2 => new UnplannedViewModel(),
                    3 => new TodayViewModel(),
                    _ => CurrentViewModel
                };
                SettingsSelectedIndex = -1;
                this.RaiseAndSetIfChanged(ref _menuSelectedIndex, value);
            } 
        }

        private int _settingsSelectedIndex = -1;

        public int SettingsSelectedIndex
        {
            get => _settingsSelectedIndex;
            set 
            {
                if (value == 0)
                {
                    MenuSelectedIndex = -1;
                    CurrentViewModel = new SettingsViewModel();
                }
                this.RaiseAndSetIfChanged(ref _settingsSelectedIndex, value);
            }
        }
        
        private ViewModelBase _currentViewModel = new HomeViewModel();
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        public ReactiveCommand<Unit, Unit> TogglePaneCmd { get; }
        
        public MainWindowViewModel()
        {
            CurrentViewModel = new HomeViewModel();
            TogglePaneCmd = ReactiveCommand.Create(ToggleBtnClk);
        }

        private void ToggleBtnClk()
        {
            IsPaneOpen = !IsPaneOpen;
        }
    }
}