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
    public class VisitViewModel : INotifyPropertyChanged
    {
        private VisitView window;
        private Visit selectedVisit;
        ModelContext db = new ModelContext();
        public ObservableCollection<Visit> VisitList { get; set; }
        public ObservableCollection<Worker> WorkerList { get; set; }
        public ObservableCollection<Meting> MetingList { get; set; }
        public Visit SelectedVisit
        {
            get { return selectedVisit; }
            set
            {
                selectedVisit = value;
                OnPropertyChanged("SelectedVisit");
            }
        }

        public VisitViewModel(VisitView window)
        {
            this.window = window;
            VisitList = new ObservableCollection<Visit>();
            db.Database.EnsureCreated();
            db.Visit.Load();
            db.Worker.Load();
            db.Meting.Load();
            VisitList = db.Visit.Local.ToObservableCollection();
            WorkerList = db.Worker.Local.ToObservableCollection();
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
                      Visit visit = new Visit();
                      visit.Id_meeting = (window.Id_meeting.SelectedItem as Meting).Id;
                      visit.Id_worker = (window.Id_worker.SelectedItem as Worker).Id;
                      db.Visit.Add(visit);
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
                      Visit visit = selectedItem as Visit;
                      if (visit == null) return;
                      db.Visit.Remove(visit);
                      db.SaveChanges();
                      VisitList.Remove(visit);
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
