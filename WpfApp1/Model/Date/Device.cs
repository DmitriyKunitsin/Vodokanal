using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Date
{
    internal class Device : NotificationObject
    {
        public int Id { get; set; }
        int vvod;
        string? name;
        DateTime datestart;
        DateTime dataend;

        public int Vvod { 
            get 
            { return vvod;
            }
            set
            {
              vvod = value;
                OnPropertyChanged();
            }
        }
        public string? Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public DateTime Datestart
        {
            get
            {
                return datestart;
            }
            set
            {
                datestart = value;
                OnPropertyChanged();
            }
        }
        public DateTime Dataend
        {
            get
            {
                return dataend;
            }
            set
            {
                dataend = value;
                OnPropertyChanged();
            }
        }
    }
}
