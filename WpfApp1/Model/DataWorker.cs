using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Data.SqlClient;
using WpfApp1.Model.Date;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data;
using System.Windows.Controls.Primitives;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;

namespace WpfApp1.Model
{
    internal class DataWorker
    {
        // выгрузка нужных данных из бд

        public void Table_date()
        {
            DataTable dataTable = Select($"SELECT  t2.name,SUM(t2.span) as total FROM (SELECT u.name ,r1.span, CASE WHEN r1.iddevice <= 7 THEN 'холодная вода' ELSE 'горячая вода' END as Type_water FROM  result r1 JOIN device d ON r1.iddevice = d.id JOIN vvod v ON v.id = d.vvodid join usl u on v.uslid = u.id WHERE r1.datestart >= d.Datestart AND r1.DateEnd <= d.Dateend) as t2 GROUP BY t2.name, t2.Type_water");
            for (int i = 0; i < dataTable.Rows.Count; i++)// создаём таблицу в приложении
            {
                MessageBox.Show("Услуга : " + dataTable.Rows[i][0] + " | Объём : " + dataTable.Rows[i][1]);
            }
        }
        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {

            DataTable dataTable = new DataTable();// подключаемся к базе данных

            SqlConnection sqlConnection = new SqlConnection("server=(localdb)\\mssqllocaldb;Trusted_Connection=Yes;DataBase=Vodoconal;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }
       
        #region Methods set date
        //вывод нформации отсортированной по возможности снятий показаний
        public ObservableCollection<Result_uslugi> Set_calc_usl(int id)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Vodoconal;Trusted_Connection=True;");
            conn.Open();
            SqlDataReader reader = null;
            try
            {
                ApplicationContext context = new ApplicationContext();
                string request_to_db = $"SELECT USL.name ,convert(varchar(10),RESULT.datestart) , convert(varchar(10),RESULT.dateend), RESULT.SPAN" +
                    $",CASE " +
                    $"WHEN RESULT.DateStart >= device.Datestart AND RESULT.DateEnd <= device.Dateend " +
                    $"THEN (SELECT RESULT.span) " +
                    $"ELSE (SELECT '') " +
                    $"END AS TEST " +
                    $"FROM RESULT " +
                    $"Left JOIN device ON RESULT.iddevice = device.id " +
                    $"JOIN vvod ON device.vvodid = vvod.id " +
                    $"JOIN USL  ON vvod.uslid = USL.id " +
                    $"WHERE USL.id = {id}" +
                  $" ORDER BY RESULT.DateStart , RESULT.DateEnd";                
                SqlCommand command = new SqlCommand(request_to_db, conn);
                reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    
                    ObservableCollection<Result_uslugi> res = new ObservableCollection<Result_uslugi>();
                    while (reader.Read())
                    {
                        
                        res.Add(new Result_uslugi()
                        {
                            Name = reader.GetString(0),
                            Datestart = reader.GetString(1),
                            Dateend = reader.GetString(2),
                            Span = reader.GetInt32(4),
                        });
                    }
                    return res;
                }
                else
                {
                    MessageBox.Show("Нет данных");
                    return null;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
        // ввод информации отсортированной по услуге
        public ObservableCollection<Result_uslugi> Set_usl_input(int id)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Vodoconal;Trusted_Connection=True;");
            conn.Open();
            SqlDataReader reader = null;
            try
            {
                string request_to_db = $"SELECT usl.name  , result.datestart , result.dateend , result.span" +
                    $" FROM result " +
                    $"JOIN device  ON(result.iddevice = device.id) " +
                    $"JOIN vvod ON(device.Vvod = vvod.id)" +
                    $" JOIN usl  ON(vvod.uslid = usl.id) " +
                    $"AND usl.id = vvod.uslid" +
                    $" WHERE usl.id = {id}" +
                    $"ORDER BY result.datestart, result.dateend ;";
                SqlCommand command = new SqlCommand(request_to_db, conn);
                reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    ObservableCollection<Result_uslugi> res = new ObservableCollection<Result_uslugi>();
                    while (reader.Read())
                    {
                        res.Add(new Result_uslugi()
                        {
                            Name = reader.GetString(0),
                            Datestart = reader.GetString(1),
                            Dateend = reader.GetString(2),
                           // Span = reader.GetInt32(3)
                        });
                    }
                    return res;
                }
                else
                {
                    MessageBox.Show("Нет данных");
                    return null;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public ObservableCollection<Result_uslugi> Get_date_table_result()
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Vodoconal;Trusted_Connection=True;");
            conn.Open();

            SqlDataReader reader = null;
            try
            {
                ApplicationContext context = new ApplicationContext();
                string request_to_db = $"SELECT  t2.name,SUM(t2.span) as total " +
                    $"FROM (" +
                    $"SELECT u.name ,r1.span, " +
                    $"CASE " +
                    $"WHEN r1.iddevice <= 7  " +
                    $"THEN 'холодная вода' " +
                    $"ELSE 'горячая вода' " +
                    $"END as Type_water " +
                    $"FROM  result r1 " +
                    $"JOIN device d ON r1.iddevice = d.id " +
                    $"JOIN vvod v ON v.id = d.vvodid join usl u on v.uslid = u.id " +
                    $"WHERE r1.datestart >= d.Datestart " +
                    $"AND r1.DateEnd <= d.Dateend) as t2 " +
                    $"GROUP BY t2.name, t2.Type_water";
                SqlCommand command = new SqlCommand(request_to_db, conn);
                reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    ObservableCollection<Result_uslugi> res = new ObservableCollection<Result_uslugi>();
                    while (reader.Read())
                    {
                        res.Add(new Result_uslugi()
                        {
                            Name = reader.GetString(0),
                            Id = reader.GetInt32(1),
                        });
                    }
                    return res;
                }
                else
                {
                    MessageBox.Show("Нет данных");
                    return null;
                }

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
        //удаление данных с бд, после всех расчетов (нажатия на кнопку ок)
        public async Task Delete_date_teble_res_usl()
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Vodoconal;Trusted_Connection=True;");
            string request_to_db = $"DELETE FROM uslugi";
            conn.OpenAsync();

            SqlDataReader reader = null;
            try
            {
                ApplicationContext context = new ApplicationContext();
                SqlCommand command = new SqlCommand(request_to_db, conn);
                reader = await command.ExecuteReaderAsync();
                if (reader.HasRows != true)
                {
                    MessageBox.Show("Данные расчетов удалены с базы данных");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader == null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            
        }
        #endregion

    }
}
