// NOTA: instale o módulo soap-server do node.js antes de iniciar, com o comando: "npm install soap-server"
// Mais informações sobre o plugin: https://www.npmjs.com/package/soap-server
// Após executar o servidor no node.js, linkar a url: http://127.0.0.1:1337/WSClientes?wsdl

var soap = require('soap-server');

function MyTestService(){ }

// Método de pesquisa
MyTestService.prototype.Pesquisar = function(Nome, CPF){
    // Inclusão de dois clientes fixos, neste ponto inclua sua lógica para utilizar os parâmetros de entrada e filtrar este resultado.
    var cliente1 = new Object();
    cliente1.Nome = "teste de nome";
    cliente1.CPF = "12345678901";
    var cliente2 = new Object();
    cliente2.Nome = "Maria 2";
    cliente2.CPF = "98765432109";
    
    var listaClientes = new Array();
    listaClientes.push(cliente1);
    listaClientes.push(cliente2);
    
    return listaClientes;
};

// Método de gravação (Criados dois somente para permitir teste de operação OK e Falha)
MyTestService.prototype.SalvarComSucesso = function(Nome, CPF){
    // Inclua aqui sua lógica de gravação de registro, se concluir corretamente, retorne True, caso contrário False. 
    return true;
};
MyTestService.prototype.SalvarComFalha = function(Nome, CPF){
    return false;
};


var soapServer = new soap.SoapServer();
var soapService = soapServer.addService('WSClientes', new MyTestService());
soapServer.listen(1337, '127.0.0.1');
console.log("Serviço iniciado em http://127.0.0.1:1337/WSClientes?wsdl");
