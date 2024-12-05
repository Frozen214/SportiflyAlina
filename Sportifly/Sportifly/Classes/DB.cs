using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class DB
    {
        //Коннект к MS SQL SERVER
        public string StringConn = @"Data Source=DESKTOP-KFHRIFS\MSSQLSERVER01;Initial Catalog=Спортивный_комплекс;Integrated Security=True";

        public SqlDataAdapter Execute(string query)
        {
            try
            {
                using (SqlConnection myCon = new SqlConnection(StringConn))
                {
                    myCon.Open();
                    if (myCon.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Не удалось установить подключение к базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(query, myCon);
                    sda.SelectCommand.ExecuteNonQuery();
                    return sda;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable ReturnData(string query, DataGridView grid)
        {
            try
            {
                using (SqlConnection myCon = new SqlConnection(StringConn))
                {
                    myCon.Open();
                    if (myCon.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Не удалось установить подключение к базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }

                    using (SqlDataAdapter sda = new SqlDataAdapter(query, myCon))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        grid.DataSource = dt;
                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Возникла ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool FillComboBox(string query, string nameColumn, Guna2ComboBox comboBox)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StringConn))
                {
                    connection.Open();
                    comboBox.Items.Clear();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal(nameColumn)))
                                {
                                    comboBox.Items.Add(reader[nameColumn].ToString());
                                }
                            }
                        }
                    }
                    if (comboBox.Items.Count > 0)
                    {
                        comboBox.SelectedIndex = 0;
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL при заполнении {comboBox}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при заполнении {comboBox}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public string GetSignleValue(string query, string nameColumn)
        {
            string value = null;

            using (SqlConnection connection = new SqlConnection(StringConn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                value = reader[nameColumn].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка чтения {nameColumn}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return value;
        }
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (SqlConnection myCon = new SqlConnection(StringConn))
                {
                    myCon.Open();
                    if (myCon.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Не удалось установить подключение к базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }

                    using (SqlCommand command = new SqlCommand(query, myCon))
                    {
                        // Add parameters to the command if they exist
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        // Execute the command and return the number of rows affected
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}


