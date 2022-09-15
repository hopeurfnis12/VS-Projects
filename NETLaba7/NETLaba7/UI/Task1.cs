﻿using NETLaba7.UI;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NETLaba7
{
	public partial class Task1 : Form
	{
		public Task1()
		{
			InitializeComponent();
		}

		private void Task1_Load(object sender, EventArgs e)
		{
            string connStr = "server=localhost; port=3306; username=root; password=root; database=university;";
            string sql = "SELECT DISTINCT city FROM lectures;";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listBox1.Items.Add(reader["city"].ToString());
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error", ex.ToString());
            }
            finally
            {
                command.Connection.Close();
            }
        }

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (listBox1.SelectedIndex != -1)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                this.Text = listBox1.SelectedItem.ToString();
                //MessageBox.Show(listBox1.SelectedItem.ToString());
                //"SERVER=" + server + ";" + "DATABASE=" +
                //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                string connStr = "server=127.0.1; port=3306; database=university; user id=root;";
                string sql = "SELECT surname, name, univ_name FROM lectures, univ " +
                    "where lectures.city = '" + listBox1.SelectedItem.ToString() + "' AND lectures.univ_id = univ.univ_id;";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader;
                try
                {
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    this.dataGridView1.Columns.Add("surname", "Surname");
                    this.dataGridView1.Columns["surname"].Width = 100;
                    this.dataGridView1.Columns.Add("name", "Name");
                    this.dataGridView1.Columns["name"].Width = 100;
                    this.dataGridView1.Columns.Add("univ_name", "University Name");
                    this.dataGridView1.Columns["univ_name"].Width = 150;
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["surname"].ToString(), reader["name"].ToString(), reader["univ_name"].ToString());
                    }
                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error", ex.ToString());
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
	}
}