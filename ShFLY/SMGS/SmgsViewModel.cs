using System.Windows;

using Microsoft.Win32;

using ShFLY.Base;
using ShFLY.DataBase.DAL.Implemtaations;
using ShFLY.DataBase.DAL.Interfaces;

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
                return this._count;
            }

            set
            {
                if (this._count != value)
                {
                    this._count = value;
                    this.NotifyPropertyChanged("Count");
                }
            }
        }

        public ViewModelCommand LoadSmgsCommand { get; set; }
        public ViewModelCommand SerializeSmgsCommand { get; set; }

        public SmgsViewModel()
        {
            this.LoadSmgsCommand = new ViewModelCommand(this.LoadSmgs, true);
            this.SerializeSmgsCommand = new ViewModelCommand(this.SerializeSmgs, false);
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
            if (dlg.FileName != string.Empty)
            {
                this.XMLSmgsWagonPatch = dlg.FileNames;
                this.NotifyPropertyChanged("XMLSmgsWagonPatch");
                this.SerializeSmgsCommand.CanExecute = true;
            }
        }

        private void SerializeSmgs(object param)
        {
            this.Count = 0;
            foreach (var item in this.XMLSmgsWagonPatch)
            {
                SmgsSeriallXML neSeriallXml = new SmgsSeriallXML(item, this.context);
                this.Count+=1;
            }
            
            MessageBox.Show("done");
        }

    }
}
