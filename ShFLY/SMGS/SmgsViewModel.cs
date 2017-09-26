using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.Base;
using ShFLY.DataBase.DAL.Implemtaations;
using ShFLY.DataBase.DAL.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace ShFLY.SMGS
{
    class SmgsViewModel : ViewModelBase
    {
        private IUnitOfWork context = new UnitOfWork();
        private string[] XMLSmgsWagonPatch;
        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    NotifyPropertyChanged("Count");
                }
            }
        }

        public ViewModelCommand LoadSmgsCommand { get; set; }
        public ViewModelCommand SerializeSmgsCommand { get; set; }

        public SmgsViewModel()
        {
            LoadSmgsCommand = new ViewModelCommand(LoadSmgs, true);
            SerializeSmgsCommand = new ViewModelCommand(SerializeSmgs, false);
        }


        private void LoadSmgs(object param)
        {
            string str = "pkc7";
            string filter = "GCS Skill files (*." + str + ")| *." + str + "| All Files(*.*) | *.* ";
            OpenFileDialog dlg = new OpenFileDialog();
            // dlg.InitialDirectory = @"D:\ДокументыЛинкевич\ПРИХОД ШФУ\ЕТК\02";
            dlg.InitialDirectory = @"C:\Users\Derdan\Dropbox\Work\Matrix";

            dlg.Filter = filter;
            dlg.Multiselect = true;
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                XMLSmgsWagonPatch = dlg.FileNames;
                NotifyPropertyChanged("XMLSmgsWagonPatch");
                SerializeSmgsCommand.CanExecute = true;
            }
        }


        private void SerializeSmgs(object param)
        {
            Count = 0;
            foreach (var item in XMLSmgsWagonPatch)
            {
                SmgsSeriallXML neSeriallXml = new SmgsSeriallXML(item, context);
               Count+=1;
            }
            
            MessageBox.Show("done");
        }

    }
}
