﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShFLY.SMGS.allsmgs;

namespace ShFLY.SMGS
{
    /// <summary>
    /// Логика взаимодействия для allSmgsView.xaml
    /// </summary>
    public partial class allSmgsView : Window
    {
        public allSmgsView()
        {
            DataContext = new allSmgsViewModel();
            InitializeComponent();
        }
    }
}
