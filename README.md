# Livros
Ainda em construção. Validação JWT token para requisições. Authenticação e registro de novos usuários através do Identity FrameWork.
 Serviço construido em ASP.NET Core 6 


# Descrição
A api de Livros, foi desenvolvida pensando em um ambiente de gerenciamento de informações sobre livros, onde executará as requisições básicas de incluir novo livro, editar e excluir. Para acesso a esse gerenciamento, é necessário um registro de usuário e login. A api também conta com usuários administradores que terão controle d manipulação dessas informações.
A API de Livros, utiliza requisições HTTP responsáveis pelas operações básicas necessárias para a manipulação dos dados. 
A API foi desenvolvida em ASP.NET Core 6. Foi utilizado também o Identity [Sistema de identidade ASP.NET](https://learn.microsoft.com/pt-br/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity), onde é possível todo o gerenciamento de usuário. o Banco de dados é em [SQL SERVER](https://www.microsoft.com/en-us/sql-server).  

# Instalação
Os requisitos mínimos para instalação do projeto em máquina:

.NET SDK version": "6.0.403"
IIS 10.0 Express

# Utilização
Foi utilizado o Swagger para uma especificação independente de linguagem para descrever a API. Para realizar o teste em Visual Studio, basta adicionar o Livros.WebApi como projeto de inicialização, iniciar o IIS Express, observar se a inicialização está apontada para: (http://localhost/swagger )

A api possui segurança JWT Bearer token. Para obter o token, precisa passar o usuario e senha no endpoint Login.Ao obter sucesso, insira na opção Authorize (o cadeado no canto superior direito) e siga as orientações descritas lá.

# Funcionalidades
  Login

Status de retorno:

- 200 Sucesso

- 401 Não Autorizado

- 500 Requisição Ruim        
Response Padrão: { "code": int , "message": string, "innerError": string }
