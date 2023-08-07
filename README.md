## Projeto "Desafio_Online_Applications". 

Este projeto foi desenvolvido como critério de avalição para a vaga de desenvolvedor .NET na empresa Online Applications. O programa consiste em receber um arquivo de texto "CNBA" (que é constituído por caracteres que representam dados de movimentações financeiras de várias lojas) e posteriormente, será devidamente tratado para que estes dados sejam exibidos em uma página web.

O projeto foi realizado através do ecossistema .NET, utilizando o ambiente  de desenvolvimento integrado Visual Studio. O padrão da arquitetura de software é o MVC (Model-View-Controller), onde a aplicação é separada em três componentes principais (lógica de negócios, apresentação e controle de fluxo). O framework é o ASP.NET, que fornece a API para a criação de aplicativos web. Para o banco de dados, foi utilizado o SQL Server.

**Em relação a estrutura da solução, seguem algumas informações relevantes:**

* A solução possui dois projetos: Desafio_Online_Applications.API e Desafio_Online_Applications.Core. O primeiro é composto pelos Controllers e pelas Views. Já o segundo é composto pelas Models.

* Na pasta Controllers, as classes são usadas para chamar interfaces, o que é uma prática recomendada, pois aumenta a modularidade do código.

* A classe InjecaoDependencia.cs é onde está o método de mesmo nome, que adiciona o DBContext do SQLServer e a injeção de dependência das classes de implementação e repositórios.

* Na pasta Migrations, gerada automaticamente depois de utilizar o comando 'Add-Migration' no Console do Gerenciador de Pacotes, é composta pelas classes que possuem as características das duas tabelas utilizadas (Operacoes e Erros). A tabela Operacoes só irá gravar os dados se todos eles estiverem corretos. Já a tabela Erros, recebe os dados que possuem algum parâmetro errado. Ambas as tabelas são utilizadas para a exibição das listas na página web.

* A pasta Servicos, é composta por por classes e interfaces. As classes são responsáveis pela separação de responsabilidades e na abstração das funcionalidades do sistema, ou seja, encapsulam um conjunto de operações relacionadas ao objetivo do programa, como as: ListarErros, ProcessarCnbaAsync, IsNullOrEmpty, InserirOperacoesAsync e AbterCPFValidade.

* A pasta Views, compõe todo o código html que será exibido na página web. Cada pasta com sua devida página: Cnab, Erros, Home e Operacoes.

* Nos itens appsetings.json e appsetings.Development.json são onde contêm as "ConnectionStrings", que recebem alguns parâmetros definíveis, como o Server, Database, User e Password.

* A pasta Entidades tem como objetivo agrupar classes que representam as entidades do domínio do aplicativo, sendo as duas tabelas usadas no banco de dados: Erro e Operacoes.

* A pasta Models é composta por classes que declaram os métodos da regra de negócio, sendo eles: ErrosViewModel que tratam das operações que tiveram algum erro na leitura, OperacoesViewModel que tratam das operações bem sucedidas e a SomaLojas que é responsável por somar os valores para uma loja determinada.