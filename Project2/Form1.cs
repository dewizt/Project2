using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Project2
{
    public partial class Form1 : Form
    {
        string[] arrayID = new string[5]; // Массив ID 
        string[] arrayName = new string[5]; // Массив Имён
        string[] status = new string[5]; // Массив статустов

        public Form1()
        {
            InitializeComponent();
            AddToMassive();
            Thread thread = new Thread(ReverseStatus);
            thread.Start();           
        }
        public void AddToMassive()
        {
            Thread thread = new Thread(() =>
            {
                string connectionStr = "server=localhost;port=3306;user=root;password=root;database=dewizt_db;";

                MySqlConnection conn = new MySqlConnection(connectionStr);

                conn.Open();

                string sql = "SELECT * FROM users";

                MySqlCommand command = new MySqlCommand(sql, conn);

                MySqlDataReader read = command.ExecuteReader();

                int i = 0;
                while (read.Read() & i < 5)
                {
                    arrayID[i] = read[0].ToString();
                    arrayName[i] = read[1].ToString();
                    status[i] = read[2].ToString();
                    i++;
                }
                read.Close();
                conn.Close();
            });
            thread.Start();
            listBox4.Items.Add(DateTime.Now + " добавили ID в массив [ADDTOMASSIVE]");
            listBox4.Items.Add(DateTime.Now + " добавили NAME в массив [ADDTOMASSIVE]");
            listBox4.Items.Add(DateTime.Now + " добавили STATUS в массив [ADDTOMASSIVE]");
            Thread.Sleep(5000);
        } // Добавление элементов в массивы
        public void AddToList()
        {
            for (int i = 0; i < 5; i++)
            {
                listBox1.Items.Add(arrayID[i]);
                listBox2.Items.Add(arrayName[i]);
                listBox3.Items.Add(status[i]);
            }
            listBox4.Items.Add(DateTime.Now + " добавили элементы в массивы");
        } // Добавление в таблицы
        public void ReverseStatus()
        {
                if (status.Length != 0)
                {
                    for (int i = 0; i < status.Length; i++)
                    {
                        if (status[i] == "0")
                        {
                            status[i] = "1 изменено";
                            listBox4.Items.Add(DateTime.Now + " произвели замену '0' на " + status[i] + " [REVERSESTATUS]");
                    }
                        if (status[i] == "1")
                        {
                            status[i] = "0 изменено";
                            listBox4.Items.Add(DateTime.Now + " произвели замену '1' на " + status[i] + " [REVERSESTATUS]");
                        }
                    }
                }
                else
                {
                    listBox4.Items.Add(DateTime.Now + " НЕЧЕГО ЗАПОЛНЯТЬ");
                }
        } // Замена статуса на противоположное значение

        public void button1_Click(object sender, EventArgs e)
        {
            AddToList();
        } // Кнопка "Показать данные"

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox4.Items.Add("Удалили ID");
            listBox2.Items.Clear();
            listBox4.Items.Add("Удалили NAME");
            listBox3.Items.Clear();
            listBox4.Items.Add("Удалили STATUS");
        } // Кнопка "Удалить"

        private void button4_Click(object sender, EventArgs e)
        {
            AddToList();
        } // Кнопка "Выполнить всё сразу"

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
