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
    public class Members : INotifyPropertyChanged
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

        private int id_commission;
        public int Id_commission
        {
            get { return id_commission; }
            set
            {
                id_commission = value;
                OnPropertyChanged("Id_commission");
            }
        }

        private string start_date;
        public string Start_date
        {
            get { return start_date; }
            set
            {
                start_date = value;
                OnPropertyChanged("Start_Date");
            }
        }

        private string end_date;
        public string End_date
        {
            get { return end_date; }
            set
            {
                end_date = value;
                OnPropertyChanged("End_date");
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