using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace REDFLAG
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        public Form1()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Подключение к базе данных успешно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Вы не ввели все необходимые данные!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                string username = textBox1.Text;
                string password = textBox2.Text;

                string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";

                using (connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (command = new MySqlCommand("SELECT * FROM users WHERE username = @login AND password = @parol", connection))
                        {
                            command.Parameters.AddWithValue("@login", username);
                            command.Parameters.AddWithValue("@parol", password);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                           
                                
                                if (reader.Read())
                                {
                                    string role = reader["role"].ToString();

                                    if (role == "admin")
                                    {
                                        Form10 form10 = new Form10();
                                        form10.Show();
                                        this.Hide();
                                    }
                                    else if (role == "doctor")
                                    {
                                        MessageBox.Show("Авторизация успешна!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                        Form2 form2 = new Form2();
                                        form2.Show();
                                        this.Hide();
                                    }
                                }

                                else
                                {
                                    MessageBox.Show("Неправильный логин или пароль!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }
    }
}
