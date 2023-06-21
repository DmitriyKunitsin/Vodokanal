using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Date
{
    internal class VVOD : NotificationObject
    {
        public int Id { get; set; }
         string? name;
         int uslid;

        public string? Name
        {
            get
            { 
                return name;
                
            }set
            { 
                name = value;
                OnPropertyChanged();
            }
        }
        public int Uslid
        { 
            get 
            {
                return uslid;
            } 
            set 
            { 
                uslid = value; 
                OnPropertyChanged();
            } 
        }
    }
}
