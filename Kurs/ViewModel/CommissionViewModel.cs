using Kurs.Model;
using Kurs.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.ViewModel
{
    public class CommissionViewModel : INotifyPropertyChanged
    {
        private CommissionView window;
        private Commission selectedCommission;
        ModelContext db = new ModelContext();
        public ObservableCollection<Commission> CommissionList { get; set; }
        public ObservableCollection<Worker> WorkerList { get; set; }
        public Commission SelectedCommission
        {
            get { return selectedCommission; }
            set
            {
                selectedCommission = value;
                OnPropertyChanged("SelectedCommission");
            }
        }

        public CommissionViewModel(CommissionView window)
        {
            this.window = window;
            CommissionList = new ObservableCollection<Commission>();
            db.Database.EnsureCreated();
            db.Commission.Load();
            db.Worker.Load();
            CommissionList = db.Commission.Local.ToObservableCollection();
            WorkerList = db.Worker.Local.ToObservableCollection();
        }
    

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Commission commission = new Commission();
                      commission.Name = window.Name.Text;
                      commission.Chairman = (window.Chairman.SelectedItem as Worker).Id;
                      db.Commission.Add(commission);
                      db.SaveChanges();
                      
                  }));
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(selectedItem =>
                  {
                      // получаем выделенный объект
                      Commission commission = selectedItem as Commission;
                      if (commission == null) return;
                      db.Commission.Remove(commission);
                      db.SaveChanges();
                      CommissionList.Remove(commission);
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
