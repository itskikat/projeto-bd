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
    public partial class ProdutosDaEncomenda : Form
    {
        private int encomendaID;
        public ProdutosDaEncomenda(int EncomendaId)
        {
            this.encomendaID = EncomendaId;
            InitializeComponent();
            InitListViews();
            FillList();
            InitListViewWidth();
        }

        public void InitListViews()
        {
            this.ProdutoListView.View = View.Details;
            this.ProdutoListView.FullRowSelect = true;
            this.ProdutoListView.Columns.Add("ID");
            this.ProdutoListView.Columns.Add("Nome");
            this.ProdutoListView.Columns.Add("Preco");
            this.ProdutoListView.Columns.Add("Iva");
            this.ProdutoListView.Columns.Add("Marca");
            this.ProdutoListView.Columns.Add("Quantidade");
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
            List<Produto> list= ps.GetProdutosByEncomendaId(this.encomendaID);
            foreach (Produto produto in list)
            {
                ListViewItem item = new ListViewItem(produto.Codigo.ToString());
                item.SubItems.Add(produto.Nome.ToString());
                item.SubItems.Add(produto.Preco.ToString());
                item.SubItems.Add(produto.Iva.ToString());
                item.SubItems.Add(produto.Marca.ToString());
                item.SubItems.Add(produto.Quantidade.ToString());
                this.ProdutoListView.Items.Add(item);
            }
        }
    }
}
