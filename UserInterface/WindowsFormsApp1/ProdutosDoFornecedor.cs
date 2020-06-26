using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using Factorys;
using Services;

namespace WindowsFormsApp1
{
    public partial class ProdutosDoFornecedor : Form
    {

        private string fornecedor;
        public ProdutosDoFornecedor(string fornecedor)
        {
            this.fornecedor = fornecedor;
            InitializeComponent();
            InitListViews();
            FillList();
            InitListViewWidth();
        }

        public void InitListViews()
        {
            ProdutoListView.View = View.Details;
            ProdutoListView.FullRowSelect = true;
            ProdutoListView.Columns.Add("ID");
            ProdutoListView.Columns.Add("Nome");
            ProdutoListView.Columns.Add("Tipo");
            ProdutoListView.Columns.Add("Preco");
            ProdutoListView.Columns.Add("Iva");
            ProdutoListView.Columns.Add("Marca");
        }

        public void InitListViewWidth()
        {
            this.ProdutoListView.Columns[0].Width = 120;
            this.ProdutoListView.Columns[1].Width = -1;
            this.ProdutoListView.Columns[2].Width = 120;
            this.ProdutoListView.Columns[3].Width = 120;
            this.ProdutoListView.Columns[4].Width = -1;
            this.ProdutoListView.Columns[5].Width = 150;
        }

        public void FillList()
        {
            ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
            List<Produto> list = ps.GetProdutosByFornecedorId(fornecedor);
            foreach (Produto produto in list)
            {
                ListViewItem item = new ListViewItem(produto.Codigo.ToString());
                item.SubItems.Add(produto.Nome.ToString());
                item.SubItems.Add(produto.Tipo.ToString());
                item.SubItems.Add(produto.Preco.ToString());
                item.SubItems.Add(produto.Iva.ToString());
                item.SubItems.Add(produto.Marca.ToString());
                this.ProdutoListView.Items.Add(item);
            }
        }

        private void Comprar_Click(object sender, EventArgs e)
        {
            if (this.ProdutoListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecionar o que pretende comprar!");
            }
            else
            {
                string codigo = ProdutoListView.FocusedItem.SubItems[0].Text;
                Produto produto = new Produto(int.Parse(codigo));
                produto.Nome = ProdutoListView.FocusedItem.SubItems[1].Text;
                produto.Tipo = ProdutoListView.FocusedItem.SubItems[2].Text;
                produto.Preco = double.Parse(this.ProdutoListView.FocusedItem.SubItems[3].Text);
                produto.Iva = new IVA(double.Parse(this.ProdutoListView.FocusedItem.SubItems[4].Text.Split(new char[] { '%' })[0]));
                produto.Marca = ProdutoListView.FocusedItem.SubItems[5].Text;
                produto.Quantidade = (int)ProductQuantity.Value;
                ProductQuantity.Value = 1;
                ForneceService fs = (ForneceService)ServiceFactory.GetInstance("ForneceService");
                
                int result = fs.ForneceProduto(produto,fornecedor);
                if (result > 0)
                {
                    MessageBox.Show("Produto comprado!");
                }
            }
        }
    }
}
