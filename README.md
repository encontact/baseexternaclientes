# Template Base Externa de clientes EnContact #

Este projeto tem como objetivo servir de apoio e exemplos para integrações de base externa de clientes aos usuários do EnContact.

## Para que foi criado este repositório? ##

* Facilitar a criação de integrações da base de clientes do sistema EnContact para bases de cliente pré-existentes.
* Servir como template inicial ou exemplo.
* Dividir conhecimento com nossos parceiros e clientes.

## Como faço para utilizar o código exemplo? ##

* Crie um Fork do código
* Configure a integração com a sua base de clientes
* Efetue seus testes

## Criando sua integração passo a passo ##

### Contextualizando ###

* O que devemos fazer é criar dois serviços no padrão SOAP para disponibilizar as funções de **Pesquisa** e **Edição** de dados, que serão acessados pelo sistema EnContact.

### Sobre o serviço de pesquisa ###

* O serviço de pesquisa será acessado pelo EnContact todas as vezes em que o sistema precisar identificar um cliente.
* Quando o serviço for chamado, o EnContact enviará como parâmetro ao serviço os campos de cliente que estiverem configurados para pesquisa, todos em formato textual.
    * O nome dos campos enviados irá respeitar a mesma nomenclatura utilizada no **Nome** do cadastro dos campos de cliente do sistema.
* O serviço deverá retornar uma lista de objeto contendo atributos referentes aos campos de cliente encontrados.
    * Os atributos do objeto devem ser equivalentes ao nome do campo do cliente que serão relacionados, desta forma o EnContact fará a ligação das duas informações.

### Sobre o serviço de gravação ###

* O serviço de gravação será acessado pelo EnContact todas as vezes em que for criado ou editado um cliente.
* Quando o serviço for chamado, o EnContact enviará como parâmetro ao serviço os campos de cliente que estiverem ativos no cadastro de campos de cliente.
    * O nome dos campos enviados irá respeirar a mesma nomenclatura utilizada no **Nome** do cadastro dos campos de cliente do sistema.
* O serviço deverá retornar um booleano, que indica se a integração ocorreu com sucesso ou falha, e isso indicará ao EnContact se ele deverá efetivar ou não o registro ou modificações efetuadas.

### Exemplificando a estrutura de comunicação ###

Imaginando que temos a seguinte configuração de Base de cliente cadastrada no EnContact:
![ConfigBaseCliente.PNG](https://bitbucket.org/repo/gMenMG/images/1294183134-ConfigBaseCliente.PNG)

#### Criando o serviço de pesquisa ####

1. Para a estrutura de base de clientes apresentada acima deveremos desenvolver um webservice receba com a seguinte estrutura:
    1. **ENTRADA**
        1. Ele receberá como parâmetro dois campos:
            * **Nome**, tipo string
            * **Cpf**, tipo string
        1. Os campos respeitam a nomenclatura da coluna **Nome** configurada na base de clientes.
        1. Os campos acima são definidos como parâmetros por serem marcados como campos do tipo **Retorno** na configuração da base de clientes.
    1. **SAÍDA**
        1. Ele deverá retornar uma lista de objetos do tipo "Cliente" com atributos na seguinte estrutura:
            * **Nome**, tipo string
            * **Cpf**, tipo string
        1. Os campos respeitam a nomenclatura da coluna **Nome** configurada na base de clientes.
        1. Não é obrigatória a lista ser marcada como tipo Cliente, mas sim os campos de informação estarem de acordo com a configuração da base de cliente.

#### Criando o serviço de gravação ####

1. Para a estrutura de base de clientes apresentada acima deveremos desenvolver um webservice SOAP receba com a seguinte estrutura:
    1. **ENTRADA**
        1. Ele receberá como parâmetro dois campos:
            * **Nome**, tipo string
            * **Cpf**, tipo string
        1. Os campos respeitam a nomenclatura da coluna **Nome** configurada na base de clientes.
        1. Os campos acima são definidos como parâmetros por serem campos da base de clientes marcados como **Ativo**.
        1. Recebendo os parâmetros, o serviço deve cuidar de toda a lógica necessária para identificação, atualização/criação do registro de cliente.
    1. **SAÍDA**.
        1. Ele deverá retornar um booleano indicando:
            a. **true** se todos os passos necessários para a atualização foram efetuados com sucesso.
            b. **false** se houve qualquer problema ou impedimento que impeça a atualização do registro.
        1. O sistema EnContact só concluirá a operação de modificação ou criação de cliente se receber do serviço um resultado de valor **true**.

* **NOTA:** A estrutura de gravação se não for utilizada pelo cliente pode ser criada apenas com um retorno **true**.

#### Configurando os serviços no sistema ####

Configuração da integração:
![ConfigIntegracaoClienteExterno.PNG](https://bitbucket.org/repo/gMenMG/images/4227205238-ConfigIntegracaoClienteExterno.PNG)

1. Para configurar a integração, acesse a tela de configuração e efetue os seguintes passos:
    1. **Situação**, se estiver como *ativo*, o sistema irá efetuar as chamadas ao serviço configurado, caso contrário não haverá integração.
    1. **Usuário**, opcional, usuário de acesso em caso de necessidade de autenticação na chamada do serviço. Se não houver, deixar em branco.
    1. **Senha**, opcional, senha de acesso em caso de necessidade de autenticação na chamada do serviço. Se não houver, deixar em branco.
    1. **Url do webservice**, deve informar a URL que retornará o XML SOAP de disponibilidade dos serviços.
        1. Após configurar a URL do serviço, é possível clicar no botão "Buscar" para que sejam carregados os métodos disponíveis para integração.
        1. **Método de consulta**, deve ser escolhido qual método do webservice irá integrar as pesquisas de cliente utilizando o serviço.
        1. **Método de adição/edição**, opcional, deve ser escolhido qual método webservice irá integrar a gravação de novos clientes ou edição de clientes existentes. Se não for escolhido um método, o sistema não permitirá cadastro e alteração da base de clientes pelo EnContact.
    1. **Parâmetros adicionais a serem enviados ao webservice**, opcional, caso haja necessidade do serviço receber algum parâmetro adicional, como por exemplo um token de acesso, o mesmo pode ser configurado nesta etapa, informando como parâmetro, o texto do campo a ser enviado ao webservice e como valor o texto fixo que deve ser enviado.

## E se eu precisar de ajuda? ##

* Entre em contato conosco em atendimento<Arroba>encontact.com.br