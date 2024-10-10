using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace REDFLAG
{
    public partial class Form9 : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM clinics";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            // Create a command to add data to the table
            string query = "INSERT INTO clinics (id, name, location, phone_number) " +
                           "VALUES (@id, @name, @location, @phone_number)";

            MySqlCommand command = new MySqlCommand(query, connection);

            // Add parameters for the command
            command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
            command.Parameters.AddWithValue("@name", textBox2.Text);
            command.Parameters.AddWithValue("@location", textBox3.Text);
            command.Parameters.AddWithValue("@phone_number", textBox4.Text);

            // Execute the command
            command.ExecuteNonQuery();

            // Close the connection


            // Update the data in DataGridView
            string selectQuery = "SELECT * FROM clinics";
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            connection.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Получаем ввод из textBox6
            string input = textBox5.Text;

            // Валидируем ввод
            if (!int.TryParse(input, out int id))
            {
                MessageBox.Show("Пожалуйста, введите корректный целочисленный идентификатор.", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Создаем соединение с базой данных и команду
            string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM clinics WHERE id = @id", conn))
            {
                // Добавляем параметр и выполняем команду
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    MessageBox.Show("Запись с указанным идентификатором не найдена.", "Удаление не удалось", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Запись удалена успешно.", "Удаление успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                textBox5.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
