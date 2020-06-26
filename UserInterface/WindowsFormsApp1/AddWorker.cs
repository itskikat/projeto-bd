using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataBaseManager;
using DTOs;
using Factorys;
using Services;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class AddWorker : Form
    {

        public AddWorker()
        {
            InitializeComponent();
            GeneroBox.SelectedIndex = 0;
        }

        private void AdicionarFuncionario(object sender, EventArgs e)
        {
            if (emailTextBox.Text.Trim().Length ==0 ||
                nomeTextBox.Text.Trim().Length == 0 ||
                telefoneTextBox.Text.Trim().Length ==0
                )
            {
                MessageBox.Show("Ha campo(s) vazio(s)!");
                return;
            }

            FuncionarioService funcionarioService = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
            Pessoa pessoa = new Pessoa
            {
                PersonNome = nomeTextBox.Text,
                PersonEmail = emailTextBox.Text,
                PersonTelefone = telefoneTextBox.Text,
                PersonGenero = GeneroBox.SelectedItem.ToString(),
                PersonIdade = (int)IdadeBox.Value,
                PersonSalario = (double)salarioTextBox.Value
            };

            if (cartaTextBox.Text.Trim().Length != 0 && cartaTextBox.Text != "(opcional)")
            {
                CondutorService condutorService = (CondutorService)ServiceFactory.GetInstance("CondutorService");
                Condutor condutor = new Condutor(pessoa.PersonID, cartaTextBox.Text);
                int res = condutorService.CreateNewCondutor(pessoa, cartaTextBox.Text);
                if (res > 0)
                {
                    MessageBox.Show("Condutor inserido com sucesso!");
                    Main2 main2 = new Main2();
                    this.Dispose();
                    main2.StartPosition = FormStartPosition.CenterScreen;
                    main2.ShowDialog();
                }
            }
            else
            {
                int result = funcionarioService.CreateNewFuncionario(pessoa);
                if (result > 0)
                {
                    MessageBox.Show("Funcionario inserido com sucesso!");
                    Main2 main2 = new Main2();
                    this.Dispose();
                    main2.StartPosition = FormStartPosition.CenterScreen;
                    main2.ShowDialog();
                }
            }
           
        }

        private void cartaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cartaTextBox.Text.EndsWith("(opcional)"))
            {
                cartaTextBox.ForeColor = Color.Black;
                cartaTextBox.Text = cartaTextBox.Text.Substring(0, cartaTextBox.Text.Length - 10);
            }
        }
    }
}
