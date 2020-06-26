using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DTOs;
using Services;
using Factorys;
using DataBaseManager;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        private Pagination<Produto> produtosOfProdutoListView;
        private Pagination<Produto> carrinho;
        private Pagination<Encomenda> encomendasOfEncomendasListView;
        private Pagination<Fatura> faturasOfFaturasListView;
        private string username;
        private Cliente cliente;

        public Main(int clientId,string username)
        {
            this.username = username;
            carrinho = new Pagination<Produto>();
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            Form1_Load(clientId); 
            Width = 1000;
        }

        private void Form1_Load(int clientId)
        {
            Width = 872;
            PageBox.Text = "1";
            PageBoxCarrinho.Text = "1";
            PageBoxEncomenda.Text = "1";
            PageBoxFatura.Text = "1";
            InitListViews(1);
            InitProdutcList(1);
            GetCliente(clientId);
            InitMarcaTipo();
            InitListViewsWidth(1);
            UserName.Text = username;
        }

        private void InitMarcaTipo()
        {
            MarcaService cs = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            List<Marca> marcas = cs.GetMarcaList();
            foreach (Marca marca in marcas)
            {
                SearchByBrandBox.Items.Add(marca.Nome);
            }
            SearchByBrandBox.SelectedIndex = 0;

            TipoService ts = (TipoService)ServiceFactory.GetInstance("TipoService");
            List<Tipo> tipos = ts.GetTipoList();
            foreach (Tipo tipo in tipos)
            {
                SearchByTypeBox.Items.Add(tipo.Nome);
            }
            SearchByTypeBox.SelectedIndex = 0;
        }
        private void GetCliente(int id)
        {
            ClienteService clienteService = (ClienteService) ServiceFactory.GetInstance("ClienteService");
            this.cliente = clienteService.GetClienteById(id);
        }

        private void InitListViews(int index)
        {
            if (index == 1)
            {
                if (this.ProdutoListView.Columns.Count > 1)
                {
                    return;
                }
                ProdutoListView.View = View.Details;
                ProdutoListView.FullRowSelect = true;
                ProdutoListView.Columns.Add("ID");
                ProdutoListView.Columns.Add("Nome");
                ProdutoListView.Columns.Add("Tipo");
                ProdutoListView.Columns.Add("Preco");
                ProdutoListView.Columns.Add("Iva");
                ProdutoListView.Columns.Add("Marca");
                ProdutoListView.Columns.Add("Quantidade");
            }
            else if (index == 2)
            {
                if (CarrinhoListView.Columns.Count > 1)
                {
                    return;
                }
                CarrinhoListView.View = View.Details;
                CarrinhoListView.FullRowSelect = true;
                CarrinhoListView.Columns.Add("ID");
                CarrinhoListView.Columns.Add("Nome");
                CarrinhoListView.Columns.Add("Preco");
                CarrinhoListView.Columns.Add("Iva");
                CarrinhoListView.Columns.Add("Marca");
                CarrinhoListView.Columns.Add("Quantidade");
            }else if (index == 3)
            {
                if (EncomendasListView.Columns.Count > 1)
                {
                    return;
                }
                EncomendasListView.View = View.Details;
                EncomendasListView.FullRowSelect = true;
                EncomendasListView.Columns.Add("ID", 120, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("Funcionario", 120, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("Condicao Pagamento", -1, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("Destino", -1, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("CustoTotal", 150, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("Estado", -1, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("Pago", -2, HorizontalAlignment.Left);
                EncomendasListView.Columns.Add("Data", -1, HorizontalAlignment.Left);
            }else if (index == 4)
            {
                if (this.FaturasListView.Columns.Count > 1)
                {
                    return;
                }
                FaturasListView.View = View.Details;
                FaturasListView.FullRowSelect = true;
                FaturasListView.Columns.Add("ID", 120, HorizontalAlignment.Left);
                FaturasListView.Columns.Add("Encomenda", 100, HorizontalAlignment.Left);
                FaturasListView.Columns.Add("Funcionario", 120, HorizontalAlignment.Left);
                FaturasListView.Columns.Add("Condicao Pagamento", 120, HorizontalAlignment.Left);
                FaturasListView.Columns.Add("CustoTotal", 150, HorizontalAlignment.Left);
                FaturasListView.Columns.Add("Data", 100, HorizontalAlignment.Left);
            }
            
        }
        private void InitListViewsWidth(int index)
        {
            if (index == 1)
            {

                if (this.ProdutoListView.Items.Count == 0)
                {
                    ProdutoListView.Columns[0].Width = -2;
                    ProdutoListView.Columns[1].Width = -2;
                    ProdutoListView.Columns[2].Width = -2;
                    ProdutoListView.Columns[3].Width = -2;
                    ProdutoListView.Columns[4].Width = -2;
                    ProdutoListView.Columns[5].Width = -2;
                    ProdutoListView.Columns[6].Width = -2;
                }
                else
                {
                    ProdutoListView.Columns[0].Width = 120;
                    ProdutoListView.Columns[1].Width = -1;
                    ProdutoListView.Columns[2].Width = -1;
                    ProdutoListView.Columns[2].Width = 120;
                    ProdutoListView.Columns[3].Width = 120;
                    ProdutoListView.Columns[4].Width = 120;
                    ProdutoListView.Columns[5].Width = -1;
                    ProdutoListView.Columns[6].Width = -2;
                }
            }
            else if (index == 2)
            {
                if (CarrinhoListView.Items.Count == 0)
                {
                    CarrinhoListView.Columns[0].Width = -2;
                    CarrinhoListView.Columns[1].Width = -2;
                    CarrinhoListView.Columns[2].Width = -2;
                    CarrinhoListView.Columns[3].Width = -2;
                    CarrinhoListView.Columns[4].Width = -2;
                    CarrinhoListView.Columns[5].Width = -2;
                }
                else
                {
                    CarrinhoListView.Columns[0].Width = 120;
                    CarrinhoListView.Columns[1].Width = -1;
                    CarrinhoListView.Columns[2].Width = 120;
                    CarrinhoListView.Columns[3].Width = 120;
                    CarrinhoListView.Columns[4].Width = -1;
                    CarrinhoListView.Columns[5].Width = 150;
                }
            }
            else if (index == 3)
            {
                if (EncomendasListView.Items.Count==0)
                {
                    EncomendasListView.Columns[0].Width = -2;
                    EncomendasListView.Columns[1].Width = -2;
                    EncomendasListView.Columns[2].Width = -2;
                    EncomendasListView.Columns[3].Width = -2;
                    EncomendasListView.Columns[4].Width = -2;
                    EncomendasListView.Columns[5].Width = -2;
                    EncomendasListView.Columns[6].Width = -2;
                    EncomendasListView.Columns[7].Width = -2;
                }
                else
                {
                    EncomendasListView.Columns[0].Width = 120;
                    EncomendasListView.Columns[1].Width = 120;
                    EncomendasListView.Columns[2].Width = -1;
                    EncomendasListView.Columns[3].Width = -1;
                    EncomendasListView.Columns[4].Width = -2;
                    EncomendasListView.Columns[5].Width = 150;
                    EncomendasListView.Columns[6].Width = 100;
                    EncomendasListView.Columns[7].Width = -1;
                }
            }
            else if (index == 4)
            {
                if (FaturasListView.Items.Count==0)
                {
                    FaturasListView.Columns[0].Width = -2;
                    FaturasListView.Columns[1].Width = -2;
                    FaturasListView.Columns[2].Width = -2;
                    FaturasListView.Columns[3].Width = -2;
                    FaturasListView.Columns[4].Width = -2;
                    FaturasListView.Columns[5].Width = -2;
                }
                else
                {
                    FaturasListView.Columns[0].Width = 120;
                    FaturasListView.Columns[1].Width = 100;
                    FaturasListView.Columns[2].Width = 120;
                    FaturasListView.Columns[3].Width = 120;
                    FaturasListView.Columns[4].Width = 100;
                    FaturasListView.Columns[5].Width = 100;
                }
            }

        }
        private void InitFaturasList(int clientID)
        {
            FaturaService fs = (FaturaService)ServiceFactory.GetInstance("FaturaService");
            Pagination<Fatura> list = fs.GetFaturaOfClient(clientID);
            FillFaturasPage(list);
        }

        private void FillFaturasPage(Pagination<Fatura> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Fatura>();

            this.faturasOfFaturasListView = pagination;

            this.PreviousFatura.Enabled = faturasOfFaturasListView.HasPrevious;
            this.FirstFatura.Enabled = faturasOfFaturasListView.ShowFirst;
            this.NextFatura.Enabled = faturasOfFaturasListView.HasNext;
            this.LastFatura.Enabled = faturasOfFaturasListView.ShowLast;
            
            foreach (Fatura fatura in faturasOfFaturasListView.Data)
            {
                ListViewItem fat = new ListViewItem(fatura.ID.ToString());
                fat.SubItems.Add(fatura.Encomenda.ToString());
                fat.SubItems.Add(fatura.Funcionario.ToString());
                fat.SubItems.Add(fatura.CondicoesPagamento.ToString());
                fat.SubItems.Add(fatura.CustoTotal.ToString());
                fat.SubItems.Add(fatura.Date.ToShortDateString().ToString());
                this.FaturasListView.Items.Add(fat);
            }
            InitListViewsWidth(4);
        }
        
        private void InitEncomendasList(int page,int clientID)
        {
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> list = es.GetEncomendaOfClient(page,clientID);
            FillEncomendasPage(list);
        }

        private void FillEncomendasPage(Pagination<Encomenda> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Encomenda>();

            this.encomendasOfEncomendasListView = pagination;

            this.PreviousEncomenda.Enabled = encomendasOfEncomendasListView.HasPrevious;
            this.FirstEncomenda.Enabled = encomendasOfEncomendasListView.ShowFirst;
            this.NextEncomenda.Enabled = encomendasOfEncomendasListView.HasNext;
            this.LastEncomenda.Enabled = encomendasOfEncomendasListView.ShowLast;
            foreach (Encomenda encomenda in encomendasOfEncomendasListView.Data)
            {
                ListViewItem enc = new ListViewItem(encomenda.EncomendaID.ToString());
                enc.SubItems.Add(encomenda.EncomendaFuncionario.ToString());
                enc.SubItems.Add(encomenda.EncomendaCondicoesPagamento.ToString());
                enc.SubItems.Add(encomenda.EncomendaDestino.ToString());
                enc.SubItems.Add(encomenda.EncomendaCustoTotal.ToString());
                enc.SubItems.Add(encomenda.EncomendaEstado.ToString().Equals("True")?"Entregue":"Por Entregar");
                enc.SubItems.Add(encomenda.Pago.ToString().Equals("True") ? "Sim" : "Nao");
                enc.SubItems.Add(encomenda.EncomendaDate.ToShortDateString().ToString());
                    
                EncomendasListView.Items.Add(enc);
            }
            InitListViewsWidth(3);
        }

        private void InitProdutcList(int page)
        {
            ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
            Pagination<Produto> pagination = ps.GetList(page);
            FillProdutosListView(pagination);
        }

        private void FillProdutosListView(Pagination<Produto> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Produto>();

            produtosOfProdutoListView = pagination;

            Previous.Enabled = produtosOfProdutoListView.HasPrevious;
            First.Enabled = produtosOfProdutoListView.ShowFirst;
            Next.Enabled = produtosOfProdutoListView.HasNext;
            Last.Enabled = produtosOfProdutoListView.ShowLast;

            foreach (Produto produto in pagination.Data)
            {
                ListViewItem item = new ListViewItem(produto.Codigo.ToString());
                item.SubItems.Add(produto.Nome);
                item.SubItems.Add(produto.Tipo);
                item.SubItems.Add(produto.Preco.ToString());
                item.SubItems.Add(produto.Iva.ToString());
                item.SubItems.Add(produto.Marca);
                item.SubItems.Add(produto.Quantidade.ToString());
                ProdutoListView.Items.Add(item);
            }
        }

        private void AddToCarrinho(object sender, EventArgs e)
        {
            if (ProdutoListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecionar o que pretende comprar!");
            }
            else
            {
                int quantidade = int.Parse(ProdutoListView.FocusedItem.SubItems[6].Text);
                if (ProductQuantity.Value>quantidade)
                {
                    MessageBox.Show("Numero maior do que a quantidade do produto disponivel!");
                }
                else
                {
                    string codigo = ProdutoListView.FocusedItem.SubItems[0].Text;
                    foreach(ListViewItem item in CarrinhoListView.Items)
                    {
                        if (item.SubItems[0].Text.Equals(codigo))
                        {
                            MessageBox.Show("O produto ja foi adicionado!");
                            ProdutoListView.SelectedItems.Clear();
                            return;
                        }
                    }
                    Produto produto = new Produto(int.Parse(codigo));
                    produto.Nome = ProdutoListView.FocusedItem.SubItems[1].Text;
                    produto.Tipo = ProdutoListView.FocusedItem.SubItems[2].Text;
                    produto.Preco = double.Parse(ProdutoListView.FocusedItem.SubItems[3].Text);
                    produto.Iva = new IVA(double.Parse(ProdutoListView.FocusedItem.SubItems[4].Text.Split(new char[]{ '%'})[0]));
                    produto.Marca = ProdutoListView.FocusedItem.SubItems[5].Text;
                    produto.Quantidade = (int) ProductQuantity.Value;

                    carrinho.Data.Add(produto);
                    carrinho.TotalPage = (carrinho.Data.Count / 11)+1;
                    InitListViews(2);
                    InitListViewsWidth(2);
                    FillCarrinho(carrinho.TotalPage); //mostra a ultima pagina
                    ProdutoListView.SelectedItems.Clear();
                    ProductQuantity.Value = 1;
                    Tab.SelectedTab = Tab.TabPages[1];
                }
            }

        }

        private void ProdutoListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProductQuantity.Value = 1;
                ProductQuantity.Maximum = int.Parse(ProdutoListView.FocusedItem.SubItems[6].Text);
            }
            catch (Exception) { }
        }

        private void TabChange(object sender, EventArgs e)
        {
            int index = Tab.SelectedIndex;
            switch (index)
            {
                case 0:
                    Width = 872;
              /*      if (!this.produtosOfProdutoListView.Search)
                    {
                        this.ProdutoListView.Items.Clear();
                        InitProdutcList(int.Parse(this.PageBox.Text));
                    }*/
                    break;
                case 1:
                    Width = 872;
                    InitListViews(2);
                    InitListViewsWidth(2);
                    int quantidadeTotal = 0;
                    double custoTotal = 0.0;
                    foreach (Produto produto in carrinho.Data)
                    {
                        int quantidade = produto.Quantidade;
                        double preco = produto.Preco;
                        quantidadeTotal += quantidade;
                        custoTotal += preco * quantidade;

                    }
                        
                    NomeBox.Text = cliente.PersonNome;
                    NIFBox.Text = cliente.ClientNIF.Equals("999999999")?"": cliente.ClientNIF;
                    CustoTotalBox.Text = custoTotal + "";
                    Quantidade.Text = quantidadeTotal + "";
                    TelefoneBox.Text = cliente.PersonTelefone;

                    if (carrinho.CurrentPage == 0)
                    {
                        FirstCarrinho.Enabled = false;
                        PreviousCarrinho.Enabled = false;
                        NextCarrinho.Enabled = false;
                        LastCarrinho.Enabled = false;
                    }
                    break;
                case 2:
                    VerBtn.Enabled = false;
                    InitListViews(3);
                    Width = 1100;
                    if (encomendasOfEncomendasListView == null || !encomendasOfEncomendasListView.Search)
                    {
                        EncomendasListView.Items.Clear();
                        InitEncomendasList(int.Parse(PageBox.Text),cliente.PersonID);
                    }
                    break;
                case 3:
                    verProdutoFaturaBtn.Enabled = false;
                    Width = 832;
                    InitListViews(4);
                    FaturasListView.Items.Clear();
                    InitFaturasList(cliente.PersonID);
                    break;

            };
        }

        private void DeleteProduct(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.CarrinhoListView.SelectedItems)
            {
                foreach(var v in carrinho.Data)
                {
                    if (v.Codigo == int.Parse(item.SubItems[0].Text))
                    {
                        carrinho.Data.Remove(v);
                        break;
                    }
                }
                item.Remove();
            }
            carrinho.TotalPage = (carrinho.Data.Count / 11) + 1;
            FillCarrinho(carrinho.CurrentPage);
            int quantidadeTotal = 0;
            double custoTotal = 0.0;
            foreach (ListViewItem item in this.CarrinhoListView.Items)
            {
                int quantidade = int.Parse(item.SubItems[5].Text); //quantidade
                double preco = double.Parse(item.SubItems[2].Text);
                quantidadeTotal += quantidade;
                custoTotal += preco * quantidade;
            }
            this.NomeBox.Text = this.cliente.PersonNome;
            this.NIFBox.Text = this.cliente.ClientNIF;
            this.CustoTotalBox.Text = custoTotal + "";
            this.Quantidade.Text = quantidadeTotal + "";
            this.CarrinhoListView.Refresh();
        }

        // AQUI
        // pedir fatura da encomenda
        private void PedirFatura(object sender, EventArgs e)
        {
            if (EncomendasListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecionar a encomenda que pretende fatura!");
            }
            else
            {
                foreach (ListViewItem encomenda in this.EncomendasListView.Items)
                {
                    if (encomenda.Selected)
                    {
                        int encomendaID = int.Parse(encomenda.SubItems[0].Text);
                        int cliente = this.cliente.PersonID;
                        int funcionario = int.Parse(encomenda.SubItems[1].Text);
                        DateTime data = DateTime.Parse(encomenda.SubItems[7].Text);
                        String condicaoPagamento = encomenda.SubItems[2].Text;
                        double total = double.Parse(encomenda.SubItems[4].Text);

                        string estado = encomenda.SubItems[5].Text;
                        if (estado.Equals("Entregue"))
                        {
                            Fatura fatura = new Fatura(encomendaID, cliente, funcionario, data, condicaoPagamento, total);
                            FaturaService fs = (FaturaService)ServiceFactory.GetInstance("FaturaService");
                            int res = fs.CreateFatura(fatura);
                            if (res != -1)
                                MessageBox.Show("Fatura emitida com sucesso!");
                            else
                                break;
                        }
                        else
                        {
                            MessageBox.Show("Nao e possivel emitir fatura! Encomenda a caminho!!");
                            return;
                        }


                    }
                }
            }
                       
            this.EncomendasListView.Refresh();
        }

        //finalizar a encomenda
        private void Finalizar(object sender, EventArgs e)
        {
            if (!Check())
                return;
            FuncionarioService funcionarioService = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
            //obter um funcionar ao calha
            Funcionario funcionario = funcionarioService.GetFuncionario();

            ClienteService clienteService = (ClienteService)ServiceFactory.GetInstance("ClienteService");
            cliente.ClientNIF = NIFBox.Text;
            int result = clienteService.UpdateNIF(cliente);

            if (result < 0)
                return;

            Encomenda encomenda = new Encomenda(cliente.PersonID,funcionario.PersonID,
               CondicaoBox.SelectedItem.ToString(),DestinoBox.Text);
            encomenda.EncomendaCustoTotal = double.Parse(CustoTotalBox.Text);
            encomenda.EncomendaEstado = false;

            List<Contem> contems = new List<Contem>();
            List<Armazena> armazenas = new List<Armazena>();
            foreach (ListViewItem item in CarrinhoListView.Items)
            {
                int id = int.Parse(item.SubItems[0].Text);
                int quantidade = int.Parse(item.SubItems[5].Text);
                double preco = double.Parse(item.SubItems[2].Text);
                Contem contem = new Contem(quantidade, encomenda.EncomendaID, id, preco);
                contems.Add(contem);
                Armazena armazena = new Armazena(id, quantidade);
                armazenas.Add(armazena);

            }

            EncomendaService encomendaService = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            int result2=encomendaService.CreateEncomenda(encomenda, contems,armazenas);
            if (result2 > 0)
            {
                MessageBox.Show("Encomenda criada!");
                Clear();
            }
            else
            {
                MessageBox.Show("O sistema encontrou um erro! A encomenda nao foi criada!");
                return;
            }
        }


        private bool Check()
        {
            if(CarrinhoListView.Items.Count==0)
            {
                MessageBox.Show("Carrinho vazio!");
                return false;
            }

            if (this.NomeBox.Text.Trim().Length == 0 ||
                this.NIFBox.Text.Trim().Length == 0 ||
                this.CustoTotalBox.Text.Trim().Length == 0 ||
                this.Quantidade.Text.Trim().Length == 0 ||
                this.TelefoneBox.Text.Trim().Length == 0 ||
                     this.DestinoBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ha campo(s) vazio(s)!");
                return false;
            }
            return true;
        }

        private void Clear()
        {
            this.CarrinhoListView.Items.Clear();
            this.CustoTotalBox.Text = "";
            this.DestinoBox.Text = "";
            this.CondicaoBox.SelectedIndex = 0;
            this.carrinho.Data.Clear();
            this.carrinho.CurrentPage = 1;
            this.carrinho.TotalPage = 1;
            this.FirstCarrinho.Enabled = false;
            this.PreviousCarrinho.Enabled = false;
            this.LastCarrinho.Enabled = false;
            this.NextCarrinho.Enabled = false;
        }

        private void FirstEnter(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Blue;
        }

        private void FirstLeave(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Black;
        }

        private void PreviousEnter(object sender, EventArgs e)
        {
            this.Previous.ForeColor = Color.Blue;
        }

        private void PreviousLeave(object sender, EventArgs e)
        {
            this.Previous.ForeColor = Color.Black;
        }

        private void CheckIsDigit(object sender, KeyPressEventArgs e)
        {
            int totalpage = produtosOfProdutoListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = this.PageBox.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (this.PageBox.Text.Trim().Length != 0)
            {
                if (int.Parse(this.PageBox.Text.Trim()) > totalpage)
                {
                    this.PageBox.Text="";
                    this.PageBox.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    this.ProdutoListView.Items.Clear();
                    if (produtosOfProdutoListView.Search == false)
                    {
                        InitProdutcList(int.Parse(PageBox.Text.Trim()));
                    }
                    else
                    {
                        ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
                        Pagination<Produto> pagination = ps.Search(int.Parse(PageBox.Text.Trim()), SearchByNameBox.Text.Trim(),
                            SearchByBrandBox.Text.Trim(), SearchByTypeBox.Text, (int)PriceMinBox.Value, (int)PriceMaxBox.Value);
                        
                        ProdutoListView.Items.Clear();
                        FillProdutosListView(pagination);
                    }
                }
            }
            
        }

        private void NextEnter(object sender, EventArgs e)
        {
            this.Next.ForeColor = Color.Blue;
        }

        private void NextLeave(object sender, EventArgs e)
        {
            this.Next.ForeColor = Color.Black;
        }

        private void LastEnter(object sender, EventArgs e)
        {
            this.Last.ForeColor = Color.Blue;
        }

        private void LastLeave(object sender, EventArgs e)
        {
            this.Last.ForeColor = Color.Black;
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            this.PageBox.Text = this.produtosOfProdutoListView.CurrentPage - 1 + "";
            this.ProdutoListView.Items.Clear();
            if (this.produtosOfProdutoListView.Search == false)
            {
                InitProdutcList(int.Parse(this.PageBox.Text));
            }
            else
            {
                ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
                Pagination<Produto> pagination = ps.Search(int.Parse(PageBox.Text), SearchByNameBox.Text.Trim(),
                    SearchByBrandBox.Text.Trim(), SearchByTypeBox.Text, (int)PriceMinBox.Value, (int)PriceMaxBox.Value);
                this.ProdutoListView.Items.Clear();
                FillProdutosListView(pagination);
            }
            
        }

        private void First_Click(object sender, EventArgs e)
        {
            this.PageBox.Text = "1";
            this.ProdutoListView.Items.Clear();
            if (this.produtosOfProdutoListView.Search == false)
            {
                InitProdutcList(1);
            }
            else
            {
                ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
                Pagination<Produto> pagination = ps.Search(1, SearchByNameBox.Text.Trim(),
                    SearchByBrandBox.Text.Trim(),SearchByTypeBox.Text, (int)PriceMinBox.Value, (int)PriceMaxBox.Value);
                this.ProdutoListView.Items.Clear();
                FillProdutosListView(pagination);
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            this.PageBox.Text = this.produtosOfProdutoListView.CurrentPage + 1 + "";
            this.ProdutoListView.Items.Clear();
            if (this.produtosOfProdutoListView.Search == false)
            {
                InitProdutcList(int.Parse(this.PageBox.Text));
            }
            else
            {
                ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
                Pagination<Produto> pagination = ps.Search(int.Parse(PageBox.Text), SearchByNameBox.Text.Trim(),
                    SearchByBrandBox.Text.Trim(), SearchByTypeBox.Text, (int)PriceMinBox.Value, (int)PriceMaxBox.Value);
                this.ProdutoListView.Items.Clear();
                FillProdutosListView(pagination);
            }
        }

        private void Last_Click(object sender, EventArgs e)
        {
            this.PageBox.Text = this.produtosOfProdutoListView.TotalPage + "";
            this.ProdutoListView.Items.Clear();
            if (this.produtosOfProdutoListView.Search == false)
            {
                InitProdutcList(int.Parse(PageBox.Text));
            }
            else
            {
                ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
                Pagination<Produto> pagination = ps.Search(int.Parse(PageBox.Text), SearchByNameBox.Text.Trim(),
                    SearchByBrandBox.Text.Trim(), SearchByTypeBox.Text, (int)PriceMinBox.Value, (int)PriceMaxBox.Value);
                FillProdutosListView(pagination);
            }
        }

        private void StartSearch(object sender, EventArgs e)
        {
            PageBox.Text = "1";
            ProdutoService ps = (ProdutoService)ServiceFactory.GetInstance("ProdutoService");
            Pagination<Produto> pagination = ps.Search(-1, SearchByNameBox.Text.Trim(),
                    SearchByBrandBox.Text.Trim(), SearchByTypeBox.Text, (int)PriceMinBox.Value, (int)PriceMaxBox.Value);
            ProdutoListView.Items.Clear();
            FillProdutosListView(pagination);
        }
        private void TodosProdutos_Click(object sender, EventArgs e)
        {
            PageBox.Text = "1";
            ProdutoListView.Items.Clear();
            InitProdutcList(1);
        }

        private void FirstCarrinho_Click(object sender, EventArgs e)
        {
            FillCarrinho(1);
            PageBoxCarrinho.Text = "1";
            carrinho.CurrentPage = 1;
        }

        private void PreviousCarrinho_Click(object sender, EventArgs e)
        {
            PageBoxCarrinho.Text = this.carrinho.CurrentPage - 1 + "";
            FillCarrinho(--this.carrinho.CurrentPage);
        }

        private void PageBoxCarrinho_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = carrinho.Data.Count;

            if (e.KeyChar != '\b')
            {
                int len = this.PageBox.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (this.PageBox.Text.Trim().Length != 0)
            {
                if (int.Parse(this.PageBox.Text.Trim()) > totalpage)
                {
                    this.PageBox.Text = "";
                    this.PageBox.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    this.CarrinhoListView.Items.Clear();
                    FillCarrinho(int.Parse(this.PageBox.Text.Trim()));
                }
            }
        }

        private void NextCarrinho_Click(object sender, EventArgs e)
        {
            this.PageBoxCarrinho.Text = this.carrinho.CurrentPage + 1 + "";
            FillCarrinho(++this.carrinho.CurrentPage); 
        }

        private void LastCarrinho_Click(object sender, EventArgs e)
        {
            this.PageBoxCarrinho.Text = carrinho.TotalPage + "";
            this.carrinho.CurrentPage = carrinho.TotalPage;
            FillCarrinho(carrinho.TotalPage);
        }
        private void FillCarrinho(int page)
        {
            if (page > carrinho.TotalPage)
            {
                page = carrinho.TotalPage;
            }else if (page < 1)
            {
                page = 1;
            }

            this.carrinho.CurrentPage = page;
            this.FirstCarrinho.Enabled = this.carrinho.CurrentPage != 1;
            this.PreviousCarrinho.Enabled = this.carrinho.CurrentPage > 1;
            this.LastCarrinho.Enabled = this.carrinho.CurrentPage != carrinho.TotalPage;
            this.NextCarrinho.Enabled = this.carrinho.CurrentPage < carrinho.TotalPage;

            this.PageBoxCarrinho.Text = page + "";

            this.CarrinhoListView.Items.Clear();
            for(int i =(page-1)*10;i<page*10;i++)
            {
                if (i == carrinho.Data.Count)
                {
                    break;
                }
                Produto produto = carrinho.Data[i];
                ListViewItem item1 = new ListViewItem(produto.Codigo + "");
                item1.SubItems.Add(produto.Nome);
                item1.SubItems.Add(produto.Preco + "");
                item1.SubItems.Add(produto.Iva.ToString());
                item1.SubItems.Add(produto.Marca);
                item1.SubItems.Add(produto.Quantidade + "");
                this.CarrinhoListView.Items.Add(item1);
            }
            InitListViewsWidth(2);
        }

        private void FirstCarrinho_MouseEnter(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Blue;
        }

        private void FirstCarrinho_MouseLeave(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Black;
        }

        private void PreviousCarrinho_MouseEnter(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Blue;
        }

        private void PreviousCarrinho_MouseLeave(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Black;
        }

        private void NextCarrinho_MouseEnter(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Blue;
        }

        private void NextCarrinho_MouseLeave(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Black;
        }

        private void LastCarrinho_MouseEnter(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Blue;
        }

        private void LastCarrinho_MouseLeave(object sender, EventArgs e)
        {
            this.First.ForeColor = Color.Black;
        }

        private void FirstEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = "1";
            EncomendasListView.Items.Clear();
            if (encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(1,cliente.PersonID);
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(1,
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, cliente.PersonID);
                this.EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }

        private void PreviousEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = encomendasOfEncomendasListView.CurrentPage - 1 + "";
            EncomendasListView.Items.Clear();
            if (encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(int.Parse(PageBoxEncomenda.Text), cliente.PersonID);
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(int.Parse(this.PageBoxEncomenda.Text), 
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, this.cliente.PersonID);
                this.EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }

        private void NextEncomenda_Click(object sender, EventArgs e)
        {
            this.PageBoxEncomenda.Text = this.encomendasOfEncomendasListView.CurrentPage + 1 + "";
            this.EncomendasListView.Items.Clear();
            if (this.encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(int.Parse(this.PageBoxEncomenda.Text), this.cliente.PersonID);
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(int.Parse(this.PageBoxEncomenda.Text), 
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, this.cliente.PersonID);
                this.EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }

        private void LastEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = encomendasOfEncomendasListView.TotalPage+"";
            EncomendasListView.Items.Clear();
            if (encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(encomendasOfEncomendasListView.TotalPage, cliente.PersonID);
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(encomendasOfEncomendasListView.TotalPage,
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, cliente.PersonID);
                EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }

        private void FirstEncomenda_MouseEnter(object sender, EventArgs e)
        {
            FirstEncomenda.ForeColor = Color.Blue;
        }

        private void FirstEncomenda_MouseLeave(object sender, EventArgs e)
        {
            FirstEncomenda.ForeColor = Color.Black;
        }

        private void PreviousEncomenda_MouseEnter(object sender, EventArgs e)
        {
            this.PreviousEncomenda.ForeColor = Color.Blue;
        }

        private void PreviousEncomenda_MouseLeave(object sender, EventArgs e)
        {
            this.PreviousEncomenda.ForeColor = Color.Black;
        }

        private void NextEncomenda_MouseEnter(object sender, EventArgs e)
        {
            this.NextEncomenda.ForeColor = Color.Blue;
        }

        private void NextEncomenda_MouseLeave(object sender, EventArgs e)
        {
            this.NextEncomenda.ForeColor = Color.Black;
        }

        private void LastEncomenda_MouseEnter(object sender, EventArgs e)
        {
            this.LastEncomenda.ForeColor = Color.Blue;
        }

        private void LastEncomenda_MouseLeave(object sender, EventArgs e)
        {
            this.LastEncomenda.ForeColor = Color.Black;
        }

        private void PageBoxEncomenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = encomendasOfEncomendasListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = this.PageBoxEncomenda.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (this.PageBoxEncomenda.Text.Trim().Length != 0)
            {
                if (int.Parse(this.PageBoxEncomenda.Text.Trim()) > totalpage)
                {
                    this.PageBoxEncomenda.Text = "";
                    this.PageBoxEncomenda.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    EncomendasListView.Items.Clear();

                    if (encomendasOfEncomendasListView.Search == false)
                    {
                        InitEncomendasList(int.Parse(PageBoxEncomenda.Text.Trim()), cliente.PersonID);
                    }
                    else
                    {
                        if (encomendasOfEncomendasListView.Data.Count > 0)
                        {
                            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                            Pagination<Encomenda> pagination = es.GetEncomandasByEstado(int.Parse(PageBoxEncomenda.Text.Trim()),
                                encomendasOfEncomendasListView.Data[0].EncomendaEstado, cliente.PersonID);
                            EncomendasListView.Items.Clear();
                            FillEncomendasPage(pagination);
                        }
                    }
                }
            }

        }

        private void EncomendasListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(EncomendasListView.SelectedItems.Count>1 || EncomendasListView.SelectedItems.Count == 0)
            {
                VerBtn.Enabled = false;
                PagoBtn.Enabled = false;
            }
            else
            {
                VerBtn.Enabled = true;
            }

            if (EncomendasListView.SelectedItems.Count == 1 &&
                EncomendasListView.SelectedItems[0].SubItems[6].Text.Equals("Nao"))
                PagoBtn.Enabled = true;

        }

        private void NaoEntregados_Click(object sender, EventArgs e)
        {
            this.PageBoxEncomenda.Text = "1";
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomandasByEstado(-1,false, this.cliente.PersonID);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void Entregados_Click(object sender, EventArgs e)
        {
            this.PageBoxEncomenda.Text = "1";
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomandasByEstado(-1, true, this.cliente.PersonID);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void TodosEncomenda_Click(object sender, EventArgs e)
        {
            this.PageBoxEncomenda.Text = "1";
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomendaOfClient(1,this.cliente.PersonID);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void FirstFatura_Click(object sender, EventArgs e)
        {
            PageBoxFatura.Text = "1";
            FaturasListView.Items.Clear();
            if (faturasOfFaturasListView.Search == false)
            {
                InitFaturasList(1);
            }
            else
            {
                FaturaService fs = (FaturaService)ServiceFactory.GetInstance("FaturaService");
                Pagination<Fatura> pagination = fs.GetFaturaByPage(1, this.cliente.PersonID);
                FaturasListView.Items.Clear();
                FillFaturasPage(pagination);
            }
        }

        private void PreviousFatura_Click(object sender, EventArgs e)
        {

            this.PageBoxFatura.Text = this.faturasOfFaturasListView.CurrentPage-1+"";
            this.FaturasListView.Items.Clear();
            if (this.faturasOfFaturasListView.Search == false)
            {
                InitFaturasList(int.Parse(this.PageBox.Text));
            }
            else
            {
                FaturaService fs = (FaturaService)ServiceFactory.GetInstance("FaturaService");
                Pagination<Fatura> pagination = fs.GetFaturaByPage(int.Parse(this.PageBox.Text), this.cliente.PersonID);
                this.FaturasListView.Items.Clear();
                FillFaturasPage(pagination);
            }
        }

        private void NextFatura_Click(object sender, EventArgs e)
        {
            this.PageBoxFatura.Text = this.faturasOfFaturasListView.CurrentPage + 1 + "";
            this.FaturasListView.Items.Clear();
            if (this.faturasOfFaturasListView.Search == false)
            {
                InitFaturasList(int.Parse(this.PageBox.Text));
            }
            else
            {
                FaturaService fs = (FaturaService)ServiceFactory.GetInstance("FaturaService");
                Pagination<Fatura> pagination = fs.GetFaturaByPage(int.Parse(this.PageBox.Text), this.cliente.PersonID);
                this.FaturasListView.Items.Clear();
                FillFaturasPage(pagination);
            }
        }

        private void LastFatura_Click(object sender, EventArgs e)
        {
            this.PageBoxFatura.Text = this.faturasOfFaturasListView.TotalPage+"";
            this.FaturasListView.Items.Clear();
            if (this.faturasOfFaturasListView.Search == false)
            {
                InitFaturasList(this.faturasOfFaturasListView.TotalPage);
            }
            else
            {
                FaturaService fs = (FaturaService)ServiceFactory.GetInstance("FaturaService");
                Pagination<Fatura> pagination = fs.GetFaturaByPage(this.faturasOfFaturasListView.TotalPage, this.cliente.PersonID);
                this.FaturasListView.Items.Clear();
                FillFaturasPage(pagination);
            }
        }

        private void FirstFatura_MouseEnter(object sender, EventArgs e)
        {
            this.FirstFatura.ForeColor = Color.Blue;
        }

        private void FirstFatura_MouseLeave(object sender, EventArgs e)
        {
            this.FirstFatura.ForeColor = Color.Black;
        }

        private void PreviousFatura_MouseEnter(object sender, EventArgs e)
        {
            this.PreviousFatura.ForeColor = Color.Blue;
        }

        private void PreviousFatura_MouseLeave(object sender, EventArgs e)
        {
            this.PreviousFatura.ForeColor = Color.Black;
        }

        private void NextFatura_MouseEnter(object sender, EventArgs e)
        {
            this.NextFatura.ForeColor = Color.Blue;
        }

        private void NextFatura_MouseLeave(object sender, EventArgs e)
        {
            this.NextFatura.ForeColor = Color.Black;
        }

        private void LastFatura_MouseEnter(object sender, EventArgs e)
        {
            this.LastFatura.ForeColor = Color.Blue;
        }

        private void LastFatura_MouseLeave(object sender, EventArgs e)
        {
            this.LastFatura.ForeColor = Color.Black;
        }

        private void VerBtn_Click(object sender, EventArgs e)
        {
            int encomendaId = int.Parse(this.EncomendasListView.SelectedItems[0].Text);
            ProdutosDaEncomenda form = new ProdutosDaEncomenda(encomendaId);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void verProdutoFaturaBtn_Click(object sender, EventArgs e)
        {
            int encomendaId = int.Parse(FaturasListView.SelectedItems[0].SubItems[1].Text);
            ProdutosDaEncomenda form = new ProdutosDaEncomenda(encomendaId);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void FaturasListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FaturasListView.SelectedItems.Count > 1 || FaturasListView.SelectedItems.Count == 0)
            {
                verProdutoFaturaBtn.Enabled = false;
            }
            else
            {
                verProdutoFaturaBtn.Enabled = true;
            }
        }

        private void PagoBtn_Click(object sender, EventArgs e)
        {
            double custo = double.Parse(EncomendasListView.SelectedItems[0].SubItems[4].Text);
            Pagamento pagamento = new Pagamento(custo);
            pagamento.StartPosition = FormStartPosition.CenterParent;
            pagamento.ShowDialog();
            if (pagamento.DialogResult == DialogResult.OK)
            {
                PagamentoService ps = (PagamentoService)ServiceFactory.GetInstance("PagamentoService");
                int result = ps.Pagar(int.Parse(EncomendasListView.SelectedItems[0].SubItems[0].Text));
                if (result > 0)
                {
                    MessageBox.Show("Encomenda pago");
                    EncomendasListView.Items.Clear();
                    InitEncomendasList(int.Parse(PageBox.Text), cliente.PersonID);
                    PagoBtn.Enabled = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            Login login = new Login();
            login.StartPosition = FormStartPosition.CenterParent;
            login.Owner = this;
            login.ShowDialog();
            Application.ExitThread();
            Dispose();
        }
    }
}
