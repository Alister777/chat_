﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;


namespace WindowsFormsApplication1
{
    public partial class Chat : Form
    {
        public string ser;
        public string uname;
        public string pass;
        public DateTime lastCheck;
        
        public bool isConnected = false;

        public string connetionString;
        public string nickname = null;
        public Chat()
        {

            InitializeComponent();
            //
            //this.Text = lastCheck.ToString("yyyy-MM-dd HH:mm:ss");
           
        }

        public void refresh(DateTime last)
        {
            
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                string query = string.Format("SELECT * FROM Chat WHERE TIME > CONVERT(DATETIME, '{0}') ORDER BY TIME", last.ToString("yyyy-MM-dd HH:mm:ss"));
                //lastCheck = DateTime.UtcNow;
                SqlCommand com = new SqlCommand(query, cnn);
                try
                {
                    cnn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        //tbChat.Clear();
                        lastCheck = DateTime.UtcNow;
                        //
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                tbChat.AppendText(dr["TIME"].ToString() + " " + dr["NAME"].ToString() + ": " + dr["MESSAGE"].ToString() + "\n");
                            }
                        }
                    }
                    cnn.Close();
                   //

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! ");
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nickname != null)
            {
                string name = nickname;
                string mes = tbSend.Text;
                //tbChat.AppendText(DateTime.Now.ToString() + " " + nickname + ": " + tbSend.Text + "\n");
                string query = string.Format("INSERT INTO Chat" +
                                                    " VALUES(0, '{0}', '{1}', CONVERT(DATETIME, GETUTCDATE()), 1)", name, mes);
                using (SqlConnection cnn = new SqlConnection(connetionString))
                {
                    SqlCommand com = new SqlCommand(query, cnn);
                    try
                    {
                        cnn.Open();
                        com.ExecuteNonQuery();

                        cnn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can not open connection ! ");
                    }
                    tbSend.Clear();
                    
                }
            }
            else MessageBox.Show("Введите ник!");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            nickname = tbName.Text;
            tbName.Enabled = false;
            button3.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string query = string.Format("DELETE FROM Chat");
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                SqlCommand com = new SqlCommand(query, cnn);
                try
                {
                    cnn.Open();
                    com.ExecuteNonQuery();

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! ");
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            DialogResult dr = new DialogResult();
            dr = login.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ser = login.server;
                uname = login.username;
                pass = login.pass;
            }
            connetionString = @"Data Source="+ser+
                ",1433;Network Library = DBMSSOCN; Initial Catalog = u453901;User ID ="+
                uname+"; Password ="+pass;
            isConnected = true; lastCheck = DateTime.UtcNow;


        }

        private void Chat_Load(object sender, EventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 3000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }
        void t_Tick(object sender, EventArgs e)
        {
            if (isConnected)
            {
                //(sender as Timer).Stop();
                refresh(lastCheck);
            }


        }
    }
}
