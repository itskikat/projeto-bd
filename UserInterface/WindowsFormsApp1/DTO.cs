using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
   public class Pessoa
    {
		protected int ID;
		protected string nome;
		protected string email;
		protected int idade;
		protected string genero;
		protected double salario=-1;
		protected string telefone;

		public int PersonID
		{
			get { return ID; }
			set { ID = value; }
		}

		public String PersonNome
		{
			get { return nome; }
			set { nome = value; }
		}

		public String PersonEmail
		{
			get { return email; }
			set { email = value; }
		}

		public int PersonIdade
		{
			get { return idade; }
			set { idade = value; }
		}

		public String PersonGenero
		{
			get { return genero; }
			set { genero = value; }
		}

		public double PersonSalario
		{
			get { return salario; }
			set { salario = value; }
		}
		public String PersonTelefone
		{
			get { return telefone; }
			set { telefone = value; }
		}
		public override string ToString()
		{
			return "Name: "+nome;
		}
	}

	public class Utilizador : Pessoa
	{
		private String userName;
		private String senha;

		public Utilizador(int id,String Nome,String Senha)
		{
			this.PersonID = id;
			this.userName = Nome;
			this.senha = Senha;

		}

		public Utilizador()
		{

		}

		public string UserName { get => userName; set => userName = value; }
		public string Senha { get => senha; set => senha = value; }
	}
	public class Condutor:Pessoa
	{
		private bool disponivel = true;
		private String cartaConducao;

		public Condutor(int id, String carta)
		{
			this.ID = id;
			this.cartaConducao = carta;
		}

		public int CondutorID
		{
			get { return ID; }
		}

		public bool CondutorDisponivel
		{
			get { return disponivel; }
			set { disponivel = value; }
		}

		public String CondutorCarta{
			get { return cartaConducao; }
		}

	}

	public class Funcionario:Pessoa
	{
		public Funcionario(int id)
		{
			this.ID = id;
		}
	}

	public class Cliente : Pessoa
	{
		private String NIF;
		private String endereco;

		public Cliente(int id,String nif, String endereco)
		{
			this.ID = id;
			this.NIF = nif;
			this.endereco = endereco;
		}

		public String ClientNIF
		{
			get { return NIF; }
			set { NIF = value; }
		}

		public String ClienteEndereco
		{
			get { return endereco; }
			set { endereco = value; }
		}

	}
	public class Tipo
	{
		private string nome;

		public string Nome { get => nome; set => nome = value; }
	}
	public class Produto
	{
		private int codigo;
		private IVA iva;
		private string nome;
		private double preco;
		private bool disponivel;
		private int armazem;
		private string marca;
		private string tipo;
		private int quantidade;
	
		public Produto(int codigo)
		{
			this.codigo = codigo;
		}

		public int Codigo { get => codigo; }
		public IVA Iva { get => iva; set => iva = value; }
		public string Nome { get => nome; set => nome = value; }
		public double Preco { get => preco; set => preco = value; }
		public bool Disponivel { get => disponivel; set => disponivel = value; }
		public int Armazem { get => armazem; set => armazem = value; }
		public string Marca { get => marca; set => marca = value; }
		public int Quantidade { get => quantidade; set => quantidade = value; }
		public string Tipo { get => tipo; set => tipo = value; }
	}

	public class IVA
	{
		private int codigo;
		private double imposto;

		public IVA(int codigo,double imposto)
		{
			this.codigo = codigo;
			this.imposto = imposto;
		}

		public IVA(double imposto)
		{
			this.imposto = imposto;
		}

		public int IvaCodigo
		{
			get { return codigo; }
		}

		public double IvaImposto
		{
			get { return imposto; }
		}

		public override string ToString()
		{
			return imposto+"%";
		}
	}

	public class Armazem
	{
		private int ID;
		private readonly String endereco;

		public override string ToString()
		{
			return "Armazem: " + ID + ", endereco: " + endereco ;
		}
	}
	
	public class Armazena
	{
		private int armazem;
		private int produto;
		private int quantidade;

		public Armazena(int armazem, int produto, int quantidade)
		{
			this.armazem = armazem;
			this.produto = produto;
			this.quantidade = quantidade;
		}

		public Armazena(int produto, int quantidade)
		{
			this.produto = produto;
			this.quantidade = quantidade;
		}

		public int ArmazenaArmazem
		{
			get { return armazem; }
		}
		public int ArmazenaProduto
		{
			get { return produto; }
		}
		public int ArmazenaQuantidade
		{
			get { return quantidade; }
		}
	}
	
	public class Marca
	{
		private String nome;
		private double compra;
		private double venda;
		private double ganho;

		public string Nome { get => nome; set => nome = value; }
		public double Compra { get => compra; set => compra = value; }
		public double Venda { get => venda; set => venda = value; }
		public double Ganho { get => ganho; set => ganho = value; }
	}

	// AQUI
	public class Fornecedor
	{
		private String fax;
		private String email;
		private String nome;
		private String NIF;
		private String tipo;

		public Fornecedor(String nome, String NIF)
		{
			this.nome = nome;
			this.NIF = NIF;
		}

		public String FornecedorFax
		{
			get { return fax; }
			set { fax = value; }
		}

		public String FornecedorEmail
		{
			get { return email; }
			set { email = value; }
		}

		public String FornecedorNome
		{
			get { return nome; }
			set { nome = value; }
		}

		public String FornecedorNIF
		{
			get { return NIF; }
			set { NIF = value; }
		}

		// AQUI
		public String FornecedorTipo
		{
			get { return tipo; }
			set { tipo = value; }
		}
	} 

	public class Fornece
	{
		private int produto;
		private String fornecedor;
		private double precoCompra;
		private int quantidade;

		public Fornece(String fornecedor,int produto,double preco,int quantidade)
		{
			this.fornecedor = fornecedor;
			this.produto = produto;
			this.precoCompra = preco;
			this.quantidade = quantidade;
		}

		public int FornecePorduto
		{
			get { return produto; }
		}
		public String Fornecefornecedor
		{
			get { return fornecedor; }
		}
		public double FornecePrecoCompra
		{
			get { return precoCompra; }
		}
		public int ForneceQuantidade
		{
			get { return quantidade; }
		}
	}

	public class Transporte
	{
		private int id;
		private string matricula;
		private string marca;
		private bool disponivel=true;
		private int capaciadade;

		public Transporte(int id,string matricula,string marca)
		{
			this.id = id;
			this.Matricula = matricula;
			this.Marca = marca;
		}

		public int ID { get => id; set => id = value; }
		public string Matricula { get => matricula; set => matricula = value; }
		public string Marca { get => marca; set => marca = value; }
		public bool Disponivel { get => disponivel; set => disponivel = value; }
		public int Capaciadade { get => capaciadade; set => capaciadade = value; }
	}

	public class Encomenda
	{
		private int ID;
		private int client;
		private int funcionario;
		private String condicoesPagamento;
		private String destino;
		private double custoTotal;
		private bool estado=false;
		private DateTime date;
		private bool pago = false;

		public Encomenda() { }
		public Encomenda(int id,int client,int funcionario,String condicoesPagamento,
							String destino)
		{
			this.ID = id;
			this.client = client;
			this.funcionario = funcionario;
			this.condicoesPagamento = condicoesPagamento;
			this.destino = destino;
		}
		public Encomenda(int client, int funcionario, string condicoesPagamento,
							string destino)
		{
			this.client = client;
			this.funcionario = funcionario;
			this.condicoesPagamento = condicoesPagamento;
			this.destino = destino;
		}

		public double EncomendaCustoTotal
		{
			get { return custoTotal; }
			set { custoTotal = value; }
		}

		public int EncomendaID{ get => ID; set => ID = value; }
		public int EncomendaClient { get => client; set => client = value; }
		public int EncomendaFuncionario { get => funcionario; }
		public string EncomendaCondicoesPagamento { get => condicoesPagamento; }
		public string EncomendaDestino { get => destino; set => destino = value; }
		public bool EncomendaEstado { get => estado; set => estado = value; }
		public DateTime EncomendaDate { get => date; set => date = value; }
		public bool Pago { get => pago; set => pago = value; }
	}

	public class Fatura
	{
		private int Id;
		private int encomenda;
		private int cliente;
		private int funcionario;
		private String condicoesPagamento;
		private double custoTotal;
		private DateTime date;

		public Fatura(int id,int encomenda,int cliente,int funcionario, DateTime date)
		{
			this.Id = id;
			this.encomenda = encomenda;
			this.cliente = cliente;
			this.funcionario = funcionario;
			this.date = date;
		}

		public Fatura(int encomenda, int cliente, int funcionario, DateTime date, String condicoesPagamento, double custoTotal)
		{
			this.encomenda = encomenda;
			this.cliente = cliente;
			this.funcionario = funcionario;
			this.date = date;
			this.condicoesPagamento = condicoesPagamento;
			this.custoTotal = custoTotal;
		}

		public int ID { get => Id; }
		public int Encomenda { get => encomenda; }
		public int Cliente { get => cliente;  }
		public int Funcionario { get => funcionario; }
		public string CondicoesPagamento { get => condicoesPagamento; set => condicoesPagamento = value; }
		public double CustoTotal { get => custoTotal; set => custoTotal = value; }
		public DateTime Date { get => date;  }
	}

	public class Pagamento
	{
		private int ID;
		private int cliente;
		private int fatura;
		private String condicoesPagamento;
		private double custoTotal;
		private DateTime date;

		public Pagamento(int id,int cliente,int fatura,String codicoes)
		{
			this.ID = id;
			this.cliente = cliente;
			this.fatura = fatura;
		}

		public int ID1 { get => ID;  }
		public int Cliente { get => cliente; }
		public int Fatura { get => fatura;  }
		public string CondicoesPagamento { get => condicoesPagamento; set => condicoesPagamento = value; }
		public double CustoTotal { get => custoTotal; set => custoTotal = value; }
		public DateTime Date { get => date; set => date = value; }
	}

	public class Contem
	{
		private int quantidade;
		private int encomenda;
		private int produto;
		private double preco;

		public Contem() { }
		public Contem(int quantidade,int encomenda, int produto,double preco)
		{
			this.quantidade = quantidade;
			this.encomenda = encomenda;
			this.produto = produto;
			this.preco = preco;
		}
		public int Quantidade { get => quantidade; set => quantidade = value; }
		public int Encomenda { get => encomenda; set => encomenda = value; }
		public int Produto { get => produto; set => produto = value; }
		public double Preco { get => preco; set => preco = value; }
	}

	public class Pagination<T>
	{
		private List<T> data;
		private bool hasPrevious;
		private bool hasNext;
		private bool showFirst;
		private bool showLast;
		private int currentPage;
		private int totalPage;
		private bool search = false;

		public Pagination()
		{
			data = new List<T>();
		}
		public List<T> Data { get => data; set => data = value; }
		public bool HasPrevious { get => hasPrevious; set => hasPrevious = value; }
		public bool HasNext { get => hasNext; set => hasNext = value; }
		public bool ShowFirst { get => showFirst; set => showFirst = value; }
		public bool ShowLast { get => showLast; set => showLast = value; }
		public int CurrentPage { get => currentPage; set => currentPage = value; }
		public int TotalPage { get => totalPage; set => totalPage = value; }
		public bool Search { get => search; set => search = value; }
	}
}
