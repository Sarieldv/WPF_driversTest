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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for TesterOptions.xaml
    /// </summary>
    public partial class TesterOptions : UserControl
    {
        public TesterOptions()
        {
            InitializeComponent();
        }

        private void addTesterButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new AddTester());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void updateTesterButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new UpdateTester());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void cancelTesterButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new EraseTester());
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Add(new openingWindow());
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
