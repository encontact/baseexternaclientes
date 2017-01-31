using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ClienteExterno
{
    /// <summary>
    /// Classe que representa a estrutura do cliente para processar.
    /// </summary>
    public class Cliente
    {
        public string Nome, CPF;
    }

    /// <summary>
    /// Exemplo de serviço para integração com Base Externa de clientes.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WSClientes : System.Web.Services.WebService
    {
        /// <summary>
        /// Este é um exemplo de método configurado para efetuar a pesquisa de clientes em sua base de dados
        /// atual. 
        /// 
        /// Os parâmetros recebidos são referentes aos campos de cliente criados na base e utilizados para pesquisa, em nosso exemplo a 
        /// base de clientes estaria configurada para permitir pesquisas pelos campos Nome e CPF.
        /// 
        /// Após utilizar os campos para efetuar a pesquisa, retorne uma lista de objetos com a estrutura de campos que mantenham os nomes
        /// configurados na base de clientes do EnContact.
        /// </summary>
        /// <param name="Nome">Campo configurado como pesquisa no Exemplo.</param>
        /// <param name="CPF">Campo configurado como pesquisa no exemplo.</param>
        /// <returns>Lista de objeto de clientes para retorno, deve retornar todos os campos de cliente a serem atualizados no EnContact.</returns>
        [WebMethod]
        public List<Cliente> Pesquisar(string Nome, string CPF)
        {
            return new List<Cliente>{
                new Cliente{
                    Nome = "teste de nome",
                    CPF = "12345678901"
                },
                new Cliente{
                    Nome = "Maria 2",
                    CPF = "98765432109"
                },
            };
        }

        /// <summary>
        /// Apenas um método de salvar é necessário, ele deve retornar um booleano para indicar se a gravação 
        /// ocorreu com sucesso ou não e garantir que o EnContact mantenha a cópia corretamente.
        /// </summary>
        /// <param name="Nome">Campo a ser gravado como exemplo.</param>
        /// <param name="CPF">Campo a ser gravado como exemplo.</param>
        /// <returns></returns>
        [WebMethod]
        public bool SalvarComSucesso(string Nome, string CPF)
        {
            return true;
        }
        [WebMethod]
        public bool SalvarComFalha(string Nome, string CPF)
        {
            return false;
        }
    }
}
