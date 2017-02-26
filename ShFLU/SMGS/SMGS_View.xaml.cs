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
using System.Windows.Shapes;

namespace ShFLU.SMGS
{
    /// <summary>
    /// Логика взаимодействия для SMGS_View.xaml
    /// </summary>
    public partial class SMGS_View : Window
    {
        public SMGS_View()
        {
            DataContext = new SmgsViewModel();
            InitializeComponent();
        }
    }
}
