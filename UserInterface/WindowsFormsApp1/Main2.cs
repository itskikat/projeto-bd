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
using System.Management.Instrumentation;

namespace WindowsFormsApp1
{
    public partial class Main2 : Form
    {
        private Pagination<Funcionario> funcionariosOfFuncionariosListView;
        private Pagination<Condutor> condutoresOfCondutoresListView;
        private Pagination<Fornecedor> fornecedoresOfFornecedoresListView;
        private Pagination<Encomenda> encomendasOfEncomendasListView;
        private Pagination<Transporte> transportesOfTranporteListView;
        private Pagination<Marca> marcasOfMarcasListView;
        private string orderby = null;
        private bool asc = false;
        public Main2()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            Form2_Load();
        }

        private void Form2_Load()
        {
            DispensarBtn.Enabled = false;
            SalaryMaxBox.Value = 1800;
            PageBoxFuncionario.Text = "1";
            PageBoxCondutor.Text = "1";
            PageBoxFornecedor.Text = "1";
            PageBoxEncomenda.Text = "1";
            PageBoxTransporte.Text = "1";
            PageBoxMarca.Text = "1";
            Entregar.Enabled = false;
            InitListViews(1);
            InitFuncionariosList(1);
            InitListViewsWidth(1);
            
        }

        private void InitListViews(int index)
        {
            if (index == 1)
            {
                if (FuncionariosListView.Columns.Count > 1)
                {
                    return;
                }
                FuncionariosListView.View = View.Details;
                FuncionariosListView.FullRowSelect = true;
                FuncionariosListView.Columns.Add("ID");
                FuncionariosListView.Columns.Add("Nome");
                FuncionariosListView.Columns.Add("Telefone");
                FuncionariosListView.Columns.Add("Salario");
                FuncionariosListView.Columns.Add("Genero");
            }
            else if(index==2)
            {
                if (CondutoresListView.Columns.Count > 1)
                {
                    return;
                }
                CondutoresListView.View = View.Details;
                CondutoresListView.FullRowSelect = true;
                CondutoresListView.Columns.Add("ID");
                CondutoresListView.Columns.Add("Nome");
                CondutoresListView.Columns.Add("Carta Conducao");
                CondutoresListView.Columns.Add("Salario");
                CondutoresListView.Columns.Add("Disponibilidade");
                CondutoresListView.Columns.Add("Telefone");
            }
            else if (index == 3)
            {
                if (FornecedoresListView.Columns.Count > 1)
                {
                    return;
                }
                FornecedoresListView.View = View.Details;
                FornecedoresListView.FullRowSelect = true;
                FornecedoresListView.Columns.Add("Nome");
                FornecedoresListView.Columns.Add("Email");
                FornecedoresListView.Columns.Add("Fax");
                FornecedoresListView.Columns.Add("NIF");
                FornecedoresListView.Columns.Add("Tipo");
            }
            else if (index == 4)
            {
                if (this.EncomendasListView.Columns.Count > 1)
                {
                    return;
                }
                this.EncomendasListView.View = View.Details;
                this.EncomendasListView.FullRowSelect = true;
                this.EncomendasListView.Columns.Add("ID");
                this.EncomendasListView.Columns.Add("Cliente");
                this.EncomendasListView.Columns.Add("Funcionario");
                this.EncomendasListView.Columns.Add("Condicao Pagamento");
                this.EncomendasListView.Columns.Add("Destino");
                this.EncomendasListView.Columns.Add("Total");
                this.EncomendasListView.Columns.Add("Estado");
                this.EncomendasListView.Columns.Add("Data");
            }
            else if (index == 5)
            {
                if (this.TransporteListView.Columns.Count > 1)
                {
                    return;
                }
                this.TransporteListView.View = View.Details;
                this.TransporteListView.FullRowSelect = true;
                this.TransporteListView.Columns.Add("ID");
                this.TransporteListView.Columns.Add("Matricula");
                this.TransporteListView.Columns.Add("Marca");
                this.TransporteListView.Columns.Add("Capacidade");
            }
            else if (index == 6)
            {
                if (this.MarcaListView.Columns.Count > 1)
                {
                    return;
                }
                this.MarcaListView.View = View.Details;
                this.MarcaListView.FullRowSelect = true;
                this.MarcaListView.Columns.Add("Nome");
                this.MarcaListView.Columns.Add("Compra");
                this.MarcaListView.Columns.Add("Venda");
                this.MarcaListView.Columns.Add("Ganho");
            }

        }
        private void InitListViewsWidth(int index)
        {
            if (index == 1)
            {
                if (FuncionariosListView.Items.Count == 0)
                {
                    FuncionariosListView.Columns[0].Width = -2;
                    FuncionariosListView.Columns[1].Width = -2;
                    FuncionariosListView.Columns[2].Width = -2;
                    FuncionariosListView.Columns[3].Width = -2;
                    FuncionariosListView.Columns[4].Width = -2;
                }
                else
                {
                    FuncionariosListView.Columns[0].Width = 120;
                    FuncionariosListView.Columns[1].Width = -1;
                    FuncionariosListView.Columns[2].Width = 120;
                    FuncionariosListView.Columns[3].Width = 120;
                    FuncionariosListView.Columns[4].Width = 120;
                }
            }
            else if (index == 2)
            {
                if (CondutoresListView.Items.Count == 0)
                {
                    CondutoresListView.Columns[0].Width = -2;
                    CondutoresListView.Columns[1].Width = -2;
                    CondutoresListView.Columns[2].Width = -2;
                    CondutoresListView.Columns[3].Width = -2;
                    CondutoresListView.Columns[4].Width = -2;
                    CondutoresListView.Columns[5].Width = -2;
                }
                else
                {
                    CondutoresListView.Columns[0].Width = 120;
                    CondutoresListView.Columns[1].Width = -1;
                    CondutoresListView.Columns[2].Width = -2;
                    CondutoresListView.Columns[3].Width = 120;
                    CondutoresListView.Columns[4].Width = -2;
                    CondutoresListView.Columns[5].Width = -1;
                }
            }
            else if (index == 3)
            {
                if (FornecedoresListView.Items.Count == 0)
                {
                    FornecedoresListView.Columns[0].Width = -2;
                    FornecedoresListView.Columns[1].Width = -2;
                    FornecedoresListView.Columns[2].Width = -2;
                    FornecedoresListView.Columns[3].Width = -2;
                    FornecedoresListView.Columns[4].Width = -2;
                }
                else
                {
                    FornecedoresListView.Columns[0].Width = -1;
                    FornecedoresListView.Columns[1].Width = -1;
                    FornecedoresListView.Columns[2].Width = 120;
                    FornecedoresListView.Columns[3].Width = 120;
                    FornecedoresListView.Columns[4].Width = -1;
                }
            }
            else if (index == 4)
            {
                if (EncomendasListView.Items.Count == 0)
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
                    EncomendasListView.Columns[0].Width = 60;
                    EncomendasListView.Columns[1].Width = 80;
                    EncomendasListView.Columns[2].Width = 120;
                    EncomendasListView.Columns[3].Width = 120;
                    EncomendasListView.Columns[4].Width = 120;
                    EncomendasListView.Columns[5].Width = 120;
                    EncomendasListView.Columns[6].Width = 120;
                    EncomendasListView.Columns[7].Width = 120;
                }
            }else if (index == 5)
            {
                if (TransporteListView.Items.Count == 0)
                {
                    TransporteListView.Columns[0].Width = -2;
                    TransporteListView.Columns[1].Width = -2;
                    TransporteListView.Columns[2].Width = -2;
                    TransporteListView.Columns[3].Width = -2;
                }
                else
                {
                    TransporteListView.Columns[0].Width = 150;
                    TransporteListView.Columns[1].Width = 180;
                    TransporteListView.Columns[2].Width = 180;
                    TransporteListView.Columns[3].Width = 180;
                }
            }
            else if (index == 6)
            {
                if (MarcaListView.Items.Count == 0)
                {
                    MarcaListView.Columns[0].Width = -2;
                    MarcaListView.Columns[1].Width = -2;
                    MarcaListView.Columns[2].Width = -2;
                    MarcaListView.Columns[3].Width = -2;
                }
                else
                {
                    MarcaListView.Columns[0].Width = -1;
                    MarcaListView.Columns[1].Width = 180;
                    MarcaListView.Columns[2].Width = 180;
                    MarcaListView.Columns[3].Width = 180;
                }
            }
        }

        private void InitEncomendasList(int page)
        {
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> list = es.GetEncomendaOfClient(page, -1);
            FillEncomendasPage(list);
        }
        private void FillEncomendasPage(Pagination<Encomenda> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Encomenda>();

            encomendasOfEncomendasListView = pagination;

            PreviousEncomenda.Enabled = encomendasOfEncomendasListView.HasPrevious;
            FirstEncomenda.Enabled = encomendasOfEncomendasListView.ShowFirst;
            NextEncomenda.Enabled = encomendasOfEncomendasListView.HasNext;
            LastEncomenda.Enabled = encomendasOfEncomendasListView.ShowLast;
            foreach (Encomenda encomenda in encomendasOfEncomendasListView.Data)
            {
                ListViewItem enc = new ListViewItem(encomenda.EncomendaID.ToString());
                enc.SubItems.Add(encomenda.EncomendaClient.ToString());
                enc.SubItems.Add(encomenda.EncomendaFuncionario.ToString());
                enc.SubItems.Add(encomenda.EncomendaCondicoesPagamento.ToString());
                enc.SubItems.Add(encomenda.EncomendaDestino.ToString());
                enc.SubItems.Add(encomenda.EncomendaCustoTotal.ToString());
                enc.SubItems.Add(encomenda.EncomendaEstado.ToString().Equals("True") ? "Entregue" : "Por Entregar");
                enc.SubItems.Add(encomenda.EncomendaDate.ToShortDateString().ToString());
                this.EncomendasListView.Items.Add(enc);
            }
        }

        private void InitFornecedoresList(int page)
        {
            FornecedorService fs = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
            Pagination<Fornecedor> list = fs.GetFornecedoresList(page);
            FillFornecedoresPage(list);
        }
        private void FillFornecedoresPage(Pagination<Fornecedor> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Fornecedor>();

            fornecedoresOfFornecedoresListView = pagination;

            PreviousFornecedor.Enabled = fornecedoresOfFornecedoresListView.HasPrevious;
            FirstFornecedor.Enabled = fornecedoresOfFornecedoresListView.ShowFirst;
            NextFornecedor.Enabled = fornecedoresOfFornecedoresListView.HasNext;
            LastFornecedor.Enabled = fornecedoresOfFornecedoresListView.ShowLast;

            FornecedoresListView.Items.Clear();
            fornecedoresOfFornecedoresListView = pagination;
            foreach (Fornecedor fornecedor in fornecedoresOfFornecedoresListView.Data)
            {
                ListViewItem forn = new ListViewItem(fornecedor.FornecedorNome.ToString());
                forn.SubItems.Add(fornecedor.FornecedorEmail.ToString());
                forn.SubItems.Add(fornecedor.FornecedorFax.ToString());
                forn.SubItems.Add(fornecedor.FornecedorNIF.ToString());
                forn.SubItems.Add(fornecedor.FornecedorTipo.ToString());
                FornecedoresListView.Items.Add(forn);
            }
        }

        private void InitCondutoresList(int page)
        {
            CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
            Pagination<Condutor> list = cs.GetCondutoresList(page);
            FillCondutoresPage(list);
        }
        private void FillCondutoresPage(Pagination<Condutor> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Condutor>();

            condutoresOfCondutoresListView = pagination;

            PreviousCondutor.Enabled = condutoresOfCondutoresListView.HasPrevious;
            FirstCondutor.Enabled = condutoresOfCondutoresListView.ShowFirst;
            NextCondutor.Enabled = condutoresOfCondutoresListView.HasNext;
            LastCondutor.Enabled = condutoresOfCondutoresListView.ShowLast;

            CondutoresListView.Items.Clear();
            foreach (Condutor condutor in condutoresOfCondutoresListView.Data)
            {
                ListViewItem cond = new ListViewItem(condutor.CondutorID.ToString());
                cond.SubItems.Add(condutor.PersonNome.ToString());
                cond.SubItems.Add(condutor.CondutorCarta.ToString());
                cond.SubItems.Add(condutor.PersonSalario.ToString());
                cond.SubItems.Add(condutor.CondutorDisponivel.ToString().Equals("True") ? "Disponivel" : "Nao Disponivel");
                cond.SubItems.Add(condutor.PersonTelefone.ToString());
                CondutoresListView.Items.Add(cond);
            }
        }

        private void InitFuncionariosList(int page)
        {
            FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
            Pagination<Funcionario> list = fs.GetFuncionariosList(page);
            FillFuncionariosPage(list);
        }

        private void FillFuncionariosPage(Pagination<Funcionario> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Funcionario>();

            this.funcionariosOfFuncionariosListView = pagination;

            this.PreviousFuncionario.Enabled = funcionariosOfFuncionariosListView.HasPrevious;
            this.FirstFuncionario.Enabled = funcionariosOfFuncionariosListView.ShowFirst;
            this.NextFuncionario.Enabled = funcionariosOfFuncionariosListView.HasNext;
            this.LastFuncionario.Enabled = funcionariosOfFuncionariosListView.ShowLast;

            this.FuncionariosListView.Items.Clear();

            foreach (Pessoa funcionario in funcionariosOfFuncionariosListView.Data)
            {
                ListViewItem func = new ListViewItem(funcionario.PersonID.ToString());
                func.SubItems.Add(funcionario.PersonNome.ToString());
                func.SubItems.Add(funcionario.PersonTelefone.ToString());
                if (funcionario.PersonSalario != -1)
                    func.SubItems.Add(funcionario.PersonSalario.ToString());
                else
                    func.Remove();
                func.SubItems.Add(funcionario.PersonGenero.ToString());

                this.FuncionariosListView.Items.Add(func);
            }
        }
        private void InitTransportesList()
        {
            TransporteService ts = (TransporteService)ServiceFactory.GetInstance("TransporteService");
            Pagination<Transporte> list = ts.GetTransportesList(1);
            FillTransportesPage(list);
        }
        private void FillTransportesPage(Pagination<Transporte> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Transporte>();

            transportesOfTranporteListView = pagination;

            PreviousTransporte.Enabled = transportesOfTranporteListView.HasPrevious;
            FirstTransporte.Enabled = transportesOfTranporteListView.ShowFirst;
            NextTransporte.Enabled = transportesOfTranporteListView.HasNext;
            LastTransporte.Enabled = transportesOfTranporteListView.ShowLast;
            foreach (Transporte transporte in transportesOfTranporteListView.Data)
            {
                ListViewItem enc = new ListViewItem(transporte.ID.ToString());
                enc.SubItems.Add(transporte.Matricula);
                enc.SubItems.Add(transporte.Marca);
                enc.SubItems.Add(transporte.Capaciadade.ToString());
                TransporteListView.Items.Add(enc);
            }
        }

        // AQUI
        private void InitMarcaList()
        {
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> list = ms.GetMarcaList(1);
            FillMarcaPage(list);
        }
        private void FillMarcaPage(Pagination<Marca> pagination)
        {
            if (pagination == null)
                pagination = new Pagination<Marca>();

            marcasOfMarcasListView = pagination;

            PreviousMarca.Enabled = marcasOfMarcasListView.HasPrevious;
            FirstMarca.Enabled = marcasOfMarcasListView.ShowFirst;
            NextMarca.Enabled = marcasOfMarcasListView.HasNext;
            LastMarca.Enabled = marcasOfMarcasListView.ShowLast;
            foreach (Marca marca in marcasOfMarcasListView.Data)
            {
                ListViewItem m = new ListViewItem(marca.Nome.ToString());
                m.SubItems.Add(marca.Compra.ToString());
                m.SubItems.Add(marca.Venda.ToString());
                m.SubItems.Add(marca.Ganho.ToString());
               MarcaListView.Items.Add(m);
            }
        }


        private void DispensarFuncionario(object sender, EventArgs e)
        {
            foreach (ListViewItem funcionario in this.FuncionariosListView.SelectedItems)
            {
                FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
                int funcionarioID = int.Parse(funcionario.SubItems[0].Text);
                Funcionario fc = fs.GetFuncionarioByID(funcionarioID);
                fc.PersonSalario = -1;
                fs.UpdateFuncionario(fc);
                funcionario.Remove();
                MessageBox.Show("Funcionario dispensado com sucesso!");
            }
            
            this.FuncionariosListView.Refresh();
        }

        private void TabChange(object sender, EventArgs e)
        {
            
            int index = this.Tab.SelectedIndex;
            switch (index)
            {
                case 0:
                   /* this.FuncionariosListView.Items.Clear();
                    InitListViews(1);
                    InitFuncionariosList(1);
                    InitListViewsWidth(1);*/
                    break;
                case 1:
                    CondutoresListView.Items.Clear();
                    InitListViews(2);
                    if (condutoresOfCondutoresListView == null || !condutoresOfCondutoresListView.Search)
                    {
                        CondutoresListView.Items.Clear();
                        InitCondutoresList(int.Parse(PageBoxCondutor.Text));
                    }
                    InitListViewsWidth(2);
                    break;
                case 2:
                    FornecedoresListView.Items.Clear();
                    InitListViews(3);
                    if (fornecedoresOfFornecedoresListView == null || !fornecedoresOfFornecedoresListView.Search)
                    {
                        FornecedoresListView.Items.Clear();
                        InitFornecedoresList(int.Parse(PageBoxFornecedor.Text));
                    }
                    InitListViewsWidth(3);
                    break;
                case 3:
                    EncomendasListView.Items.Clear();
                    InitListViews(4);
                    InitEncomendasList(1);
                    InitListViewsWidth(4);
                    break;
                case 4:
                    TransporteListView.Items.Clear();
                    InitListViews(5);
                    InitTransportesList();
                    InitListViewsWidth(5);
                    break;
                case 5:
                    orderby = null;
                    MarcaListView.Items.Clear();
                    InitListViews(6);
                    InitMarcaList();
                    InitListViewsWidth(6);
                    break;

            };
        }

        private void ContratarFuncionario(object sender, EventArgs e)
        {
            this.Hide();
            AddWorker aw = new AddWorker();
            aw.StartPosition = FormStartPosition.CenterScreen;
            aw.ShowDialog();
        }

        private void ContratarCondutor(object sender, EventArgs e)
        {
            this.Hide();
            AddWorker aw = new AddWorker();
            aw.StartPosition = FormStartPosition.CenterScreen;
            aw.ShowDialog();
        }

        private void AdicionarFornecedor(object sender, EventArgs e)
        {
            this.Hide();
            AddSupplier asup = new AddSupplier();
            asup.StartPosition = FormStartPosition.CenterScreen;
            asup.ShowDialog();
        }

        private void FirstFuncionario_MouseEnter(object sender, EventArgs e)
        {
            this.FirstFuncionario.ForeColor = Color.Blue;
        }

        private void FirstFuncionario_MouseLeave(object sender, EventArgs e)
        {
            this.FirstFuncionario.ForeColor = Color.Black;
        }

        private void NextFuncionario_MouseEnter(object sender, EventArgs e)
        {
            this.NextFuncionario.ForeColor = Color.Blue;
        }

        private void NextFuncionario_MouseLeave(object sender, EventArgs e)
        {
            this.NextFuncionario.ForeColor = Color.Black;
        }

        private void LastFuncionario_MouseEnter(object sender, EventArgs e)
        {
            this.LastFuncionario.ForeColor = Color.Blue;
        }

        private void LastFuncionario_MouseLeave(object sender, EventArgs e)
        {
            this.LastFuncionario.ForeColor = Color.Black;
        }

        private void FirstFuncionario_Click(object sender, EventArgs e)
        {
            this.PageBoxFuncionario.Text = "1";
            this.FuncionariosListView.Items.Clear();
            if (this.funcionariosOfFuncionariosListView.Search == false)
            {
                InitFuncionariosList(1);
            }
            else
            {
                FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
                Pagination<Funcionario> pagination = fs.Search(1, this.SearchByNameBoxFuncionario.Text,this.SearchByEmailBoxFuncionario.Text,
                    (int)this.SalaryMinBox.Value, (int)this.SalaryMaxBox.Value);
                this.FuncionariosListView.Items.Clear();
                FillFuncionariosPage(pagination);
            }

        }

        private void PreviousFuncionario_Click(object sender, EventArgs e)
        {
            PageBoxFuncionario.Text = funcionariosOfFuncionariosListView.CurrentPage - 1 + "";
            FuncionariosListView.Items.Clear();
            if (funcionariosOfFuncionariosListView.Search == false)
            {
                InitFuncionariosList(int.Parse(PageBoxFuncionario.Text));
            }
            else
            {
                FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
                Pagination<Funcionario> pagination = fs.Search(int.Parse(PageBoxFuncionario.Text), SearchByNameBoxFuncionario.Text, SearchByEmailBoxFuncionario.Text,
                    (int)SalaryMinBox.Value, (int)SalaryMaxBox.Value);
                FuncionariosListView.Items.Clear();
                FillFuncionariosPage(pagination);
            }
        }

        private void NextFuncionario_Click(object sender, EventArgs e)
        {
            PageBoxFuncionario.Text = funcionariosOfFuncionariosListView.CurrentPage + 1 + "";
            FuncionariosListView.Items.Clear();
            if (funcionariosOfFuncionariosListView.Search == false)
            {
                InitFuncionariosList(int.Parse(PageBoxFuncionario.Text));
            }
            else
            {
                FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
                Pagination<Funcionario> pagination = fs.Search(int.Parse(PageBoxFuncionario.Text), SearchByNameBoxFuncionario.Text, SearchByEmailBoxFuncionario.Text,
                    (int)SalaryMinBox.Value, (int)SalaryMaxBox.Value);
                FuncionariosListView.Items.Clear();
                FillFuncionariosPage(pagination);
            }
        }

        private void LastFuncionario_Click(object sender, EventArgs e)
        {
            PageBoxFuncionario.Text = funcionariosOfFuncionariosListView.TotalPage + "";
            FuncionariosListView.Items.Clear();
            if (funcionariosOfFuncionariosListView.Search == false)
            {
                InitFuncionariosList(int.Parse(PageBoxFuncionario.Text));
            }
            else
            {
                FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
                Pagination<Funcionario> pagination = fs.Search(funcionariosOfFuncionariosListView.TotalPage, SearchByNameBoxFuncionario.Text, SearchByEmailBoxFuncionario.Text,
                    (int)SalaryMinBox.Value, (int)SalaryMaxBox.Value);
                FuncionariosListView.Items.Clear();
                FillFuncionariosPage(pagination);
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            this.PageBoxFuncionario.Text = "1";
            FuncionarioService fs = (FuncionarioService)ServiceFactory.GetInstance("FuncionarioService");
            Pagination<Funcionario> pagination = fs.Search(-1, this.SearchByNameBoxFuncionario.Text, this.SearchByEmailBoxFuncionario.Text,
                (int)this.SalaryMinBox.Value, (int)this.SalaryMaxBox.Value);
            this.FuncionariosListView.Items.Clear();
            FillFuncionariosPage(pagination);
        }

        private void FuncionariosListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FuncionariosListView.SelectedItems.Count > 0)
            {
                this.DispensarBtn.Enabled = true;
            }
            else
            {
                this.DispensarBtn.Enabled = false;
            }
        }

        private void TodosFuncionarios_Click(object sender, EventArgs e)
        {
            this.PageBoxFuncionario.Text = "1";
            InitFuncionariosList(1);
        }

        private void FirstCondutor_Click(object sender, EventArgs e)
        {
            this.PageBoxCondutor.Text = "1";
            this.CondutoresListView.Items.Clear();
            if (this.condutoresOfCondutoresListView.Search == false)
            {
                InitCondutoresList(1);
            }
            else
            {
                CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
                Pagination<Condutor> pagination = cs.Search(1, this.SearchByNameBoxCondutor.Text, this.SearchByEmailBoxCondutor.Text,
                    (int)this.SalaryMinCondutor.Value, (int)this.SalaryMaxCondutor.Value);
                this.CondutoresListView.Items.Clear();
                FillCondutoresPage(pagination);
            }

        }

        private void PreviousCondutor_Click(object sender, EventArgs e)
        {
            PageBoxCondutor.Text = condutoresOfCondutoresListView.CurrentPage-1+"";
            CondutoresListView.Items.Clear();
            if (condutoresOfCondutoresListView.Search == false)
            {
                InitCondutoresList(int.Parse(PageBoxCondutor.Text));
            }
            else
            {
                CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
                Pagination<Condutor> pagination = cs.Search(int.Parse(PageBoxCondutor.Text),SearchByNameBoxCondutor.Text, SearchByEmailBoxCondutor.Text,
                    (int)SalaryMinCondutor.Value, (int)SalaryMaxCondutor.Value);
                CondutoresListView.Items.Clear();
                FillCondutoresPage(pagination);
            }

        }

        private void NextCondutor_Click(object sender, EventArgs e)
        {
            this.PageBoxCondutor.Text = this.condutoresOfCondutoresListView.CurrentPage + 1 + "";
            this.CondutoresListView.Items.Clear();
            if (this.condutoresOfCondutoresListView.Search == false)
            {
                InitCondutoresList(int.Parse(PageBoxCondutor.Text));
            }
            else
            {
                CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
                Pagination<Condutor> pagination = cs.Search(int.Parse(PageBoxCondutor.Text), SearchByNameBoxCondutor.Text, this.SearchByEmailBoxCondutor.Text,
                    (int)SalaryMinCondutor.Value, (int)SalaryMaxCondutor.Value);
                CondutoresListView.Items.Clear();
                FillCondutoresPage(pagination);
            }
        }

        private void LastCondutor_Click(object sender, EventArgs e)
        {
            PageBoxCondutor.Text = condutoresOfCondutoresListView.TotalPage+ "";
            CondutoresListView.Items.Clear();
            if (condutoresOfCondutoresListView.Search == false)
            {
                InitCondutoresList(condutoresOfCondutoresListView.TotalPage);
            }
            else
            {
                CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
                Pagination<Condutor> pagination = cs.Search(condutoresOfCondutoresListView.TotalPage, SearchByNameBoxCondutor.Text, SearchByEmailBoxCondutor.Text,
                    (int)SalaryMinCondutor.Value, (int)SalaryMaxCondutor.Value);
                CondutoresListView.Items.Clear();
                FillCondutoresPage(pagination);
            }
        }

        private void FirstCondutor_MouseEnter(object sender, EventArgs e)
        {
            this.FirstCondutor.ForeColor = Color.Blue;
        }

        private void FirstCondutor_MouseLeave(object sender, EventArgs e)
        {
            this.FirstCondutor.ForeColor = Color.Black;
        }

        private void PreviousCondutor_MouseEnter(object sender, EventArgs e)
        {
            this.PreviousCondutor.ForeColor = Color.Blue;
        }

        private void PreviousCondutor_MouseLeave(object sender, EventArgs e)
        {
            this.PreviousCondutor.ForeColor = Color.Black;
        }

        private void NextCondutor_MouseEnter(object sender, EventArgs e)
        {
            this.NextCondutor.ForeColor = Color.Blue;
        }

        private void NextCondutor_MouseLeave(object sender, EventArgs e)
        {
            this.NextCondutor.ForeColor = Color.Black;
        }

        private void LastCondutor_MouseEnter(object sender, EventArgs e)
        {
            this.LastCondutor.ForeColor = Color.Blue;
        }

        private void LastCondutor_MouseLeave(object sender, EventArgs e)
        {
            this.LastCondutor.ForeColor = Color.Black;
        }

        private void PageBoxFuncionario_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = funcionariosOfFuncionariosListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = this.PageBoxFuncionario.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (this.PageBoxFuncionario.Text.Trim().Length != 0)
            {
                if (int.Parse(this.PageBoxFuncionario.Text.Trim()) > totalpage)
                {
                    this.PageBoxFuncionario.Text = "";
                    this.PageBoxFuncionario.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    this.FuncionariosListView.Items.Clear();
                    InitFuncionariosList(int.Parse(this.PageBoxFuncionario.Text.Trim()));
                }
            }
        }

        private void PageBoxCondutor_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = condutoresOfCondutoresListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = this.PageBoxCondutor.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (this.PageBoxFuncionario.Text.Trim().Length != 0)
            {
                if (int.Parse(this.PageBoxCondutor.Text.Trim()) > totalpage)
                {
                    this.PageBoxCondutor.Text = "";
                    this.PageBoxCondutor.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    this.CondutoresListView.Items.Clear();
                    InitCondutoresList(int.Parse(this.PageBoxCondutor.Text.Trim()));
                }
            }
        }

        private void SearchCondutor_Click(object sender, EventArgs e)
        {
            this.PageBoxCondutor.Text = "1";
            CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
            Pagination<Condutor> pagination = cs.Search(-1, this.SearchByNameBoxCondutor.Text, this.SearchByEmailBoxCondutor.Text,
                (int)this.SalaryMinCondutor.Value, (int)this.SalaryMaxCondutor.Value);
            this.CondutoresListView.Items.Clear();
            FillCondutoresPage(pagination);
        }

        private void TodosCondutores_Click(object sender, EventArgs e)
        {
            this.PageBoxCondutor.Text = "1";
            InitCondutoresList(1);
        }

        private void TodosFornecedores_Click(object sender, EventArgs e)
        {
            this.PageBoxFuncionario.Text = "1";
            InitFornecedoresList(1);
        }

        private void FirstFornecedor_Click(object sender, EventArgs e)
        {
            PageBoxFornecedor.Text = "1";
            FornecedoresListView.Items.Clear();
            if (fornecedoresOfFornecedoresListView.Search == false)
            {
                InitFornecedoresList(1);
            }
            else
            {
                FornecedorService fs = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
                Pagination<Fornecedor> pagination = fs.Search(fornecedoresOfFornecedoresListView.TotalPage, SearchByNomeFornecedor.Text, SearchByEmailFornecedor.Text
                    , SearchByFax.Text, SearchByNIF.Text);
                FornecedoresListView.Items.Clear();
                FillFornecedoresPage(pagination);
            }

        }

        private void PreviousFornecedor_Click(object sender, EventArgs e)
        {
            PageBoxFornecedor.Text = fornecedoresOfFornecedoresListView.CurrentPage - 1 + "";
            FornecedoresListView.Items.Clear();
            if (fornecedoresOfFornecedoresListView.Search == false)
            {
                InitFornecedoresList(int.Parse(PageBoxCondutor.Text));
            }
            else
            {
                FornecedorService fs = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
                Pagination<Fornecedor> pagination = fs.Search(fornecedoresOfFornecedoresListView.TotalPage, SearchByNomeFornecedor.Text, SearchByEmailFornecedor.Text
                    , SearchByFax.Text, SearchByNIF.Text);
                FornecedoresListView.Items.Clear();
                FillFornecedoresPage(pagination);
            }

        }

        private void NextFornecedor_Click(object sender, EventArgs e)
        {
            PageBoxFornecedor.Text = fornecedoresOfFornecedoresListView.CurrentPage + 1 + "";
            FornecedoresListView.Items.Clear();
            if (fornecedoresOfFornecedoresListView.Search == false)
            {
                InitFornecedoresList(int.Parse(PageBoxFornecedor.Text));
            }
            else
            {
                FornecedorService fs = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
                Pagination<Fornecedor> pagination = fs.Search(int.Parse(PageBoxFornecedor.Text), SearchByNomeFornecedor.Text, SearchByEmailFornecedor.Text
                    , SearchByFax.Text, SearchByNIF.Text);
                FornecedoresListView.Items.Clear();
                FillFornecedoresPage(pagination);
            }
        }

        private void LastFornecedor_Click(object sender, EventArgs e)
        {
            PageBoxFornecedor.Text = fornecedoresOfFornecedoresListView.TotalPage + "";
            FornecedoresListView.Items.Clear();
            if (fornecedoresOfFornecedoresListView.Search == false)
            {
                InitFornecedoresList(fornecedoresOfFornecedoresListView.TotalPage);
            }
            else
            {
                FornecedorService fs = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
                Pagination<Fornecedor> pagination = fs.Search(fornecedoresOfFornecedoresListView.TotalPage, SearchByNomeFornecedor.Text, SearchByEmailFornecedor.Text
                    , SearchByFax.Text, SearchByNIF.Text);
                FornecedoresListView.Items.Clear();
                FillFornecedoresPage(pagination);
            }
        }

        private void FirstFornecedor_MouseEnter(object sender, EventArgs e)
        {
            this.FirstFornecedor.ForeColor = Color.Blue;
        }

        private void FirstFornecedor_MouseLeave(object sender, EventArgs e)
        {
            this.FirstFornecedor.ForeColor = Color.Black;
        }

        private void PreviousFornecedor_MouseEnter(object sender, EventArgs e)
        {
            this.PreviousFornecedor.ForeColor = Color.Blue;
        }

        private void PreviousFornecedor_MouseLeave(object sender, EventArgs e)
        {
            this.PreviousFornecedor.ForeColor = Color.Black;
        }

        private void NextFornecedor_MouseEnter(object sender, EventArgs e)
        {
            this.NextFornecedor.ForeColor = Color.Blue;
        }

        private void NextFornecedor_MouseLeave(object sender, EventArgs e)
        {
            this.NextFornecedor.ForeColor = Color.Black;
        }

        private void LastFornecedor_MouseEnter(object sender, EventArgs e)
        {
            this.LastFornecedor.ForeColor = Color.Blue;
        }

        private void LastFornecedor_MouseLeave(object sender, EventArgs e)
        {
            this.LastFornecedor.ForeColor = Color.Black;
        }

        private void SearchFornecedor_Click(object sender, EventArgs e)
        {
            PageBoxFornecedor.Text = "1";
            FornecedorService fs = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
            Pagination<Fornecedor> pagination = fs.Search(-1, SearchByNomeFornecedor.Text, SearchByEmailFornecedor.Text
                , SearchByFax.Text, SearchByNIF.Text);
            FornecedoresListView.Items.Clear();
            FillFornecedoresPage(pagination);
        }

        private void TodosFornecedores_Click_1(object sender, EventArgs e)
        {
            this.PageBoxFornecedor.Text = "1";
            InitFornecedoresList(1);
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
            PreviousEncomenda.ForeColor = Color.Blue;
        }

        private void PreviousEncomenda_MouseLeave(object sender, EventArgs e)
        {
            PreviousEncomenda.ForeColor = Color.Black;
        }

        private void NextEncomenda_MouseEnter(object sender, EventArgs e)
        {
            NextEncomenda.ForeColor = Color.Blue;
        }

        private void NextEncomenda_MouseLeave(object sender, EventArgs e)
        {
            NextEncomenda.ForeColor = Color.Black;
        }

        private void LastEncomenda_MouseEnter(object sender, EventArgs e)
        {
            LastEncomenda.ForeColor = Color.Blue;
        }

        private void LastEncomenda_MouseLeave(object sender, EventArgs e)
        {
             LastEncomenda.ForeColor = Color.Black;
        }

        private void NaoEntregados_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = "1";
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomandasByEstado(-1, false, -1);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void Entregados_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = "1";
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomandasByEstado(-1, true, -1);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void TodosEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = "1";
            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomendaOfClient(1, -1);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void Entregar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem encomenda in this.EncomendasListView.Items)
            {
                if (encomenda.Selected)
                {
                    int encomendaID = int.Parse(encomenda.SubItems[0].Text);
                    Encomenda encomenda1 = new Encomenda();
                    encomenda1.EncomendaID = encomendaID;
                    encomenda1.EncomendaEstado = true;
                    if (encomenda.SubItems[6].Text.Equals("Por Entregar"))
                    {
                        EncomendaService encomendaService = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                        int result = encomendaService.EntregarEncomenda(encomenda1);
                        if (result < 1)
                        {
                            MessageBox.Show("Nao e possivel entregar a encomenda!");
                            return;
                        }         
                        MessageBox.Show("Encomenda entregue com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Encomenda ja entregue!");
                        return;
                    }


                }
            }

            EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
            Pagination<Encomenda> pagination = es.GetEncomendaOfClient(int.Parse(PageBoxEncomenda.Text), -1);
            this.EncomendasListView.Items.Clear();
            FillEncomendasPage(pagination);
        }

        private void EncomendasListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.EncomendasListView.SelectedItems)
            {
                if (item.SubItems[6].Text.Equals("Entregue"))
                    Entregar.Enabled = false;
                else
                    Entregar.Enabled = true;
            }
        }

        private void FirstTransporte_MouseEnter(object sender, EventArgs e)
        {
            FirstTransporte.ForeColor = Color.Blue;
        }

        private void FirstTransporte_MouseLeave(object sender, EventArgs e)
        {
            FirstTransporte.ForeColor = Color.Black;
        }

        private void PreviousTransporte_MouseEnter(object sender, EventArgs e)
        {
            PreviousTransporte.ForeColor = Color.Blue;
        }

        private void PreviousTransporte_MouseLeave(object sender, EventArgs e)
        {
            PreviousTransporte.ForeColor = Color.Black;
        }

        private void NextTransporte_MouseEnter(object sender, EventArgs e)
        {
            NextTransporte.ForeColor = Color.Blue;
        }

        private void NextTransporte_MouseLeave(object sender, EventArgs e)
        {
            NextTransporte.ForeColor = Color.Black;
        }

        private void LastTransporte_MouseEnter(object sender, EventArgs e)
        {
            LastTransporte.ForeColor = Color.Blue;
        }

        private void LastTransporte_MouseLeave(object sender, EventArgs e)
        {
            LastTransporte.ForeColor = Color.Black;
        }

        private void FirstMarca_MouseEnter(object sender, EventArgs e)
        {
            FirstMarca.ForeColor = Color.Blue;
        }

        private void FirstMarca_MouseLeave(object sender, EventArgs e)
        {
            FirstMarca.ForeColor = Color.Black;
        }

        private void PreviousMarca_MouseEnter(object sender, EventArgs e)
        {
            PreviousMarca.ForeColor = Color.Blue;
        }

        private void PreviousMarca_MouseLeave(object sender, EventArgs e)
        {
            PreviousMarca.ForeColor = Color.Black;
        }

        private void NextMarca_MouseEnter(object sender, EventArgs e)
        {
            NextMarca.ForeColor = Color.Blue;
        }

        private void NextMarca_MouseLeave(object sender, EventArgs e)
        {
            NextMarca.ForeColor = Color.Black;
        }

        private void LastMarca_MouseEnter(object sender, EventArgs e)
        {
            LastMarca.ForeColor = Color.Blue;
        }

        private void LastMarca_MouseLeave(object sender, EventArgs e)
        {
            LastMarca.ForeColor = Color.Black;
        }

        private void VerProdutos_Click(object sender, EventArgs e)
        {
            string fornecedor = FornecedoresListView.SelectedItems[0].Text;
            ProdutosDoFornecedor form = new ProdutosDoFornecedor(fornecedor);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
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


        private void FirstTransporte_Click(object sender, EventArgs e)
        {
            PageBoxTransporte.Text = "1";
            TransporteListView.Items.Clear();
            TransporteService ts = (TransporteService)ServiceFactory.GetInstance("TransporteService");
            Pagination<Transporte> pagination = ts.GetTransportesList(1);
            FillTransportesPage(pagination);
            
        }

        private void PreviousTransporte_Click(object sender, EventArgs e)
        {
            PageBoxTransporte.Text = transportesOfTranporteListView.CurrentPage-1+"";
            TransporteListView.Items.Clear();
            TransporteService ts = (TransporteService)ServiceFactory.GetInstance("TransporteService");
            Pagination<Transporte> pagination = ts.GetTransportesList(int.Parse(PageBoxTransporte.Text));
            FillTransportesPage(pagination);
        }

        private void NextTransporte_Click(object sender, EventArgs e)
        {
            PageBoxTransporte.Text = transportesOfTranporteListView.CurrentPage + 1 + "";
            TransporteListView.Items.Clear();
            TransporteService ts = (TransporteService)ServiceFactory.GetInstance("TransporteService");
            Pagination<Transporte> pagination = ts.GetTransportesList(int.Parse(PageBoxTransporte.Text));
            FillTransportesPage(pagination);
        }

        private void LastTransporte_Click(object sender, EventArgs e)
        {
            PageBoxTransporte.Text = transportesOfTranporteListView.TotalPage + "";
            TransporteListView.Items.Clear();
            TransporteService ts = (TransporteService)ServiceFactory.GetInstance("TransporteService");
            Pagination<Transporte> pagination = ts.GetTransportesList(transportesOfTranporteListView.TotalPage);
            FillTransportesPage(pagination);
        }

        private void PageBoxFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = fornecedoresOfFornecedoresListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = PageBoxFornecedor.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (PageBoxFornecedor.Text.Trim().Length != 0)
            {
                if (int.Parse(PageBoxFornecedor.Text.Trim()) > totalpage)
                {
                    PageBoxFornecedor.Text = "";
                    PageBoxFornecedor.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    TransporteListView.Items.Clear();
                    FornecedorService ts = (FornecedorService)ServiceFactory.GetInstance("FornecedorService");
                    Pagination<Fornecedor> pagination = ts.GetFornecedoresList(int.Parse(PageBoxFornecedor.Text));
                    FillFornecedoresPage(pagination);
                }
            }
        }

        private void FirstEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = "1";
            EncomendasListView.Items.Clear();
            if (encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(1);
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(1,
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, -1);
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
                InitEncomendasList(int.Parse(PageBoxEncomenda.Text));
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(int.Parse(PageBoxEncomenda.Text),
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, -1);
                this.EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }


        private void NextEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = encomendasOfEncomendasListView.CurrentPage + 1 + "";
            EncomendasListView.Items.Clear();
            if (encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(int.Parse(PageBoxEncomenda.Text));
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(int.Parse(PageBoxEncomenda.Text),
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado, -1);
                this.EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }

        private void LastEncomenda_Click(object sender, EventArgs e)
        {
            PageBoxEncomenda.Text = encomendasOfEncomendasListView.TotalPage + "";
            EncomendasListView.Items.Clear();
            if (encomendasOfEncomendasListView.Search == false)
            {
                InitEncomendasList(encomendasOfEncomendasListView.TotalPage);
            }
            else
            {
                EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                Pagination<Encomenda> pagination = es.GetEncomandasByEstado(encomendasOfEncomendasListView.TotalPage,
                    encomendasOfEncomendasListView.Data[0].EncomendaEstado,-1);
                EncomendasListView.Items.Clear();
                FillEncomendasPage(pagination);
            }
        }

        private void PageBoxEncomenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = encomendasOfEncomendasListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = PageBoxEncomenda.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (PageBoxEncomenda.Text.Trim().Length != 0)
            {
                if (int.Parse(PageBoxEncomenda.Text.Trim()) > totalpage)
                {
                    PageBoxEncomenda.Text = "";
                    PageBoxEncomenda.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    EncomendasListView.Items.Clear();
                    EncomendaService es = (EncomendaService)ServiceFactory.GetInstance("EncomendaService");
                    Pagination<Encomenda> pagination = es.GetEncomendaOfClient(int.Parse(PageBoxEncomenda.Text), -1);
                    FillEncomendasPage(pagination);
                }
            }
        }

        private void PageBoxTransporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = transportesOfTranporteListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = PageBoxTransporte.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (PageBoxTransporte.Text.Trim().Length != 0)
            {
                if (int.Parse(PageBoxTransporte.Text.Trim()) > totalpage)
                {
                    PageBoxTransporte.Text = "";
                    PageBoxTransporte.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    TransporteListView.Items.Clear();
                    TransporteService ts = (TransporteService)ServiceFactory.GetInstance("TransporteService");
                    Pagination<Transporte> pagination = ts.GetTransportesList(int.Parse(PageBoxTransporte.Text));
                    FillTransportesPage(pagination);
                }
            }
        }

        private void FirstMarca_Click(object sender, EventArgs e)
        {
            PageBoxMarca.Text =  "1";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination;
            if (orderby == null)
                pagination = ms.GetMarcaList(1);
            else if("Ganho".Equals(orderby))
                pagination = ms.GetMarcaOrderByGanho(1,asc==true?1:-1);
            else if("Venda".Equals(orderby))
                pagination = ms.GetMarcaOrderByVenda(1, asc == true ? 1 : -1);
            else
                pagination = ms.GetMarcaOrderByCompra(1, asc == true ? 1 : -1);

            FillMarcaPage(pagination);
        }

        private void PreviousMarca_Click(object sender, EventArgs e)
        {
            PageBoxMarca.Text = marcasOfMarcasListView.CurrentPage-1 + "";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination;
            if (orderby == null)
                pagination = ms.GetMarcaList(int.Parse(PageBoxMarca.Text));
            else if ("Ganho".Equals(orderby))
                pagination = ms.GetMarcaOrderByGanho(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);
            else if ("Venda".Equals(orderby))
                pagination = ms.GetMarcaOrderByVenda(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);
            else
                pagination = ms.GetMarcaOrderByCompra(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);

            FillMarcaPage(pagination);
        }

        private void NextMarca_Click(object sender, EventArgs e)
        {
            PageBoxMarca.Text = marcasOfMarcasListView.CurrentPage + 1 + "";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination;
            if (orderby == null)
                pagination = ms.GetMarcaList(int.Parse(PageBoxMarca.Text));
            else if ("Ganho".Equals(orderby))
                pagination = ms.GetMarcaOrderByGanho(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);
            else if ("Venda".Equals(orderby))
                pagination = ms.GetMarcaOrderByVenda(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);
            else
                pagination = ms.GetMarcaOrderByCompra(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);

            FillMarcaPage(pagination);
        }

        private void LastMarca_Click(object sender, EventArgs e)
        {
            PageBoxMarca.Text = marcasOfMarcasListView.TotalPage + "";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination;
            if (orderby == null)
                pagination = ms.GetMarcaList(marcasOfMarcasListView.TotalPage);
            else if ("Ganho".Equals(orderby))
                pagination = ms.GetMarcaOrderByGanho(marcasOfMarcasListView.TotalPage, asc == true ? 1 : -1);
            else if ("Venda".Equals(orderby))
                pagination = ms.GetMarcaOrderByVenda(marcasOfMarcasListView.TotalPage, asc == true ? 1 : -1);
            else
                pagination = ms.GetMarcaOrderByCompra(marcasOfMarcasListView.TotalPage, asc == true ? 1 : -1);

            FillMarcaPage(pagination);
        }

        private void PageBoxMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            int totalpage = marcasOfMarcasListView.TotalPage;

            if (e.KeyChar != '\b')
            {
                int len = PageBoxMarca.Text.Length;
                if (len < 1 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
            if (PageBoxMarca.Text.Trim().Length != 0)
            {
                if (int.Parse(PageBoxMarca.Text.Trim()) > totalpage)
                {
                    PageBoxMarca.Text = "";
                    PageBoxMarca.Text = totalpage.ToString();
                }

                if (e.KeyChar == '\r')
                {
                    MarcaListView.Items.Clear();
                    MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
                    Pagination<Marca> pagination;
                    if (orderby == null)
                        pagination = ms.GetMarcaList(1);
                    else if ("Ganho".Equals(orderby))
                        pagination = ms.GetMarcaOrderByGanho(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);
                    else if ("Venda".Equals(orderby))
                        pagination = ms.GetMarcaOrderByVenda(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);
                    else
                        pagination = ms.GetMarcaOrderByCompra(int.Parse(PageBoxMarca.Text), asc == true ? 1 : -1);

                    FillMarcaPage(pagination);
                }
            }
        }

        private void Compra_Click(object sender, EventArgs e)
        {
            orderby = "Compra";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination = ms.GetMarcaOrderByCompra(1,-1);
            FillMarcaPage(pagination);
        }

        private void Venda_Click(object sender, EventArgs e)
        {
            orderby = "Venda";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination = ms.GetMarcaOrderByVenda(1, -1);
            FillMarcaPage(pagination);
        }

        private void Ganho_Click(object sender, EventArgs e)
        {
            orderby = "Ganho";
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");
            Pagination<Marca> pagination = ms.GetMarcaOrderByGanho(1, -1);
            FillMarcaPage(pagination);
        }

        private void Asc_Click(object sender, EventArgs e)
        {
            asc = true;
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");

            if ("Ganho".Equals(orderby))
            {
                Pagination<Marca> pagination = ms.GetMarcaOrderByGanho(1,1);
                FillMarcaPage(pagination);
            }else if ("Venda".Equals(orderby))
            {
                Pagination<Marca> pagination = ms.GetMarcaOrderByVenda(1, 1);
                FillMarcaPage(pagination);
            }else if ("Compra".Equals(orderby))
            {
                Pagination<Marca> pagination = ms.GetMarcaOrderByCompra(1,1);
                FillMarcaPage(pagination);
            }
        }

        private void Desc_Click(object sender, EventArgs e)
        {
            asc = false;
            MarcaListView.Items.Clear();
            MarcaService ms = (MarcaService)ServiceFactory.GetInstance("MarcaService");

            if ("Ganho".Equals(orderby))
            {
                Pagination<Marca> pagination = ms.GetMarcaOrderByGanho(1, -1);
                FillMarcaPage(pagination);
            }
            else if ("Venda".Equals(orderby))
            {
                Pagination<Marca> pagination = ms.GetMarcaOrderByVenda(1, -1);
                FillMarcaPage(pagination);
            }
            else if ("Compra".Equals(orderby))
            {
                Pagination<Marca> pagination = ms.GetMarcaOrderByCompra(1, -1);
                FillMarcaPage(pagination);
            }
        }
    }
}
