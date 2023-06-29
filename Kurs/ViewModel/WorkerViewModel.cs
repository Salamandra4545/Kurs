using Kurs.Model;
using Kurs.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Kurs.ViewModel
{
    class WorkerViewModel : INotifyPropertyChanged
    {
        private WorkerView window;
        private Worker selectedWorker;
        ModelContext db = new ModelContext();
        private string ImageFileName { get; set; }
        public ObservableCollection<Worker> WorkerList { get; set; }
        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }

        public WorkerViewModel(WorkerView window)
        {
            this.window = window;
            WorkerList = new ObservableCollection<Worker>();
            db.Database.EnsureCreated();
            db.Worker.Load();
            WorkerList = db.Worker.Local.ToObservableCollection();
        }

        private RelayCommand loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                  (loadCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog ofd = new OpenFileDialog();
                      ofd.Filter = "*.jpg|*.jpg|*.bmp|*.bmp";
                      if (ofd.ShowDialog() == true)
                      {
                          BitmapImage myBitmapImage = new BitmapImage();
                          ImageFileName = ofd.FileName;
                          myBitmapImage.BeginInit();
                          myBitmapImage.UriSource = new Uri(ofd.FileName);
                          myBitmapImage.DecodePixelWidth = 200;
                          myBitmapImage.EndInit();
                          window.WorkerImage.Source = myBitmapImage;

                      }
                  }));
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Worker worker = new Worker();
                      worker.Name = window.Name.Text;
                      worker.Phone_number = window.Phone_number.Text;
                      worker.Home_number = window.Home_number.Text;
                      worker.Address = window.Address.Text;
                      worker.Foto = Convert.ToBase64String(File.ReadAllBytes(ImageFileName));
                      db.Worker.Add(worker);
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
                      Worker worker = selectedItem as Worker;
                      if (worker == null) return;
                      db.Worker.Remove(worker);
                      db.SaveChanges();
                      WorkerList.Remove(worker);
                  }));
            }
        }
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new RelayCommand(selectedItem =>
                  {
                      // получаем выделенный объект
                      Worker worker = selectedItem as Worker;
                      if (worker == null) return;
                      worker.Name = window.Name.Text;
                      worker.Phone_number = window.Phone_number.Text;
                      worker.Home_number = window.Home_number.Text;
                      worker.Address = window.Address.Text;
                      db.Entry(worker).State = EntityState.Modified;
                      db.SaveChanges();
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
