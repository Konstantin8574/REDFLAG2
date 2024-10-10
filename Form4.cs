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
    public partial class Form4 : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM bloodsamples";
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

            // Создаем команду для добавления данных в таблицу
            string query = "INSERT INTO bloodsamples (id, collection_date, donor_id, storage_id, employee_id, quantity, storage_period, hemoglobin, leukocytes, thrombocytes) " +
                           "VALUES (@id, @collection_date, @donor_id, @storage_id, @employee_id, @quantity, @storage_period, @hemoglobin, @leukocytes, @thrombocytes)";

            MySqlCommand command = new MySqlCommand(query, connection);

            // Добавляем параметры для команды
            command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
            command.Parameters.AddWithValue("@collection_date", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@donor_id", int.Parse(textBox2.Text));
            command.Parameters.AddWithValue("@storage_id", int.Parse(textBox3.Text));
            command.Parameters.AddWithValue("@employee_id", int.Parse(textBox4.Text));
            command.Parameters.AddWithValue("@quantity", int.Parse(textBox5.Text));
            command.Parameters.AddWithValue("@storage_period", int.Parse(textBox6.Text));
            command.Parameters.AddWithValue("@hemoglobin", double.Parse(textBox7.Text));
            command.Parameters.AddWithValue("@leukocytes", double.Parse(textBox8.Text));
            command.Parameters.AddWithValue("@thrombocytes", double.Parse(textBox9.Text));

            // Выполняем команду
            command.ExecuteNonQuery();

            // Закрываем соединение
            connection.Close();

            // Обновляем данные в DataGridView
            string selectQuery = "SELECT * FROM bloodsamples";
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            // Очищаем текстовые поля
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            dateTimePicker1.Value = DateTime.Now; // Reset date picker to current date

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = textBox10.Text;

            // Валидируем ввод
            if (!int.TryParse(input, out int id))
            {
                MessageBox.Show("Пожалуйста, введите корректный целочисленный идентификатор.", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Создаем соединение с базой данных и команду
            string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM bloodsamples WHERE id = @id", conn))
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
            }
            textBox10.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
