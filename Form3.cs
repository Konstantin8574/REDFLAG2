using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace REDFLAG
{
    public partial class Form3 : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";
        public Form3()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM blooddonors";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();


            // Create a command to add data to the table
            string query = "INSERT INTO `blooddonors` (`id`, `full_name`, `birth_date`, `passport_number`, `gender`, `phone_number`, `rh_factor_id`)" +
                "VALUES (@id, @full_name, @birth_date, @passport_number, @gender, @phone_number, @rh_factor_id)";

            MySqlCommand command = new MySqlCommand(query, connection);
            

            

            DateTime birthDate = dateTimePicker1.Value;



            command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
            // Add parameters for the command
            command.Parameters.AddWithValue("@full_name", textBox2.Text);

                // Дата рождения была успешно преобразована
            command.Parameters.AddWithValue("@birth_date", dateTimePicker1.Value);

            command.Parameters.AddWithValue("@passport_number", textBox3.Text);
            command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@phone_number", textBox4.Text);
            command.Parameters.AddWithValue("@rh_factor_id", int.Parse(textBox5.Text));

            // Execute the command
            command.ExecuteNonQuery();
              
            // Update the data in DataGridView
            string selectQuery = "SELECT * FROM blooddonors";
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            connection.Close();
            // Очищаем текстовые поля
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now; // Reset date picker to current date
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Получаем ввод из textBox6
            string input = textBox6.Text;

            // Валидируем ввод
            if (!int.TryParse(input, out int id))
            {
                MessageBox.Show("Пожалуйста, введите корректный целочисленный идентификатор.", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Создаем соединение с базой данных и команду
            string connectionString = "Server=localhost;Database=authorization_registration;User ID=root;Password=fs!if3nj#wkr32jrf/9/329fjwfgsgf3t@r23#8t32trh32ht2!3@ht832532";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM blooddonors WHERE id = @id", conn))
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

            // Очищаем textBox6
            textBox6.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
