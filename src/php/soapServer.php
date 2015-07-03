<?php
// Após executar o servidor, linkar a url: http://localhost/WSClientes?wsdl


// Classe cliente contendo todos os campos do cliente
class Cliente
{
  public $Nome;
  public $CPF;
}

// Classe servidora para os serviços SOAP.
class MySoapServer
{
  // Método de pesquisa
  public function Pesquisar($nome, $cpf)
  {
    // Inclusão de dois clientes fixos, neste ponto inclua sua lógica para utilizar os parâmetros de entrada e filtrar este resultado.
    $cliente1 = new Cliente();
    $cliente1->Nome = "teste de nome";
    $cliente1->CPF = "12345678901";

    $cliente2 = new Cliente();
    $cliente2->Nome = "Maria 2";
    $cliente2->CPF = "98765432109";
    
    $items = array();
    $items[0] = $client1;
    $items[1] = $client2;
    return $items;
  }
  
  // Método de gravação (Criados dois somente para permitir teste de operação OK e Falha)
  public function SalvarComSucesso($nome, $cpf)
  {
    // Inclua aqui sua lógica de gravação de registro, se concluir corretamente, retorne True, caso contrário False. 
    return true;
  }
  public function SalvarComFalha($nome, $cpf)
  {
    return false;
  }
}

$options= array('uri'=>'http://localhost/WSClientes');
$server=new SoapServer(NULL,$options);
$server->setClass('MySoapServer');
$server->handle();
