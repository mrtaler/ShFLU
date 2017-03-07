using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ShFLU.DataBase.Table;
using ShFLU.DataBase;
using ShFLU.MVVM;

namespace ShFLY.AllWag
{
  public  class AllWagViewModel:ViewModelBase
    {
      ShFluContext context;
      public ObservableCollection<Wagon> AllWag { get; set; }
      public ViewModelCommand RefreshWagonCommand { get; set; }
      
      public AllWagViewModel()
      { 
          context= new ShFluContext();
          RefreshWagonCommand = new ViewModelCommand(RefreshWagon, true);
          AllWag = new ObservableCollection<Wagon>(context.WagonDbSet);
      }
      
      private void RefreshWagon(object param)
      {
          context.Dispose();
          context = new ShFluContext();
          AllWag = new ObservableCollection<Wagon>(context.WagonDbSet);
          NotifyPropertyChanged("AllWag");
      }
    }
}
