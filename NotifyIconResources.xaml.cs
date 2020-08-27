﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//file is WIP, this file contains logic to handle opening, closing and hiding of the window from the tray
namespace LoL_Generator
{
    /// <summary>
    /// Interaction logic for NotifyIconResources.xaml
    /// </summary>
    public partial class NotifyIconResources : Page
    {
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => App.window.Visibility == Visibility.Hidden || App.window.WindowState == WindowState.Minimized,
                    CommandAction = () => {
                        if (App.window.Visibility == Visibility.Hidden)
                        {
                            App.window.Show();
                        }

                        if (App.window.WindowState == WindowState.Minimized)
                        {
                            App.window.WindowState = WindowState.Normal;
                        }

                        App.window.Activate();
                        App.window.Topmost = true;
                        App.window.Topmost = false;
                    }
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => App.window.Visibility == Visibility.Visible,
                    CommandAction = () => App.window.Hide()
                };
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand 
                { 
                    CommandAction = () => Application.Current.Shutdown()
                };
            }
        }
    }

    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
