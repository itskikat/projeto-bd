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

namespace WindowsFormsApp1
{
    public partial class AddSupplier : Form
    {
        public AddSupplier()
        {
            InitializeComponent();
        }

        // AQUI
        private void AdicionarFornecedor(object sender, EventArgs e)
        {
            if (emailTextBox.Text.Trim().Length==0 ||
                nomeTextBox.Text.Trim().Length == 0 ||
                faxTextBox.Text.Trim().Length==0 ||
                nifTextBox.Text.Trim().Length ==0 ||
                TipoBox.SelectedItem.ToString() == null

                )
            {
                MessageBox.Show("Ha campo(s) vazio(s)!");
                return;
            }

            FornecedorService fornecedorService = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
            Fornecedor fornecedor = new Fornecedor(this.nomeTextBox.Text, this.nifTextBox.Text);
            fornecedor.FornecedorNome = this.nomeTextBox.Text;
            fornecedor.FornecedorNIF = this.nifTextBox.Text;
            fornecedor.FornecedorEmail = this.emailTextBox.Text;
            fornecedor.FornecedorFax = this.faxTextBox.Text;
            fornecedor.FornecedorTipo = this.TipoBox.SelectedItem.ToString();

            int result = fornecedorService.CreateNewFornecedor(fornecedor);
            if (result > 0)
            {
                MessageBox.Show("Fornecedor adicionado com sucesso!");
                Main2 main2 = new Main2();
                Dispose();
                main2.StartPosition = FormStartPosition.CenterScreen;
                main2.ShowDialog();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
