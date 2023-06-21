using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Date
{
    internal class USL : NotificationObject
    {
        public int Id {get ; set ; }
        string? name;

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
    }
}
