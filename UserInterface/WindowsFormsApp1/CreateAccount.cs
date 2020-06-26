using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataBaseManager;
using DTOs;
using Factorys;
using Services;

namespace WindowsFormsApp1
{
    public partial class CreateAccount : Form
    {
        private SqlConnection cn;

        public CreateAccount()
        {
            InitializeComponent();
            this.cn = ConnectionUtils.GetConnection();
         }

        private void Create_Account(object sender, EventArgs e)
        {
            if (emailTextBox.Text.Trim().Length == 0 ||
                nomeTextBox.Text.Trim().Length == 0 ||
                telefoneTextBox.Text.Trim().Length == 0 ||
                utilizadorTextBox.Text.Trim().Length == 0 ||
                passwordTextBox.Text.Trim().Length == 0
                )
            {
                MessageBox.Show("Ha campo(s) vazio(s)!");
                return;
            }

            UtilizadorService utilizadorService = (UtilizadorService)ServiceFactory.GetInstance("UtilizadorService");
            Pessoa pessoa = new Pessoa
            {
                PersonNome = nomeTextBox.Text,
                PersonEmail = emailTextBox.Text,
                PersonTelefone = telefoneTextBox.Text,
                PersonGenero = GeneroBox.SelectedItem.ToString(),
                PersonIdade = (int)IdadeBox.Value
            };

            int result = utilizadorService.CreateNewUser(pessoa, utilizadorTextBox.Text, passwordTextBox.Text);
            if (result > 0)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
