using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseManager;
using DTOs;
using Factorys;
using WindowsFormsApp1;

namespace Services
{
    public abstract class Service
    {
        protected static TransactionManager transaction = new TransactionManager();
        protected static int size = 10;
        protected bool verifySGBDConnection()
        {
            return ConnectionUtils.verifySGBDConnection();
        }

        public void Close()
        {
            ConnectionUtils.Release();
        }

    }

    public class ProdutoService : Service
    {
        private IvaService ivaService;
        private int totalProducts; //numero total de produtos
        private int searchTotal; //numero total de produtos da pesquisa

        public ProdutoService() : base() {
            ivaService = (IvaService)ServiceFactory.GetInstance("IvaService");
            GetTotalCount();
        }

        public List<Produto> GetProdutosByEncomendaId(int EncomendaID)
        {
            List<Produto> produtos = new List<Produto>();
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getProdutosByEncomendaId(@ID)";
            cmd.Parameters.AddWithValue("@ID", EncomendaID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Produto produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                produto.Nome = reader["Nome"].ToString();
                produto.Preco = double.Parse(reader["Preco"].ToString());
                produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                produto.Marca = reader["Marca"].ToString();
                produto.Disponivel = int.Parse(reader["Disponivel"].ToString()) == 0 ? true : false;
                produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                produtos.Add(produto);
            }
            reader.Close();
            ConnectionUtils.Release();
            return produtos;
        }

        public Pagination<Produto> GetList(int page)
        {
            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Produto> pagination = InitPagination(page, this.totalProducts);
            int startLine = size * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getProdutoList(@Size,@StartLine)";
            cmd.Parameters.AddWithValue("@Size", size);
            cmd.Parameters.AddWithValue("@StartLine", startLine);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Produto produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                produto.Nome = reader["Nome"].ToString();
                produto.Preco = double.Parse(reader["Preco"].ToString());
                produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                produto.Marca = reader["Marca"].ToString();
                produto.Disponivel = int.Parse(reader["Disponivel"].ToString()) == 0 ? true : false;
                produto.Armazem = int.Parse(reader["Armazem"].ToString());
                produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                produto.Tipo = reader["Tipo"].ToString();
                pagination.Data.Add(produto);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        // AQUI
        public Produto GetOneByCodigo(int codigo)
        {
            if (!verifySGBDConnection())
            {
                return null;
            }

            Produto produto = null;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getProdutoByCodigo(@Codigo)";
            cmd.Parameters.AddWithValue("@Codigo", codigo);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                produto.Nome = reader["Nome"].ToString();
                produto.Preco = double.Parse(reader["Preco"].ToString());
                produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                produto.Marca = reader["Marca"].ToString();
                produto.Disponivel = int.Parse(reader["Disponivel"].ToString()) == 0 ? true : false;
                produto.Armazem = int.Parse(reader["Armazem"].ToString());
                produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
            }
            reader.Close();
            ConnectionUtils.Release();
            return produto;
        }

        public int CreateNewProduct(Produto produto)
        {
            this.totalProducts++;
            return -1;
        }
        
        // AQUI
        private void GetTotalCount()
        {
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getCountProdutos()";
            this.totalProducts = (int)cmd.ExecuteScalar();
        }

        public Pagination<Produto> Search(int page, string name, string brand, string tipo, int min, int max)
        {
            if (page == -1)
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchProdutoAndGetQuantity(@Size,@Marca,@Min,@Max,@Name,@Tipo)";

                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@Marca", brand);
                cmd.Parameters.AddWithValue("@Min", min);
                cmd.Parameters.AddWithValue("@Max", max);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                SqlDataReader reader = cmd.ExecuteReader();
                Pagination<Produto> pagination = null;
                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);
                    this.searchTotal = (int)reader["Totalcount"];
                    Produto produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                    produto.Nome = reader["Nome"].ToString();
                    produto.Preco = double.Parse(reader["Preco"].ToString());
                    produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                    produto.Marca = reader["Marca"].ToString();
                    produto.Disponivel = int.Parse(reader["Disponivel"].ToString()) == 0 ? true : false;
                    produto.Armazem = int.Parse(reader["Armazem"].ToString());
                    produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                    produto.Tipo = reader["Tipo"].ToString();
                    pagination.Data.Add(produto);

                    while (reader.Read())
                    {
                        produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                        produto.Nome = reader["Nome"].ToString();
                        produto.Preco = double.Parse(reader["Preco"].ToString());
                        produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                        produto.Marca = reader["Marca"].ToString();
                        produto.Disponivel = int.Parse(reader["Disponivel"].ToString()) == 0 ? true : false;
                        produto.Armazem = int.Parse(reader["Armazem"].ToString());
                        produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                        produto.Tipo = reader["Tipo"].ToString();
                        pagination.Data.Add(produto);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
            else
            {
                Pagination<Produto> pagination = InitPagination(page, this.searchTotal);
                int startLine = size * (pagination.CurrentPage - 1);
                startLine = startLine < 0 ? 0 : startLine;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchProduct(@Size,@StartLine,@Marca,@Min,@Max,@Name,@Tipo)";

                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@StartLine", startLine);
                cmd.Parameters.AddWithValue("@Marca", brand);
                cmd.Parameters.AddWithValue("@Min", min);
                cmd.Parameters.AddWithValue("@Max", max);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Tipo", tipo);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Produto produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                    produto.Nome = reader["Nome"].ToString();
                    produto.Preco = double.Parse(reader["Preco"].ToString());
                    produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                    produto.Marca = reader["Marca"].ToString();
                    produto.Disponivel = int.Parse(reader["Disponivel"].ToString()) == 0 ? true : false;
                    produto.Armazem = int.Parse(reader["Armazem"].ToString());
                    produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                    produto.Tipo = reader["Tipo"].ToString();
                    pagination.Data.Add(produto);
                }
                pagination.Search = true;
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
        }

        public List<Produto> GetProdutosByFornecedorId(string fornecedor)
        {
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getProdutosByFornecedorName(@Name);";
            cmd.Parameters.AddWithValue("@Name", fornecedor);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Produto> produtos = new List<Produto>();
            while (reader.Read())
            {
                Produto produto = new Produto(int.Parse(reader["Codigo"].ToString()));
                produto.Nome = reader["Nome"].ToString();
                produto.Iva = ivaService.GetIva(int.Parse(reader["IvaCodigo"].ToString()));
                produto.Preco = Math.Round(double.Parse(reader["Preco"].ToString()) / 1.1 / (1+produto.Iva.IvaImposto),2);
                produto.Marca = reader["Marca"].ToString();
                produto.Tipo = reader["Tipo"].ToString();
                produtos.Add(produto);
            }
            reader.Close();
            ConnectionUtils.Release();
            return produtos;
        }
        private Pagination<Produto> InitPagination(int page,int totalItems)
        {
            int totalPage;
            if (totalItems % size == 0)
            {
                totalPage = totalItems / size;
            }
            else
            {
                totalPage = totalItems / size + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Produto> pagination = new Pagination<Produto>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }

    public class IvaService : Service
    {
        private static Dictionary<int, IVA> ivas;

        public IvaService() : base() {

            if (ivas == null)
            {
                InitIvas();
            }
        }

        // AQUI
        private void InitIvas()
        {
            if (!verifySGBDConnection())
                return;

            ivas = new Dictionary<int, IVA>();
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.initIvas()";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader["Codigo"].ToString());
                double imposto = double.Parse(reader["imposto"].ToString());
                IVA iva = new IVA(id, imposto);
                ivas.Add(id, iva);
            }
            reader.Close();
            ConnectionUtils.Release();
        }

        public IVA GetIva(int id)
        {
            if (ivas.ContainsKey(id))
            {
                return ivas[id];
            }
            return null;
        }

        public void Reload()
        {
            InitIvas();
        }
    }
    
    public class UtilizadorService:Service{

        public UtilizadorService():base(){}

        public int CreateNewUser(Pessoa pessoa, string username, string password)
        {
            if (!verifySGBDConnection())
                return -1;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cmd.CommandText = "SuperMercado.createNewUser";
                cmd.Parameters.AddWithValue("@Nome", pessoa.PersonNome);
                cmd.Parameters.AddWithValue("@Email", pessoa.PersonEmail);
                cmd.Parameters.AddWithValue("@Telefone", pessoa.PersonTelefone);
                cmd.Parameters.AddWithValue("@Genero", pessoa.PersonGenero);
                cmd.Parameters.AddWithValue("@Idade", pessoa.PersonIdade);
                cmd.Parameters.AddWithValue("@Salario", pessoa.PersonSalario);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Senha", password);
                int result = cmd.ExecuteNonQuery();
                return result;
            }catch(SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
            }
            return -1;
        }

        // AQUI
        public Utilizador CheckIsExiste(string email, string pwd)
        {
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.checkIfUserExists(@Email,@Senha)";
            cmd.Parameters.AddWithValue("@Email",email);
            cmd.Parameters.AddWithValue("Senha", pwd);
            SqlDataReader reader = cmd.ExecuteReader();

            Utilizador utilizador = null;
            if (reader.Read())
            {
                utilizador = new Utilizador();
                string username = reader["Name"].ToString();
                int id = (int)reader["ID"];
                utilizador.UserName = username;
                utilizador.PersonID = id;
            }
            reader.Close();
            return utilizador;
        }

        internal int CreateAdmin(Pessoa pessoa, string username, string password)
        {
            if (!verifySGBDConnection())
                return -1;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cmd.CommandText = "SuperMercado.createAdmin";
                cmd.Parameters.AddWithValue("@Nome", pessoa.PersonNome);
                cmd.Parameters.AddWithValue("@Email", pessoa.PersonEmail);
                cmd.Parameters.AddWithValue("@Telefone", pessoa.PersonTelefone);
                cmd.Parameters.AddWithValue("@Genero", pessoa.PersonGenero);
                cmd.Parameters.AddWithValue("@Idade", pessoa.PersonIdade);
                cmd.Parameters.AddWithValue("@Salario", pessoa.PersonSalario);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Senha", password);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
            }
            return -1;
        }
    };
    public class ClienteService : Service
    {

        public ClienteService() : base() { }

        // AQUI
        public Cliente GetClienteById(int id)
        {
            if (!verifySGBDConnection())
                return null;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getClientByID(@ID)";
            cmd.Parameters.AddWithValue("@ID", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string nome = reader["Nome"].ToString();
                int Idade = int.Parse(reader["Idade"].ToString());
                string Email = reader["Email"].ToString();
                string Genero = reader["Genero"].ToString();
                string NIF = reader["NIF"].ToString();
                string endereco = reader["Endereco"].ToString();
                string telefone = reader["Telefone"].ToString();

                Cliente cliente = new Cliente(id, NIF, endereco)
                {
                    PersonIdade = Idade,
                    PersonEmail = Email,
                    PersonGenero = Genero,
                    PersonNome = nome,
                    PersonTelefone = telefone
                };
                reader.Close();
                ConnectionUtils.Release();
                return cliente;
            }
            reader.Close();
            return null;
        }

        public int UpdateNIF(Cliente cliente)
        {
            if (!verifySGBDConnection())
                return -1;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.UpdateClient";
            cmd.Parameters.AddWithValue("@ID", cliente.PersonID);
            cmd.Parameters.AddWithValue("@NIF", cliente.ClientNIF);
            try
            {
                int result = -1;
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
            }
            return -1;
        }
    };
    public class EncomendaService : Service
    {
        private int totalEncomendas;
        private int totalEncomendasForCliente;
        private int NaoEntregadosTotal;
        private int EntregadosTotal;

        public EncomendaService() : base() {  }

        public int EntregarEncomenda(Encomenda encomenda)
        {
            if (!verifySGBDConnection())
            {
                return -1;
            }

            CondutorService cs = (CondutorService)ServiceFactory.GetInstance("CondutorService");
            int id = cs.GetCondutor().CondutorID;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.entregarEncomenda";

            cmd.Parameters.AddWithValue("@Encomenda",encomenda.EncomendaID);
            cmd.Parameters.AddWithValue("@Condutor", id);
            int result = cmd.ExecuteNonQuery();
            ConnectionUtils.Release();
            return result;
        }

        // AQUI
        private void GetTotalCount(int clientID)
        {
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getCountEncomendas(@ID)";
            cmd.Parameters.AddWithValue("@ID", clientID);
            if (clientID == -1)
            {
                totalEncomendas = (int)cmd.ExecuteScalar();
            }
            else
            {
                totalEncomendasForCliente = (int)cmd.ExecuteScalar();
            }
           
        }
        public int CreateEncomenda(Encomenda encomenda,List<Contem> contems,List<Armazena>armazenas)
        {
            if (!verifySGBDConnection())
                return -1;

            if (totalEncomendasForCliente == 0||totalEncomendas==0)
            {
                GetTotalCount(encomenda.EncomendaClient);
            }
            transaction.BeginTransaction();
            ArmazenaService armazena = (ArmazenaService)ServiceFactory.GetInstance("ArmazenaService");
            ContemService contem = (ContemService)ServiceFactory.GetInstance("ContemService");
            string sql = "Insert into SuperMercado.Encomenda (Client,Funcionario,CondicoesPagamento,Destino,CustoTotal,Estado,Data) output  inserted.id" +
                        " values(@Cliente,@Funcionario,@CondicoesPagamento,@Destino,@CustoTotal,@Estado,@Data);";
  
            try
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@Cliente", encomenda.EncomendaClient);
                cmd.Parameters.AddWithValue("@Funcionario", encomenda.EncomendaFuncionario);
                cmd.Parameters.AddWithValue("@CondicoesPagamento", encomenda.EncomendaCondicoesPagamento);
                cmd.Parameters.AddWithValue("@Destino", encomenda.EncomendaDestino);
                cmd.Parameters.AddWithValue("CustoTotal", encomenda.EncomendaCustoTotal);
                cmd.Parameters.AddWithValue("@Estado", encomenda.EncomendaEstado);
                cmd.Parameters.AddWithValue("@Data", DateTime.Now.ToShortDateString());
                int id = (int)cmd.ExecuteScalar();
            
                for(int i = 0; i < contems.Count; i++)
                {
                    contems[i].Encomenda = id;
                    contem.SetEncomendaProdutos(contems[i]);
                    armazena.UpdateArmazena(armazenas[i]);
                }
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                totalEncomendasForCliente--;
            }
            finally
            {
                transaction.Release();
                totalEncomendasForCliente++;
            }

            return -1;
        }

        public Pagination<Encomenda> GetEncomendaOfClient(int page,int clientID)
        {
            if (!verifySGBDConnection())
            {
                return null;
            }
            if (totalEncomendasForCliente == 0 || totalEncomendas == 0)
            {
                GetTotalCount(clientID);
            }
            Pagination<Encomenda> pagination = InitPagination(page, totalEncomendas);
            int startLine = 15 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getEncomendaOfClient(@Size, @StartLine, @ClientID)";
            cmd.Parameters.AddWithValue("@Size",size);
            cmd.Parameters.AddWithValue("@StartLine", startLine);
            cmd.Parameters.AddWithValue("@ClientID", clientID);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                Encomenda encomenda = new Encomenda(int.Parse(reader["ID"].ToString()), int.Parse(reader["Funcionario"].ToString()), reader["CondicoesPagamento"].ToString(), reader["Destino"].ToString());
                encomenda.EncomendaClient = (int) reader["Client"];
                encomenda.EncomendaCustoTotal = (double) reader["CustoTotal"];
                encomenda.Pago = (int)reader["Pago"] == 0 ? false : true;
                encomenda.EncomendaEstado = (int)reader["Estado"] == 1 ? true : false;
                encomenda.EncomendaDate = DateTime.Parse(reader["Data"].ToString());
                encomenda.EncomendaID = (int)reader["id"];
                pagination.Data.Add(encomenda);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }


        public Pagination<Encomenda> GetEncomandasByEstado(int page, bool estado,int clienteID)
        {
            if (totalEncomendas == 0)
            {
                GetTotalCount(clienteID);
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            if (page == -1)
            {
                SqlCommand cmd = transaction.GetCmd();
                Pagination<Encomenda> pagination = null;
                cmd.CommandText = "SELECT * FROM SuperMercado.getEncomandasByEstadoAndGetQuantity(@Size,@Estado, @ClientID)";
                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@Estado", estado == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@ClientID", clienteID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);

                    if (estado)
                    {
                        this.EntregadosTotal = (int)reader["Totalcount"];
                    }
                    else
                    {
                        this.NaoEntregadosTotal = (int)reader["Totalcount"];
                    }

                    Encomenda encomenda = new Encomenda(int.Parse(reader["ID"].ToString()), int.Parse(reader["Funcionario"].ToString()), reader["CondicoesPagamento"].ToString(), reader["Destino"].ToString());
                    encomenda.EncomendaCustoTotal = double.Parse(reader["CustoTotal"].ToString());
                    encomenda.Pago = (int)reader["Pago"] == 0 ? false : true;
                    encomenda.EncomendaEstado = (int)reader["Estado"] == 1 ? true : false;
                    encomenda.EncomendaDate = DateTime.Parse(reader["Data"].ToString());
                    encomenda.EncomendaID = (int)reader["id"];
                    pagination.Data.Add(encomenda);
                    while (reader.Read())
                    {
                        encomenda = new Encomenda(int.Parse(reader["ID"].ToString()), int.Parse(reader["Funcionario"].ToString()), reader["CondicoesPagamento"].ToString(), reader["Destino"].ToString());
                        encomenda.EncomendaCustoTotal = double.Parse(reader["CustoTotal"].ToString());
                        encomenda.Pago = (int)reader["Pago"] == 0 ? false : true;
                        encomenda.EncomendaEstado = (int)reader["Estado"] == 1 ? true : false;
                        encomenda.EncomendaDate = DateTime.Parse(reader["Data"].ToString());
                        encomenda.EncomendaID = (int)reader["id"];
                        pagination.Data.Add(encomenda);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;

            }
            else
            {
                Pagination<Encomenda> pagination;
                if (estado)
                {
                    pagination = InitPagination(page, this.EntregadosTotal);
                }
                else
                {
                    pagination = InitPagination(page, this.NaoEntregadosTotal);
                }
                
                int startLine = 15 * (pagination.CurrentPage - 1);
                startLine = startLine < 0 ? 0 : startLine;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.getEncomandasByEstado(@Size,@StartLine, @Estado, @ClientID)";
                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@StartLine", startLine);
                cmd.Parameters.AddWithValue("@Estado", estado == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@ClientID", clienteID);
                
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Encomenda encomenda = new Encomenda(int.Parse(reader["ID"].ToString()), int.Parse(reader["Funcionario"].ToString()), reader["CondicoesPagamento"].ToString(), reader["Destino"].ToString());
                    encomenda.EncomendaCustoTotal = double.Parse(reader["CustoTotal"].ToString());
                    encomenda.Pago = (int)reader["Pago"] == 0 ? false : true;
                    encomenda.EncomendaEstado = (int)reader["Estado"] == 1 ? true : false;
                    encomenda.EncomendaDate = DateTime.Parse(reader["Data"].ToString());
                    encomenda.EncomendaID = (int)reader["id"];
                    pagination.Data.Add(encomenda);
                    while (reader.Read())
                    {
                        encomenda = new Encomenda(int.Parse(reader["ID"].ToString()), int.Parse(reader["Funcionario"].ToString()), reader["CondicoesPagamento"].ToString(), reader["Destino"].ToString());
                        encomenda.EncomendaCustoTotal = double.Parse(reader["CustoTotal"].ToString());
                        encomenda.Pago = (int)reader["Pago"] == 0 ? false : true;
                        encomenda.EncomendaEstado = (int)reader["Estado"] == 1 ? true : false;
                        encomenda.EncomendaDate = DateTime.Parse(reader["Data"].ToString());
                        encomenda.EncomendaID = (int)reader["id"];
                        pagination.Data.Add(encomenda);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
            
        }
        private Pagination<Encomenda> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % 15 == 0)
            {
                totalPage = totalItems / 15;
            }
            else
            {
                totalPage = totalItems / 15 + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage && totalItems != 0)
                page = totalPage;

            Pagination<Encomenda> pagination = new Pagination<Encomenda>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }
    public class FaturaService : Service
    {
        private int totalFatura;

        public FaturaService() : base() { }

        // AQUI
        private void GetTotalCount(int clientID)
        {
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SELECT SuperMercado.getCountFaturas(@ID)";
            cmd.Parameters.AddWithValue("@ID", clientID);
            this.totalFatura = (int)cmd.ExecuteScalar();
        }

        // AQUI
        public int CreateFatura(Fatura fatura)
        {
            try
            {
                if (!verifySGBDConnection())
                    return -1;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SuperMercado.createNewFatura";
                cmd.Parameters.AddWithValue("@Encomenda", fatura.Encomenda);
                cmd.Parameters.AddWithValue("@Cliente", fatura.Cliente);
                cmd.Parameters.AddWithValue("@Funcionario", fatura.Funcionario);
                cmd.Parameters.AddWithValue("@CondicoesPagamento", fatura.CondicoesPagamento);
                cmd.Parameters.AddWithValue("@CustoTotal", fatura.CustoTotal);
                cmd.Parameters.AddWithValue("@Data", DateTime.Now.ToShortDateString());
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
                totalFatura++;
            }
            return 1;
        }

        public Pagination<Fatura> GetFaturaOfClient(int clientID)
        {
            if (!verifySGBDConnection())
            {
                return null;
            }

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getFaturaOfClient(@ClientID)";
            cmd.Parameters.AddWithValue("@ClientID", clientID);

            SqlDataReader reader = cmd.ExecuteReader();
            Pagination<Fatura> list = new Pagination<Fatura>();
            while (reader.Read())
            {
                Fatura fatura = new Fatura(int.Parse(reader["ID"].ToString()), int.Parse(reader["Encomenda"].ToString()), int.Parse(reader["Client"].ToString()), int.Parse(reader["Funcionario"].ToString()), DateTime.Parse(reader["Data"].ToString()));
                fatura.CondicoesPagamento = reader["CondicoesPagamento"].ToString();
                fatura.CustoTotal = Double.Parse(reader["CustoTotal"].ToString());
                
                list.Data.Add(fatura);
            }
            reader.Close();
            ConnectionUtils.Release();
            return list;
        }

        public Pagination<Fatura> GetFaturaByPage(int page, int clienteID)
        {
            if (this.totalFatura == 0)
            {
                GetTotalCount(clienteID);
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            if (page == -1)
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.getFaturaByPageAndGetQuantity(@Size,@ClientID)";
                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@ClientID", clienteID);
                SqlDataReader reader = cmd.ExecuteReader();

                Pagination<Fatura> pagination = null;
                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);
                    int encomenda = (int)reader["Encomenda"];
                    int funcionario = (int)reader["Funcionario"];
                    string cp = reader["CondicoesPagamento"].ToString();
                    DateTime date = DateTime.Parse(reader["Data"].ToString());
                    double custo = (double)reader["CustoTotal"];
                    int id = (int)reader["id"];
                    Fatura fatura = new Fatura(id,encomenda,clienteID,funcionario,date);
                    fatura.CondicoesPagamento = cp;
                    fatura.CustoTotal = custo;
                    pagination.Data.Add(fatura);

                    while (reader.Read())
                    {
                        pagination = InitPagination(1, (int)reader["Totalcount"]);
                        encomenda = (int)reader["Encomenda"];
                        funcionario = (int)reader["Funcionario"];
                        cp = reader["CondicoesPagamento"].ToString();
                        date = DateTime.Parse(reader["Data"].ToString());
                        custo = (double)reader["CustoTotal"];
                        id = (int)reader["id"];
                        fatura = new Fatura(id, encomenda, clienteID, funcionario, date);
                        fatura.CondicoesPagamento = cp;
                        fatura.CustoTotal = custo;
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;

            }
            else
            {
                Pagination<Fatura> pagination = null;
                int startLine = 15 * (pagination.CurrentPage - 1);
                startLine = startLine < 0 ? 0 : startLine;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.getFaturaByPage(@Size,@StartLine, @ClientID)";
                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@StartLine", startLine);
                cmd.Parameters.AddWithValue("@ClientID", clienteID);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);
                    int encomenda = (int)reader["Encomenda"];
                    int funcionario = (int)reader["Funcionario"];
                    string cp = reader["CondicoesPagamento"].ToString();
                    DateTime date = DateTime.Parse(reader["Data"].ToString());
                    double custo = (double)reader["CustoTotal"];
                    int id = (int)reader["id"];
                    Fatura fatura = new Fatura(id, encomenda, clienteID, funcionario, date);
                    fatura.CondicoesPagamento = cp;
                    fatura.CustoTotal = custo;
                    pagination.Data.Add(fatura);
                    while (reader.Read())
                    {
                        pagination = InitPagination(1, (int)reader["Totalcount"]);
                        encomenda = (int)reader["Encomenda"];
                        funcionario = (int)reader["Funcionario"];
                        cp = reader["CondicoesPagamento"].ToString();
                        date = DateTime.Parse(reader["Data"].ToString());
                        custo = (double)reader["CustoTotal"];
                        id = (int)reader["id"];
                        fatura = new Fatura(id, encomenda, clienteID, funcionario, date);
                        fatura.CondicoesPagamento = cp;
                        fatura.CustoTotal = custo;
                        pagination.Data.Add(fatura);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }

        }
        private Pagination<Fatura> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % 15 == 0)
            {
                totalPage = totalItems / 15;
            }
            else
            {
                totalPage = totalItems / 15 + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Fatura> pagination = new Pagination<Fatura>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }
    public class FuncionarioService : Service
    {
        private int totalFuncionario;
        private int searchTotal;

        public FuncionarioService() : base() { }

        // AQUI
        private void GetAllFuncionario()
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getAllFuncionario()";
            totalFuncionario = (int)cmd.ExecuteScalar();
            ConnectionUtils.Release();
        }

        // AQUI
        public int UpdateFuncionario(Funcionario funcionario)
        {
            if (this.totalFuncionario == 0)
            {
                GetAllFuncionario();
            }

            if (!verifySGBDConnection())
                return -1;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.updateFuncionario";
            cmd.Parameters.AddWithValue("@Nome",funcionario.PersonNome);
            cmd.Parameters.AddWithValue("@Telefone", funcionario.PersonTelefone);
            cmd.Parameters.AddWithValue("@Salario", funcionario.PersonSalario);
            cmd.Parameters.AddWithValue("@ID", funcionario.PersonID);
            return cmd.ExecuteNonQuery();
        }

        // AQUI
        public Funcionario GetFuncionario()
        {
            if (this.totalFuncionario == 0)
            {
                GetAllFuncionario();
            }
            if (!verifySGBDConnection())
                return null;

            Funcionario funcionario = null;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.getRandomFuncionario";

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                funcionario=new Funcionario((int)reader["id"]);
            }
            reader.Close();
            return funcionario;
        }

        // AQUI
        public Funcionario GetFuncionarioByID(int id)
        {
            if (this.totalFuncionario == 0)
            {
                GetAllFuncionario();
            }
            if (!verifySGBDConnection())
                return null;

            Funcionario funcionario;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getFuncionarioByID(@ID)";
            cmd.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                funcionario = new Funcionario(int.Parse(reader["id"].ToString()));
                funcionario.PersonNome = reader["Nome"].ToString();
                funcionario.PersonTelefone = reader["Telefone"].ToString();
                funcionario.PersonSalario = Double.Parse(reader["Salario"].ToString());
                funcionario.PersonGenero = reader["Genero"].ToString();
                reader.Close();
                return funcionario;
            }

            return null;
        }

        public int CreateNewFuncionario(Pessoa pessoa)
        {
            if (this.totalFuncionario == 0)
            {
                GetAllFuncionario();
            }

            if (!verifySGBDConnection())
                return -1;

            try
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SuperMercado.createNewFuncionario";
                cmd.Parameters.AddWithValue("@Nome", pessoa.PersonNome);
                cmd.Parameters.AddWithValue("@Email", pessoa.PersonEmail);
                cmd.Parameters.AddWithValue("@Telefone", pessoa.PersonTelefone);
                cmd.Parameters.AddWithValue("@Genero", pessoa.PersonGenero);
                cmd.Parameters.AddWithValue("@Idade", pessoa.PersonIdade);
                cmd.Parameters.AddWithValue("@Salario", pessoa.PersonSalario);
                int result = cmd.ExecuteNonQuery();
                totalFuncionario++;
                return result;
            }
            catch (Exception e)
            {
               
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
            }
            return -1;
        }

        //TODO
        public Pagination<Funcionario> GetFuncionariosList(int page)
        {
            if (this.totalFuncionario == 0)
            {
                GetAllFuncionario();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Funcionario> pagination = InitPagination(page, this.totalFuncionario);
            int startLine = 12 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getFuncionariosList(@Size,@StartLine)";
            cmd.Parameters.AddWithValue("@Size", 12);
            cmd.Parameters.AddWithValue("@StartLine", startLine);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Funcionario funcionario = new Funcionario(int.Parse(reader["id"].ToString()));
                funcionario.PersonNome = reader["Nome"].ToString();
                funcionario.PersonTelefone = reader["Telefone"].ToString();
                funcionario.PersonSalario = double.Parse(reader["Salario"].ToString());
                funcionario.PersonGenero = reader["Genero"].ToString();

                pagination.Data.Add(funcionario);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        //TODO
        public Pagination<Funcionario> Search(int page, string name, string email,int min, int max)
        {
            if (page == -1)
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchFuncionarioAndGetQuantity(@Size,@Name,@Email,@Min, @Max)";
                cmd.Parameters.AddWithValue("@Size", 12);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Min", min);
                cmd.Parameters.AddWithValue("@Max", max);
                cmd.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = cmd.ExecuteReader();
                Pagination<Funcionario> pagination = null;
                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);
                    this.searchTotal = (int)reader["Totalcount"];
                    Funcionario funcionario = new Funcionario(int.Parse(reader["id"].ToString()));
                    funcionario.PersonNome = reader["Nome"].ToString();
                    funcionario.PersonTelefone = reader["Telefone"].ToString();
                    funcionario.PersonSalario = double.Parse(reader["Salario"].ToString());
                    funcionario.PersonGenero = reader["Genero"].ToString();
                    pagination.Data.Add(funcionario);

                    while (reader.Read())
                    {
                        funcionario = new Funcionario(int.Parse(reader["id"].ToString()));
                        funcionario.PersonNome = reader["Nome"].ToString();
                        funcionario.PersonTelefone = reader["Telefone"].ToString();
                        funcionario.PersonSalario = double.Parse(reader["Salario"].ToString());
                        funcionario.PersonGenero = reader["Genero"].ToString();
                        pagination.Data.Add(funcionario);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
            else
            {
                Pagination<Funcionario> pagination = InitPagination(page, this.searchTotal);
                int startLine = 12 * (pagination.CurrentPage - 1);
                startLine = startLine < 0 ? 0 : startLine;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchFuncionario(@Size,@StartLine, @Name,@Email,@Min, @Max)";
                cmd.Parameters.AddWithValue("@Size", 12);
                cmd.Parameters.AddWithValue("@StartLine", startLine);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Min", min);
                cmd.Parameters.AddWithValue("@Max", max);
                cmd.Parameters.AddWithValue("@Name", name);


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Funcionario funcionario = new Funcionario(int.Parse(reader["id"].ToString()));
                    funcionario = new Funcionario(int.Parse(reader["id"].ToString()));
                    funcionario.PersonNome = reader["Nome"].ToString();
                    funcionario.PersonTelefone = reader["Telefone"].ToString();
                    funcionario.PersonSalario = double.Parse(reader["Salario"].ToString());
                    funcionario.PersonGenero = reader["Genero"].ToString();
                    pagination.Data.Add(funcionario);
                }
                pagination.Search = true;
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
        }

        private Pagination<Funcionario> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % 12 == 0)
            {
                totalPage = totalItems / 12;
            }
            else
            {
                totalPage = totalItems / 12 + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Funcionario> pagination = new Pagination<Funcionario>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }
    public class ArmazenaService : Service
    {
        public ArmazenaService() : base() { }

        // AQUI
        public int UpdateArmazena(Armazena armazena)
        {
            if (!verifySGBDConnection())
            {
                return -1;
            }

            int flag = CheckedStock(armazena);
            if (flag == 0)
            {
                MessageBox.Show("Stock insuficiente para produto " + armazena.ArmazenaProduto + "!");
                return -1;
            }
            else if (flag == -1)
            {
                MessageBox.Show("Ligacao falhou!");
                return -1;
            }

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.updateArmazena";
            cmd.Parameters.AddWithValue("@Quantidade", armazena.ArmazenaQuantidade);
            cmd.Parameters.AddWithValue("@Produto", armazena.ArmazenaProduto);
            cmd.ExecuteNonQuery();
            return 1;
        }

        // AQUI
        public int CheckedStock(Armazena armazena)
        {
            if (!verifySGBDConnection())
            {
                return -1;
            }

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.checkStocked(@Produto)";
            cmd.Parameters.AddWithValue("@Produto", armazena.ArmazenaProduto);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int stock = (int)reader["Quantidade"];
                reader.Close();
                if (stock < armazena.ArmazenaQuantidade)
                {
                    return 0;
                }
                return 1;
            }
            reader.Close();
            return 0;
        }
    }
    public class CondutorService : Service
    {
        private int totalCondutor;
        private int searchTotal;
        public CondutorService() : base() { }
        
        // AQUI
        private void GetAllCondutor()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getAllCondutor()";
            this.totalCondutor = (int)cmd.ExecuteScalar();
            ConnectionUtils.Release();
        }

        // AQUI
        public Condutor GetCondutor()
        {
            if (this.totalCondutor == 0)
            {
                GetAllCondutor();
            }
            if (!verifySGBDConnection())
                return null;

            Condutor condutor=null;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.getRandomCondutor";

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = (int)reader["ID"];
                string carta = reader["CartaoConducao"].ToString();
                condutor = new Condutor(id, carta);
            }
            reader.Close();
            ConnectionUtils.Release();
            return condutor;
        }

        public int CreateNewCondutor(Pessoa pessoa, string carta)
        {
            if (this.totalCondutor == 0)
            {
                GetAllCondutor();
            }

            if (!verifySGBDConnection())
                return -1;

            try
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SuperMercado.createNewCondutor";
                cmd.Parameters.AddWithValue("@Nome", pessoa.PersonNome);
                cmd.Parameters.AddWithValue("@Email", pessoa.PersonEmail);
                cmd.Parameters.AddWithValue("@Telefone", pessoa.PersonTelefone);
                cmd.Parameters.AddWithValue("@Genero", pessoa.PersonGenero);
                cmd.Parameters.AddWithValue("@Idade", pessoa.PersonIdade);
                cmd.Parameters.AddWithValue("@Salario", pessoa.PersonSalario);
                cmd.Parameters.AddWithValue("@Carta", carta);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
            }
            return -1;
        }

        //TODO
        public Pagination<Condutor> GetCondutoresList(int page)
        {
            if (this.totalCondutor == 0)
            {
                GetAllCondutor();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Condutor> pagination = InitPagination(page, this.totalCondutor);
            int startLine = 12 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getCondutoresList(@Size,@StartLine)";
            cmd.Parameters.AddWithValue("@Size", 12);
            cmd.Parameters.AddWithValue("@StartLine", startLine);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Condutor condutor = new Condutor(int.Parse(reader["id"].ToString()), reader["CartaoConducao"].ToString());
                condutor.PersonNome = reader["Nome"].ToString();
                condutor.PersonTelefone = reader["Telefone"].ToString();
                condutor.PersonSalario = double.Parse(reader["Salario"].ToString());
                if (int.Parse(reader["Disponivel"].ToString()) == 1)
                {
                    bool disponivel = true;
                    condutor.CondutorDisponivel = disponivel;
                }
                else
                {
                    bool disponivel = false;
                    condutor.CondutorDisponivel = disponivel;
                }

                pagination.Data.Add(condutor);
            }
            reader.Close();
            return pagination;
        }

        //TODO
        public Pagination<Condutor> Search(int page, string name, string email, int min, int max)
        {
            if (totalCondutor == 0)
            {
                GetAllCondutor();
            }
            if (!verifySGBDConnection())
            {
                return null;
            }

            if (page == -1)
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchCondutorAndGetQuantity(@Size,@Name,@Email,@Min, @Max)";
                cmd.Parameters.AddWithValue("@Size", size);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Min", min);
                cmd.Parameters.AddWithValue("@Max", max);
                cmd.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = cmd.ExecuteReader();
                Pagination<Condutor> pagination = null;
                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);
                    Condutor condutor = new Condutor(int.Parse(reader["id"].ToString()), reader["CartaoConducao"].ToString());
                    condutor.PersonNome = reader["Nome"].ToString();
                    condutor.PersonTelefone = reader["Telefone"].ToString();
                    condutor.PersonSalario = Double.Parse(reader["Salario"].ToString());
                    if (int.Parse(reader["Disponivel"].ToString()) == 1)
                    {
                        bool disponivel = true;
                        condutor.CondutorDisponivel = disponivel;
                    }
                    else
                    {
                        bool disponivel = false;
                        condutor.CondutorDisponivel = disponivel;
                    }

                    pagination.Data.Add(condutor);

                    while (reader.Read())
                    {
                        condutor = new Condutor(int.Parse(reader["id"].ToString()), reader["CartaoConducao"].ToString());
                        condutor.PersonNome = reader["Nome"].ToString();
                        condutor.PersonTelefone = reader["Telefone"].ToString();
                        condutor.PersonSalario = Double.Parse(reader["Salario"].ToString());
                        if (int.Parse(reader["Disponivel"].ToString()) == 1)
                        {
                            bool disponivel = true;
                            condutor.CondutorDisponivel = disponivel;
                        }
                        else
                        {
                            bool disponivel = false;
                            condutor.CondutorDisponivel = disponivel;
                        }

                        pagination.Data.Add(condutor);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
            else
            {
                Pagination<Condutor> pagination = InitPagination(page, this.searchTotal);
                int startLine = 12 * (pagination.CurrentPage - 1);
                startLine = startLine < 0 ? 0 : startLine;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchCondutor(@Size,@StartLine, @Name,@Email,@Min, @Max)";
                cmd.Parameters.AddWithValue("@Size", 12);
                cmd.Parameters.AddWithValue("@StartLine", startLine);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Min", min);
                cmd.Parameters.AddWithValue("@Max", max);
                cmd.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Condutor condutor = new Condutor(int.Parse(reader["id"].ToString()), reader["CartaoConducao"].ToString());
                    condutor.PersonNome = reader["Nome"].ToString();
                    condutor.PersonTelefone = reader["Telefone"].ToString();
                    condutor.PersonSalario = Double.Parse(reader["Salario"].ToString());
                    if (int.Parse(reader["Disponivel"].ToString()) == 1)
                    {
                        bool disponivel = true;
                        condutor.CondutorDisponivel = disponivel;
                    }
                    else
                    {
                        bool disponivel = false;
                        condutor.CondutorDisponivel = disponivel;
                    }

                    pagination.Data.Add(condutor);
                }
                pagination.Search = true;
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
        }

        private Pagination<Condutor> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % size == 0)
            {
                totalPage = totalItems / size;
            }
            else
            {
                totalPage = totalItems / size + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Condutor> pagination = new Pagination<Condutor>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }
    public class FornecedorService : Service
    {
        private int totalFornecedor;
        private int searchTotal;

        public FornecedorService() : base() {
            if (totalFornecedor == 0)
            {
                GetAllFornecedores();
            }
        }
        
        // AQUI
        private void GetAllFornecedores()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getAllFornecedores()";
            totalFornecedor = (int)cmd.ExecuteScalar();
            ConnectionUtils.Release();
        }

        // AQUI
        public int CreateNewFornecedor(Fornecedor fornecedor)
        {
            if (totalFornecedor == 0)
            {
                GetAllFornecedores();
            }

            if (!verifySGBDConnection())
                return -1;

            try
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SuperMercado.createNewFornecedor";
                cmd.Parameters.AddWithValue("@Fax", fornecedor.FornecedorFax);
                cmd.Parameters.AddWithValue("@Email", fornecedor.FornecedorEmail);
                cmd.Parameters.AddWithValue("@Nome", fornecedor.FornecedorNome);
                cmd.Parameters.AddWithValue("@NIF", fornecedor.FornecedorNIF);
                cmd.Parameters.AddWithValue("@Tipo", fornecedor.FornecedorTipo);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ConnectionUtils.Release();
            }
            return -1;
        }

        //TODO
        public Pagination<Fornecedor> GetFornecedoresList(int page)
        {
            if (totalFornecedor == 0)
            {
                GetAllFornecedores();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Fornecedor> pagination = InitPagination(page,totalFornecedor);
            int startLine = 10 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getFornecedoresList(@Size,@StartLine)";
            cmd.Parameters.AddWithValue("@Size",10);
            cmd.Parameters.AddWithValue("@StartLine", startLine);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Fornecedor fornecedor = new Fornecedor(reader["Nome"].ToString(), reader["NIF"].ToString());
                fornecedor.FornecedorNome = reader["Nome"].ToString();
                fornecedor.FornecedorNIF = reader["NIF"].ToString();
                fornecedor.FornecedorEmail = reader["Email"].ToString();
                fornecedor.FornecedorFax = reader["Fax"].ToString();
                fornecedor.FornecedorTipo = reader["Tipo"].ToString();
                pagination.Data.Add(fornecedor);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        //TODO
        public Pagination<Fornecedor> Search(int page, string name, string email, string fax, string nif)
        {
            if (page == -1)
            {
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchFornecedorAndGetQuantity(@Size,@Name,@Email,@Fax, @NIF)";
                cmd.Parameters.AddWithValue("@Size", 10);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Fax", fax);
                cmd.Parameters.AddWithValue("@NIF", nif);
                cmd.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = cmd.ExecuteReader();
                Pagination<Fornecedor> pagination = null;
                if (reader.Read())
                {
                    pagination = InitPagination(1, (int)reader["Totalcount"]);
                    this.searchTotal = (int)reader["Totalcount"];
                    Fornecedor fornecedor = new Fornecedor(reader["Nome"].ToString(), reader["NIF"].ToString());
                    fornecedor.FornecedorNome = reader["Nome"].ToString();
                    fornecedor.FornecedorNIF = reader["NIF"].ToString();
                    fornecedor.FornecedorEmail = reader["Email"].ToString();
                    fornecedor.FornecedorFax = reader["Fax"].ToString();
                    fornecedor.FornecedorTipo = reader["Tipo"].ToString();
                    pagination.Data.Add(fornecedor);

                    while (reader.Read())
                    {
                        fornecedor = new Fornecedor(reader["Nome"].ToString(), reader["NIF"].ToString());
                        fornecedor.FornecedorNome = reader["Nome"].ToString();
                        fornecedor.FornecedorNIF = reader["NIF"].ToString();
                        fornecedor.FornecedorEmail = reader["Email"].ToString();
                        fornecedor.FornecedorFax = reader["Fax"].ToString();
                        fornecedor.FornecedorTipo = reader["Tipo"].ToString();
                        pagination.Data.Add(fornecedor);
                    }
                    pagination.Search = true;
                }
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
            else
            {
                Pagination<Fornecedor> pagination = InitPagination(page, searchTotal);
                int startLine = 10 * (pagination.CurrentPage - 1);
                startLine = startLine < 0 ? 0 : startLine;
                SqlCommand cmd = transaction.GetCmd();
                cmd.CommandText = "SELECT * FROM SuperMercado.searchFornecedor(@Size, @StartLine, @Name,@Email,@Fax, @NIF)";
                cmd.Parameters.AddWithValue("@Size", 10);
                cmd.Parameters.AddWithValue("@StartLine", startLine);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Fax", fax);
                cmd.Parameters.AddWithValue("@NIF", nif);
                cmd.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fornecedor fornecedor = new Fornecedor(reader["Nome"].ToString(), reader["NIF"].ToString());
                    fornecedor.FornecedorNome = reader["Nome"].ToString();
                    fornecedor.FornecedorNIF = reader["NIF"].ToString();
                    fornecedor.FornecedorEmail = reader["Email"].ToString();
                    fornecedor.FornecedorFax = reader["Fax"].ToString();
                    fornecedor.FornecedorTipo = reader["Tipo"].ToString();
                    pagination.Data.Add(fornecedor);
                }
                pagination.Search = true;
                reader.Close();
                ConnectionUtils.Release();
                return pagination;
            }
        }
        private Pagination<Fornecedor> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % 10 == 0)
            {
                totalPage = totalItems / size;
            }
            else
            {
                totalPage = totalItems / 10 + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Fornecedor> pagination = new Pagination<Fornecedor>
            {
                TotalPage = totalPage>0?totalPage:1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }
    public class ContemService : Service
    {
        public ContemService() : base() { }

        // AQUI
        public int SetEncomendaProdutos(Contem contem)
        {
            if (!verifySGBDConnection())
            {
                return -1;
            }

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.setEncomendaProdutos";             
            cmd.Parameters.AddWithValue("@Quantidade", contem.Quantidade);
            cmd.Parameters.AddWithValue("@Encomenda", contem.Encomenda);
            cmd.Parameters.AddWithValue("@Codigo", contem.Produto);
            cmd.Parameters.AddWithValue("@Preco", contem.Preco);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

    }
    public class TransporteService : Service
    {
        private int totalTransporte;
        public TransporteService() : base() { }

        // AQUI
        private void GetAllTransporte()
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getAllTransporte()";
            totalTransporte = (int)cmd.ExecuteScalar();
            ConnectionUtils.Release();
        }

        //TODO
        public Pagination<Transporte> GetTransportesList(int page)
        {
            if (this.totalTransporte == 0)
            {
                GetAllTransporte();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Transporte> pagination = InitPagination(page, this.totalTransporte);
            int startLine = 16 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getTransportesList(@Size,@StartLine)";
            cmd.Parameters.AddWithValue("@Size", 16);
            cmd.Parameters.AddWithValue("@StartLine", startLine);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["ID"];
                string matricula = reader["Matricula"].ToString();
                string marca = reader["Marca"].ToString();
                Transporte transporte = new Transporte(id, matricula, marca);
                transporte.Capaciadade = (int)reader["capacidade"];
                pagination.Data.Add(transporte);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        private Pagination<Transporte> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % 16 == 0)
            {
                totalPage = totalItems / 16;
            }
            else
            {
                totalPage = totalItems / 16 + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Transporte> pagination = new Pagination<Transporte>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
    }

    public class MarcaService : Service
    {
        private int totalMarca;

        public MarcaService() : base() { }

        private void GetAllMarca()
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT SuperMercado.getAllMarca()";
            totalMarca = (int)cmd.ExecuteScalar();
            ConnectionUtils.Release();
        }
        public Pagination<Marca> GetMarcaList(int page)
        {
            if (this.totalMarca == 0)
            {
                GetAllMarca();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Marca> pagination = InitPagination(page, this.totalMarca);
            int startLine = 14 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getMarcaList(@Size,@StartLine)";
            cmd.Parameters.AddWithValue("@Size", 14);
            cmd.Parameters.AddWithValue("@StartLine", startLine);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Marca marca = new Marca();
                marca.Nome = reader["Nome"].ToString();
                marca.Ganho = Double.Parse(reader["Ganho"].ToString());
                marca.Venda = Double.Parse(reader["Venda"].ToString());
                marca.Compra = Double.Parse(reader["Compra"].ToString());
                pagination.Data.Add(marca);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        private Pagination<Marca> InitPagination(int page, int totalItems)
        {
            int totalPage;
            if (totalItems % 14 == 0)
            {
                totalPage = totalItems / 14;
            }
            else
            {
                totalPage = totalItems / 14 + 1;
            }

            if (page < 1)
                page = 1;
            else if (page > totalPage)
                page = totalPage;

            Pagination<Marca> pagination = new Pagination<Marca>
            {
                TotalPage = totalPage > 0 ? totalPage : 1,
                HasPrevious = page != 1 && totalPage != 0,
                HasNext = page != totalPage && totalPage != 0,
                ShowFirst = page != 1 && totalPage != 0,
                ShowLast = page != totalPage && totalPage != 0,
                CurrentPage = page
            };
            return pagination;
        }
        public List<Marca> GetMarcaList()
        {
            if (!verifySGBDConnection())
            {
                return null;
            }
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getAllMarcaList()";
            SqlDataReader reader = cmd.ExecuteReader();
            List<Marca> list = new List<Marca>();
            while (reader.Read())
            {
                Marca marca = new Marca();
                marca.Nome = reader["Nome"].ToString();
                marca.Compra = (double)reader["Compra"];
                marca.Ganho = (double)reader["Ganho"];
                marca.Venda = (double)reader["Venda"];
                list.Add(marca);
            }
            ConnectionUtils.Release();
            return list;
        }

        internal Pagination<Marca> GetMarcaOrderByCompra(int page,int asc)
        {
            if (totalMarca == 0)
            {
                GetAllMarca();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Marca> pagination = InitPagination(page, totalMarca);
            int startLine = 14 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getMarcaOrderByCompra(@Size,@StartLine,@Asc)";
            cmd.Parameters.AddWithValue("@Size", 14);
            cmd.Parameters.AddWithValue("@StartLine", startLine);
            cmd.Parameters.AddWithValue("@Asc", asc);
      
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Marca marca = new Marca();
                marca.Nome = reader["Nome"].ToString();
                marca.Ganho = Double.Parse(reader["Ganho"].ToString());
                marca.Venda = Double.Parse(reader["Venda"].ToString());
                marca.Compra = Double.Parse(reader["Compra"].ToString());
                pagination.Data.Add(marca);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        internal Pagination<Marca> GetMarcaOrderByVenda(int page, int asc)
        {
            if (totalMarca == 0)
            {
                GetAllMarca();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Marca> pagination = InitPagination(page, totalMarca);
            int startLine = 14 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getMarcaOrderByVenda(@Size,@StartLine,@Asc)";
            cmd.Parameters.AddWithValue("@Size", 14);
            cmd.Parameters.AddWithValue("@StartLine", startLine);
            cmd.Parameters.AddWithValue("@Asc", asc);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Marca marca = new Marca();
                marca.Nome = reader["Nome"].ToString();
                marca.Ganho = Double.Parse(reader["Ganho"].ToString());
                marca.Venda = Double.Parse(reader["Venda"].ToString());
                marca.Compra = Double.Parse(reader["Compra"].ToString());
                pagination.Data.Add(marca);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }

        internal Pagination<Marca> GetMarcaOrderByGanho(int page, int asc)
        {
            if (totalMarca == 0)
            {
                GetAllMarca();
            }

            if (!verifySGBDConnection())
            {
                return null;
            }

            Pagination<Marca> pagination = InitPagination(page, totalMarca);
            int startLine = 14 * (pagination.CurrentPage - 1);
            startLine = startLine < 0 ? 0 : startLine;
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getMarcaOrderByGanho(@Size,@StartLine,@Asc)";
            cmd.Parameters.AddWithValue("@Size", 14);
            cmd.Parameters.AddWithValue("@StartLine", startLine);
            cmd.Parameters.AddWithValue("@Asc", asc);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Marca marca = new Marca();
                marca.Nome = reader["Nome"].ToString();
                marca.Ganho = Double.Parse(reader["Ganho"].ToString());
                marca.Venda = Double.Parse(reader["Venda"].ToString());
                marca.Compra = Double.Parse(reader["Compra"].ToString());
                pagination.Data.Add(marca);
            }
            reader.Close();
            ConnectionUtils.Release();
            return pagination;
        }
    }
    class TipoService : Service
    {
        public List<Tipo> GetTipoList()
        {
            if (!verifySGBDConnection())
            {
                return null;
            }
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandText = "SELECT * FROM SuperMercado.getAllTipoList()";
            SqlDataReader reader = cmd.ExecuteReader();
            List<Tipo> list = new List<Tipo>();
            while (reader.Read())
            {
                Tipo tipo = new Tipo();
                tipo.Nome = reader["Nome"].ToString();
                list.Add(tipo);
            }
            ConnectionUtils.Release();
            return list;
        }
    }

    public class ForneceService : Service {
    
        public int ForneceProduto(Produto produto,string fornecedor)
        {

            if (!verifySGBDConnection())
            {
                return -1;
            }
            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.fornecerProduto";
            cmd.Parameters.AddWithValue("@ProdutoID",produto.Codigo);
            cmd.Parameters.AddWithValue("@Fornecedor", fornecedor);
            cmd.Parameters.AddWithValue("@PrecoCompra", produto.Preco);
            cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
            cmd.Parameters.AddWithValue("@Marca", produto.Marca);
            int result = cmd.ExecuteNonQuery();
            ConnectionUtils.Release();
            return result;
        }
    }

    public class PagamentoService : Service
    {
        public int Pagar(int EncomendaID)
        {
            if (!verifySGBDConnection())
            {
                return -1;
            }

            SqlCommand cmd = transaction.GetCmd();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuperMercado.payment";
            cmd.Parameters.AddWithValue("@Encomenda", EncomendaID);
            int result = cmd.ExecuteNonQuery();
            ConnectionUtils.Release();
            return result;
        }
    }
}
