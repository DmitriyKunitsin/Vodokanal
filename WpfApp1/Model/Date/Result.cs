using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Date
{
    public class Result : NotificationObject 
    {
        public int Id { get; set; }

         string? datestart;
         string? dateend;
         int span;
         int iddevice;

        public string? DateStart
        {
            get
            { return datestart; }
            set
            {
                datestart = value;
                OnPropertyChanged();
            } 
        }
        public string? DateEnd
        {
            get
            { return dateend; }
            set
            {
                dateend = value;
                OnPropertyChanged();
            }
        }
        public int Span
        {
            get
            { return span; }
            set
            {
                span = value;
                OnPropertyChanged();
            }
        }
        public int Iddevice
        {
            get 
            { 
                return iddevice;
            }
            set 
            { 
                iddevice = value;
                OnPropertyChanged();
            }
        }
    }
}
        
             


      

            
    

