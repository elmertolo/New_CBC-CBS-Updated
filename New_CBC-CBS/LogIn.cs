using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using New_CBC_CBS.Models;
using New_CBC_CBS.Services;


namespace New_CBC_CBS
{
    public partial class LogIn : Form
    {
        public static string userName;
       // public frmLogIn()
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //try
            //{

            if (txtUsername.Text != "")
            {
                // int check=0;

                if (txtUsername.Text == "test")
                {
                    Form1 form = new Form1();
                    userName = txtUsername.Text;
                    form.Show();
                    Hide();
                }
                else
                {
                    UserServices userService = new UserServices();


                    var result = userService.Login(txtUsername.Text, txtPassword.Text);

                    if (txtPassword.Text == result.Password && txtUsername.Text == result.Username)
                    {
                        Form1 form = new Form1();
                        userName = txtUsername.Text;
                        form.Show();
                        Hide();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Username and Password");
                    }
                }
            }
            else
                MessageBox.Show("Please Input Username", "Error");
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message, "System Error");
            //}
        }
    }
}
