# Sistema de Apuração Eleitoral

Este projeto implementa um sistema de gerenciamento e apuração eleitoral com o objetivo de facilitar o acompanhamento e divulgação dos resultados de forma acessível e transparente. Ele inclui funcionalidades como configuração da eleição, importação de resultados por seções e consulta de resultados consolidados.

## Tecnologias Utilizadas
- **C#** com **ASP.NET Core 9.0**
- **Entity Framework Core** para manipulação de banco de dados
- **SQLite** como banco de dados principal (ou SQL Server, conforme configuração)
- **Swagger** para documentação da API

## Dependências
As seguintes dependências são usadas no projeto:

- `Microsoft.AspNetCore.OpenApi` (9.0.0)
- `Microsoft.Data.Sqlite` (9.0.0)
- `Microsoft.EntityFrameworkCore.Sqlite` (9.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Swashbuckle.AspNetCore` (6.5.0)

## Configuração e Execução do Projeto

### Pré-requisitos
- [.NET SDK 9.0](https://dotnet.microsoft.com/) instalado na máquina
- Banco de dados configurado (por padrão, SQLite está pronto para uso)

### Passos para rodar o projeto

1. **Clone o repositório**:
   ```bash
   git clone <URL_DO_REPOSITORIO>
   cd projeto-urnas-backend


# Instruções de Configuração e Execução do Projeto

## 1. Restaurar as dependências do projeto

Execute o seguinte comando para restaurar as dependências do projeto:

```bash
dotnet restore
```

## 2. Configurar o banco de dados

- **SQLite (padrão):** O banco será configurado automaticamente.
- **SQL Server:** Atualize a string de conexão no arquivo de configuração `appsettings.json`.

## 3. Executar as migrações para criar o banco de dados

Execute o comando abaixo para aplicar as migrações e criar o banco de dados:

```bash
dotnet ef database update
```

## 4. Iniciar a aplicação

Execute o comando abaixo para iniciar a aplicação:

```bash
dotnet run
```

## 5. Acessar a documentação Swagger

Abra o navegador e acesse: `http://localhost:5000/swagger`

# Endpoints da API

## Configurar Eleição
- **URL:** `POST /api/eleicao`
- **Descrição:** Configura a eleição com base nos dados fornecidos.
- **Entrada:** Objeto Eleicao (JSON)
- **Saída:** Confirmação ou erro.

## Importar Resultados de Seções
- **URL:** `POST /api/eleicao/importacoes-secoes`
- **Descrição:** Importa resultados de votação para as seções eleitorais.
- **Entrada:** Objeto ResultadosEleicao (JSON)
- **Saída:** Confirmação ou erro.

## Obter Status das Seções
- **URL:** `GET /api/eleicao/importacoes-secoes`
- **Descrição:** Retorna o status das seções eleitorais (importadas, pendentes, etc.).
- **Parâmetros de Query:**
  - `zonaId` (opcional): Filtra por zona eleitoral.
  - `secaoId` (opcional): Filtra por seção.
- **Saída:** Objeto StatusSecoesViewModel.

## Consultar Resultados Consolidados
- **URL:** `GET /api/eleicao/resultados`
- **Descrição:** Retorna os resultados consolidados da eleição.
- **Parâmetros de Query:**
  - `zonaId` (opcional): Filtra por zona eleitoral.
  - `secaoId` (opcional): Filtra por seção.
- **Saída:** Objeto ResultadosViewModel.

# Estrutura do Projeto

- **Controllers:** Controladores responsáveis pelos endpoints da API.
- **Models:** Classes que representam as entidades principais, como Eleicao, ResultadosEleicao e Candidato.
- **ViewModel:** Classes que formatam as respostas da API, como ResultadosViewModel e StatusSecoesViewModel.
- **Services:** Contém as regras de negócio, como configuração da eleição e consolidação dos resultados.
- **Context:** Contém as informações e configurações do banco de dados.
