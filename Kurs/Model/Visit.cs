using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.Model
{
    public class Visit : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private int id_worker;
        public int Id_worker
        {
            get { return id_worker; }
            set
            {
                id_worker = value;
                OnPropertyChanged("Id_worker");
            }
        }

        private int id_meeting;
        public int Id_meeting
        {
            get { return id_meeting; }
            set
            {
                id_meeting = value;
                OnPropertyChanged("Id_meeting");
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