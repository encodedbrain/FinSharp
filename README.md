# FinSharp - API Web para Controle de Finanças


**status**:
  em desenvolvimento 🚧



O FinSharp é um projeto de API Web desenvolvido em C# utilizando a plataforma .NET. Essa API é voltada para o controle de finanças pessoais, investimentos e utilização de cartão de crédito. Ela permite que os usuários gerenciem suas transações financeiras, acompanhem seus investimentos e monitorem o uso de seus cartões de crédito.

## Funcionalidades

O projeto FinSharp possui as seguintes funcionalidades principais:

1. **API RESTful**: A aplicação expõe uma API RESTful que permite a comunicação com a aplicação cliente. Através desta API, é possível realizar operações relacionadas a finanças, investimentos e cartões de crédito, como criação de transações, gerenciamento de contas e categorias, acompanhamento de investimentos, entre outros.

2. **Banco de Dados SQL Server**: O projeto utiliza o banco de dados SQL Server para armazenar as informações dos usuários, transações financeiras, investimentos e detalhes do cartão de crédito. O SQL Server oferece recursos robustos e escaláveis para o armazenamento seguro dos dados.

3. **Entity Framework**: O projeto faz uso do Entity Framework, um framework de mapeamento objeto-relacional (ORM), para facilitar a interação com o banco de dados SQL Server. O Entity Framework simplifica a manipulação de dados, permitindo a criação de consultas, atualizações e exclusões de forma eficiente.

4. **Autenticação e Autorização**: A API implementa um sistema de autenticação e autorização para proteger os recursos e as informações dos usuários. Os usuários podem se registrar, fazer login e gerenciar suas credenciais de acesso. Além disso, é possível definir diferentes níveis de acesso aos recursos, garantindo a segurança dos dados financeiros.

5. **Controle de Finanças**: O FinSharp permite que os usuários registrem suas transações financeiras, categorizem os gastos, acompanhem seus saldos, gerem relatórios e estabeleçam metas financeiras.

6. **Gerenciamento de Investimentos**: Os usuários podem registrar seus investimentos, acompanhar o desempenho de suas carteiras, visualizar gráficos e relatórios, além de receber informações relevantes sobre o mercado financeiro.

7. **Utilização de Cartão de Crédito**: O FinSharp oferece recursos para que os usuários cadastrem seus cartões de crédito, monitorem seus gastos, definam limites de crédito, visualizem faturas e recebam alertas sobre pagamentos.

## Configuração e Execução

Siga as instruções abaixo para configurar e executar o projeto FinSharp em seu ambiente local:

1. **Pré-requisitos**: Certifique-se de ter instalado em sua máquina o seguinte:

   - [.NET SDK](https://dotnet.microsoft.com/download) (versão 7.0.304 ou superior)
   - [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (instalação local ou em um servidor)

2. **Clonar o repositório**: Clone este repositório em seu ambiente local utilizando o seguinte comando:

   ```shell
   git clone https://github.com/encodedbrain/finsharp.git
   ```

3. **Configurar o

 banco de dados**: Utilize o SQL Server Management Studio ou outra ferramenta de administração do SQL Server para criar um banco de dados vazio chamado "FinSharp".

4. **Configurar a conexão com o banco de dados**: Crie o arquivo  `appsettings.Development.json` na raiz do projeto e atualize as informações de conexão com o banco de dados, como o nome do servidor, nome do banco de dados, usuário e senha.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=nome-do-servidor;Database=FinSharpDB;User Id=usuario;Password=senha;",
       "Secret":"sua hash" 
     },
     ...
   }
   ```


Para configurar a connection string do banco de dados com o Entity Framework usando o método `OnConfiguring` do seu DbContext, siga as etapas abaixo:

1. Abra o arquivo que contém a classe do seu DbContext.

2. Dentro da classe do seu DbContext, localize o método `OnConfiguring`.

3. No método `OnConfiguring`, adicione o seguinte código para configurar a connection string:

   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       if (!optionsBuilder.IsConfigured)
       {
           string connectionString = "Server=nome-do-servidor;Database=nome-do-banco;User Id=usuario;Password=senha;";
           optionsBuilder.UseSqlServer(connectionString);
       }
   }
   ```

   Certifique-se de substituir `"nome-do-servidor"`, `"nome-do-banco"`, `"usuario"` e `"senha"` pelos valores corretos da sua configuração do SQL Server.

   Observe que o trecho `if (!optionsBuilder.IsConfigured)` é usado para garantir que a configuração só seja aplicada se o DbContext ainda não estiver configurado.


Agora, o Entity Framework utilizará a connection string configurada no método `OnConfiguring` para estabelecer a conexão com o banco de dados ao executar as operações de leitura e gravação.

Certifique-se de proteger suas informações confidenciais, como senhas e credenciais de acesso ao banco de dados, utilizando práticas adequadas de segurança.


5. **Executar as migrações do Entity Framework**: No terminal, navegue até a pasta raiz do projeto e execute o seguinte comando:

   ```shell
   dotnet ef database update
   ```

   Esse comando aplicará as migrações pendentes e criará as tabelas necessárias no banco de dados.

6. **Executar a aplicação**: No terminal, execute o seguinte comando para iniciar a aplicação:

   ```shell
   dotnet run
   ```

   A aplicação será iniciada e estará pronta para receber requisições na URL `http://localhost:5000`.

## Contribuição

Se você deseja contribuir com este projeto, siga as etapas abaixo:

1. Faça um fork deste repositório e clone-o em seu ambiente local.
2. Crie uma branch para sua nova funcionalidade ou correção: `git checkout -b nome-da-branch`.
3. Faça as alterações desejadas e adicione-as ao commit: `git add .`.
4. Faça o commit das suas alterações: `git commit -m "Descrição das alterações"`.
5. Faça o push para o repositório remoto: `git push origin nome-da-branch`.
6. Abra uma pull request neste repositório, descrevendo as alterações propostas.

## Licença

Este projeto está licenciado sob a [MIT License](https://opensource.org/licenses/MIT).
