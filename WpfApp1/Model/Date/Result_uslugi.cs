using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.Date
{
    public class Result_uslugi : NotificationObject
    {
       
         public int Id { get; set; }
         string? name;
        string? datestart;
         string? dateend;
         int span;
        //string? span_condition;

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string? Datestart
        {
            get { return datestart; }
            set
            {
                datestart = value;
                OnPropertyChanged();
            }
        }
        public string? Dateend
        {
            get { return dateend; }
            set
            {
                dateend = value;
                OnPropertyChanged();
            }
        }
        public int Span
        {
            get { return span; }
            set
            {
                span = value;
                OnPropertyChanged();
            }
        }
        //public string? Span_condition
        //{   get
        //    { return span_condition; }
        //    set  
        //    {
        //        span_condition = value;
        //        OnPropertyChanged();
        //    } 
        //}
        
    }
}
