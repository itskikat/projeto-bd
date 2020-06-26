using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DataBaseManager;
using DTOs;
using Factorys;
using Services;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public bool admin = false;
        public int clienteId = -1;
        public string username;

        public Login()
        {
            InitializeComponent();
        }

        private void login(object sender, EventArgs e)
        {
            if(EmailBox.Text==""|| pwdBox.Text == "")
            {
                MessageBox.Show("Nome ou Senha está vazio!");
                return;
            }

            UtilizadorService utilizadorService = (UtilizadorService)ServiceFactory.GetInstance("UtilizadorService");
            Utilizador utilizador = utilizadorService.CheckIsExiste(EmailBox.Text.Trim(),pwdBox.Text);
            if (utilizador == null ||(utilizador.PersonID==0&&utilizador.UserName.Equals("unkown")))
            {
                MessageBox.Show("Nome ou Senha está incorreto!");
                return;
            }
            else
            {
                if (EmailBox.Text.Equals("admin@ua.pt") && utilizador.UserName.Equals("admin")) //admin
                {
                    admin = true;
                    username = "admin";
                    Hide();
                    Main2 main2 = new Main2();
                    main2.StartPosition = FormStartPosition.CenterParent;
                    main2.Owner = this;
                    main2.ShowDialog();
                    Application.ExitThread();
                    Dispose();
                }
                else
                {
                    clienteId = utilizador.PersonID;
                    username = utilizador.UserName;
                    Hide();
                    Main main = new Main(clienteId, username) ;
                    main.StartPosition = FormStartPosition.CenterParent;
                    main.Owner = this;
                    main.ShowDialog();
                    Application.ExitThread();
                    Dispose();
                }
                DialogResult = DialogResult.OK;
                Dispose();
            }
        }

        private void nova_conta(object sender, EventArgs e)
        {
            CreateAccount ca = new CreateAccount();
            ca.StartPosition = FormStartPosition.CenterScreen;
            ca.ShowDialog();
            if (ca.DialogResult == DialogResult.OK)
                EmailBox.Text = ca.emailTextBox.Text;
        }
    }
}
