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
    public class MetingViewModel : INotifyPropertyChanged
    {
        private MetingView window;
        private Meting selectedMeeting;
        ModelContext db = new ModelContext();
        public ObservableCollection<Commission> CommissionList { get; set; }
        public ObservableCollection<Meting> MetingList { get; set; }
        public Meting SelectedMeeting
        {
            get { return selectedMeeting; }
            set
            {
                selectedMeeting = value;
                OnPropertyChanged("SelectedMeeting");
            }
        }

        public MetingViewModel(MetingView window)
        {
            this.window = window;
            MetingList = new ObservableCollection<Meting>();
            db.Database.EnsureCreated();
            db.Commission.Load();
            db.Meting.Load();
            CommissionList = db.Commission.Local.ToObservableCollection();
            MetingList = db.Meting.Local.ToObservableCollection();
        }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Meting meting = new Meting();
                      meting.Date = window.Date.Text;
                      meting.Id_commision = (window.Id_commision.SelectedItem as Commission).Id;
                      db.Meting.Add(meting);
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
                      Meting meting = selectedItem as Meting;
                      if (meting == null) return;
                      db.Meting.Remove(meting);
                      db.SaveChanges();
                      MetingList.Remove(meting);
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
