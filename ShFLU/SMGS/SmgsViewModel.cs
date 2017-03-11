using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ShFLU.DataBase;
using ShFLU.MVVM;
using Microsoft.Win32;
using ShFLY.SMGS;

namespace ShFLU.SMGS
{
    class SmgsViewModel : ViewModelBase
    {
        private ShFluContext context = new ShFluContext();
        private string XMLSmgsWagonPatch;
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
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                XMLSmgsWagonPatch = dlg.FileName;
                NotifyPropertyChanged("XMLSmgsWagonPatch");
                SerializeSmgsCommand.CanExecute = true;
            }
        }


        private void SerializeSmgs(object param)
        {
            SmgsSeriallXML neSeriallXml = new SmgsSeriallXML(XMLSmgsWagonPatch);
            MessageBox.Show("done");
        }

    }
}

