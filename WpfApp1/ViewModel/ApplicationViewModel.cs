using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WpfApp1.Model;
using WpfApp1.Model.Date;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    internal class ApplicationViewModel : NotificationObject
    {
        ApplicationContext appContext = new ApplicationContext();
        DataWorker worker = new DataWorker();
        

        #region OBS date Base
        public ObservableCollection<Result> result { get; set; }
        public ObservableCollection<USL> usl { get; set; } = null!;
        public ObservableCollection<Device> devices { get; set; } = null!;
        public ObservableCollection<VVOD> vODs { get; set; } = null!;
        public ObservableCollection<Result_uslugi> res_uslugi { get; set; } = null!;

        #endregion
        // Свойства для классов бд, для оповещения о изменении
        #region Parapmetrs data base







        private Result_uslugi _res;
        public Result_uslugi Res
        {
            get { return _res; }
            set
            {
                _res = value;
                OnPropertyChanged();
            }
        }


        private USL _uslugi;
        public USL Uslugi
        {
            get { return _uslugi; }
            set
            {
                _uslugi = value;
                OnPropertyChanged();
            }
        }


        private USL _usl;
        public USL SUsl
        {
            get { return _usl; }
            set
            {
                _usl = value;
                OnPropertyChanged();
            }
        }


        private Result _result;
        public Result Sresult
        {
            get { return _result; }
            set
            { _result = value;
                OnPropertyChanged();
            }
        }


        #endregion
       
        public ApplicationViewModel()
        {
            appContext.usl.Load();
            usl = appContext.usl.Local.ToObservableCollection();
            appContext.uslugi.Load();
            res_uslugi = appContext.uslugi.Local.ToObservableCollection();
        }

        #region Button tab element

        private RelayCommand tabbFirstComboBoxElelment;
        public RelayCommand TabbFirstComboBoxElelment
        {
            get
            {
                return tabbFirstComboBoxElelment ?? new RelayCommand(async SelectedItem =>
                {
                    try
                    {
                        if ( _usl.Id == 1)
                        {
                            int idusl = _usl.Id;
                            var test = worker.Set_calc_usl(idusl);
                            foreach (var item in test)
                            {
                                appContext.uslugi.Add(item);

                            }
                            appContext.SaveChanges();
                            WindowResultUsl ResWin = new WindowResultUsl();
                            if (ResWin.ShowDialog() == true)
                            {
                                await worker.Delete_date_teble_res_usl();
                                appContext.SaveChanges();
                            }
                        }
                        if ( _usl.Id == 2)
                        {
                            int idusl = _usl.Id;
                            var test = worker.Set_calc_usl(idusl);
                            foreach (var item in test)
                            {
                                appContext.uslugi.Add(item);

                            }
                            appContext.SaveChanges();
                            WindowResultUsl ResWin = new WindowResultUsl();
                            if (ResWin.ShowDialog() == true)
                            {
                                await worker.Delete_date_teble_res_usl();
                                appContext.SaveChanges();
                            }
                        }
                        if ( _usl.Id == 3)
                        {
                            MessageBox.Show("Функционал еще не подвезли");
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        #endregion

    }
}
    
