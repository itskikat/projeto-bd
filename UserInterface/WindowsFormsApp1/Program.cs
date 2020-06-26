using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseManager;
using DTOs;
using Factorys;
using Services;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //criar a conta de admin
            CreateAdmin();
            Login login = new Login();
            login.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(login);
        }

        private static void CreateAdmin()
        {
            string Nome = ConfigurationManager.AppSettings["Nome"];
            string Email = ConfigurationManager.AppSettings["Email"];
            string Telefone = ConfigurationManager.AppSettings["Telefone"];
            string Idade = ConfigurationManager.AppSettings["Idade"];
            string Salario = ConfigurationManager.AppSettings["Salario"];
            string Genero = ConfigurationManager.AppSettings["Genero"];
            string Username = ConfigurationManager.AppSettings["Username"];
            string Pwd = ConfigurationManager.AppSettings["Pwd"];
            UtilizadorService utilizadorService = (UtilizadorService)ServiceFactory.GetInstance("UtilizadorService");
            Utilizador utilizador = utilizadorService.CheckIsExiste(Email, Pwd);
            if (utilizador.PersonID == 0 && utilizador.UserName.Equals("unkown"))
            {
                Pessoa pessoa = new Pessoa
                {
                    PersonNome = Nome,
                    PersonEmail = Email,
                    PersonTelefone = Telefone,
                    PersonGenero = Genero,
                    PersonIdade = int.Parse(Idade),
                    PersonSalario = int.Parse(Salario)
                };

                utilizadorService.CreateAdmin(pessoa, Username, Pwd);
            }
        }
    }
}
