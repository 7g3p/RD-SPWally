﻿using System;
using System.Windows;
using SPWally.FunctionalPages;

namespace SPWally
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var newPage = new MainPage();

            _mainFrame.Navigate(newPage);
        }
    }
}
