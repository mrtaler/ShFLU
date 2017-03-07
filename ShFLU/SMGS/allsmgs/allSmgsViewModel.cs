using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ShFLU.DataBase.Table;
using ShFLU.DataBase;
using ShFLU.MVVM;

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
        public allSmgsViewModel()
        {
            context = new ShFluContext();
            AllSmgsNakl = new ObservableCollection<SmgsNakl>(context.SmgsNaklDbSet);
        }
    }
}
