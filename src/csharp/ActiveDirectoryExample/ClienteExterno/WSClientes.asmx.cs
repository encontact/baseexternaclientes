using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Web.Services;

namespace ClienteExterno
{
    /// <summary>
    /// Classe que representa a estrutura do cliente para processar.
    /// </summary>
    public class Cliente
    {
        public string Nome, DomainName;
    }

    /// <summary>
    /// Exemplo de serviço para integração com Base Externa de clientes.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WSClientes : WebService
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
        /// <param name="Email">Campo configurado como pesquisa no exemplo.</param>
        /// <returns>Lista de objeto de clientes para retorno, deve retornar todos os campos de cliente a serem atualizados no EnContact.</returns>
        [WebMethod]
        public List<Cliente> Pesquisar(string Nome, string DomainName)
        {
            var clientes = new List<Cliente>();
            try
            {
                //Chamada para conexão com o servidor LDAP/Active Directory
                DirectoryEntry enTry = new DirectoryEntry("LDAP://192.168.0.1", "SeuUsuario", "SuaSenha");
                DirectorySearcher mySearcher = new DirectorySearcher(enTry);
                // Cria um filtro no Active Directory para todos os campos filtrados pela condição E, e exigindo categoria Person e classe User.
                // Mais sobre como filtrar em: https://msdn.microsoft.com/en-us/library/windows/desktop/aa746475(v=vs.85).aspx
                mySearcher.Filter = "(&(objectCategory=person)(objectClass=user)(&(name=" + Nome + "*)(userPrincipalName=" + DomainName + "*)))";

                try
                {
                    foreach (SearchResult resEnt in mySearcher.FindAll())
                    {
                        // NOTA: O código abaixo apenas serve pra listar todas as entradas encontradas
                        //       no AD acessado, utilize esta listagem para saber quais campos você 
                        //       deseja recuperar e 
                        var properties = new List<KeyValuePair<string, string>>();
                        foreach (PropertyValueCollection property in resEnt.GetDirectoryEntry().Properties)
                        {
                            foreach (object o in property)
                            {
                                string value = o.ToString();
                                properties.Add(new KeyValuePair<string, string>(property.PropertyName, value));
                            }
                        }

                        // Recupera informações de interesse e adiciona no objeto para retorno.
                        var cn = resEnt.GetDirectoryEntry().Properties["name"].Value;
                        var pn = resEnt.GetDirectoryEntry().Properties["userPrincipalName"].Value;
                        clientes.Add(new Cliente
                        {
                            Nome = cn.ToString(),
                            DomainName = pn.ToString()
                        });
                    }
                }
                catch (Exception f)
                {
                    Console.WriteLine(f.Message);
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
            }

            return clientes;
        }

        /// <summary>
        /// Apenas um método de salvar é necessário, ele deve retornar um booleano para indicar se a gravação 
        /// ocorreu com sucesso ou não e garantir que o EnContact mantenha a cópia corretamente.
        /// </summary>
        /// <param name="Nome">Campo a ser gravado como exemplo.</param>
        /// <param name="CPF">Campo a ser gravado como exemplo.</param>
        /// <returns>True se salvou com sucesso e False se falhou</returns>
        [WebMethod]
        public bool SalvarComSucesso(string Nome, string CPF)
        {
            throw new NotImplementedException();
        }
        [WebMethod]
        public bool SalvarComFalha(string Nome, string CPF)
        {
            throw new NotImplementedException();
        }
    }
}
