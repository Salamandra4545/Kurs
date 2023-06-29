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
    public class MembersViewModel : INotifyPropertyChanged
    {
        private MembersView window;
        private Members selectedMembers;
        ModelContext db = new ModelContext();
        public ObservableCollection<Commission> CommissionList { get; set; }
        public ObservableCollection<Members> MembersList { get; set; }
        public ObservableCollection<Worker> WorkerList { get; set; }
        public Members SelectedMembers
        {
            get { return selectedMembers; }
            set
            {
                selectedMembers = value;
                OnPropertyChanged("SelectedMembers");
            }
        }

        public MembersViewModel(MembersView window)
        {
            this.window = window;
            MembersList = new ObservableCollection<Members>();
            db.Database.EnsureCreated();
            db.Commission.Load();
            db.Members.Load();
            db.Worker.Load();
            CommissionList = db.Commission.Local.ToObservableCollection();
            MembersList = db.Members.Local.ToObservableCollection();
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
                        Members members = new Members();
                        members.Id_worker = (window.Id_worker.SelectedItem as Worker).Id;
                        members.Id_commission = (window.Id_commission.SelectedItem as Commission).Id;
                        members.Start_date = window.Start_Date.Text;
                        if (window.End_date.Text != null) members.End_date = window.End_date.Text;
                        db.Members.Add(members);
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
                        Members members = selectedItem as Members;
                        if (members == null) return;
                        db.Members.Remove(members);
                        db.SaveChanges();
                        MembersList.Remove(members);
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


