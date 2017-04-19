using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using ShFLU.DataBase.Table;
using ShFLU.DataBase;
using ShFLU.MVVM;
using ShFLU.SMGS.allsmgs.FindMatrix;

namespace ShFLY.SMGS.allsmgs
{
    public class allSmgsViewModel : ViewModelBase
    {
        private ShFluContext context;
        public ObservableCollection<SmgsNakl> AllSmgsNakl { get; set; }
        private SmgsNakl selectSmgs;
        public SmgsNakl SelectSmgs
        {
            get
            {
                return selectSmgs;
            }
            set
            {
                if (selectSmgs != value)
                {
                    selectSmgs = value;
                    NotifyPropertyChanged("AllWagInSmgs");
                    NotifyPropertyChanged("SelectSmgs");
                }
            }
        }
        public ObservableCollection<WagInSmgs> AllWagInSmgs
        {
            get
            {
                if (SelectSmgs!=null)
                {
                    return new ObservableCollection<WagInSmgs>(SelectSmgs.WagInSmgses); 
                }
                else
                {
                    return  null; 
                }
               
            }
        }

        public ViewModelCommand EditCommand { get; set; }
        public ViewModelCommand DeleteCommand { get; set; }

        public allSmgsViewModel()
        {
            context = new ShFluContext();
            AllSmgsNakl = new ObservableCollection<SmgsNakl>(context.SmgsNaklDbSet);
            EditCommand = new ViewModelCommand(Edit, true);
            DeleteCommand = new ViewModelCommand(Delete, true);
           
        }
        private void Edit(object param)
        {
            FindMatrixView win=new FindMatrixView((WagInSmgs)param, context);
            win.ShowDialog();
            //MessageBox.Show(((WagInSmgs)param).Wagon.Nwag.ToString());
        }

        private void Delete(object param)
        {
            AllSmgsNakl.Remove((SmgsNakl) param);
            context.SmgsNaklDbSet.Remove((SmgsNakl) param);
            context.SaveChanges();
        }

      
    }
}
