
# PessoaCad - Gerenciamento de Cadastro de Pessoas

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-v8.0-blue)
![C#](https://img.shields.io/badge/C%23-Language-brightgreen)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-v6.0-blue)
![Blazor](https://img.shields.io/badge/Blazor-Framework-purple)
![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-purple)
![License](https://img.shields.io/badge/License-MIT-blue)

## Descrição do Projeto
O **PessoaCad** é uma solução completa para gerenciar o cadastro de pessoas, composta por uma aplicação multi-plataforma com .NET MAUI, uma API em ASP.NET Core e uma interface web utilizando Blazor. A arquitetura do projeto segue o padrão MVVM e MVC para garantir modularidade, escalabilidade e facilidade de manutenção.

## Tecnologias Utilizadas
- ![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-v8.0-blue)
- ![C#](https://img.shields.io/badge/C%23-Programação-brightgreen)
- ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-v6.0-blue)
- ![Blazor](https://img.shields.io/badge/Blazor-Framework-purple)
- ![Newtonsoft.Json](https://img.shields.io/badge/Newtonsoft.Json-13.0.1-orange)

## Estrutura de Diretórios

```plaintext
PessoaCad/
├── apppessoa/
│   ├── Models/
│   │   ├── Endereco.cs
│   │   ├── Pessoa.cs
│   │   ├── Telefone.cs
│   ├── ViewModels/
│   ├── Views/
│   │   ├── MainPage.xaml
│   │   └── DetalhesPage.xaml
│   ├── Resources/
│   │   ├── Fonts/
│   │   └── Images/
│   ├── App.xaml
│   └── MainPage.xaml.cs
├── apipessoa/
│   ├── Controllers/
│   │   ├── PessoaController.cs
│   ├── Models/
│   │   ├── Pessoa.cs
│   │   ├── Endereco.cs
│   │   └── Telefone.cs
│   ├── Program.cs
│   ├── Startup.cs
│   └── appsettings.json
├── webpessoa/
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   └── PessoaController.cs
│   ├── Models/
│   │   ├── Endereco.cs
│   │   ├── ErrorViewModel.cs
│   │   ├── Pessoa.cs
│   │   ├── Telefone.cs
│   │   └── TelefoneTipo.cs
│   ├── Program.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Services/
│   │   └── apiPessoaService.cs
│   ├── Views/
│   │   ├── Home/
│   │   │   ├── Index.cshtml
│   │   │   └── Privacy.cshtml
│   │   ├── Pessoa/
│   │   │   ├── Create.cshtml
│   │   │   ├── Delete.cshtml
│   │   │   ├── Details.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   └── Index.cshtml
│   │   ├── Shared/
│   │   │   ├── Error.cshtml
│   │   │   ├── _Layout.cshtml
│   │   │   ├── _Layout.cshtml.css
│   │   │   └── _ValidationScriptsPartial.cshtml
│   │   ├── _ViewImports.cshtml
│   │   └── _ViewStart.cshtml
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── wwwroot/
│   │   ├── css/
│   │   │   └── site.css
│   │   ├── favicon.ico
│   │   ├── js/
│   │   │   └── site.js
│   │   └── lib/
│   │       ├── bootstrap/
│   │       │   ├── LICENSE
│   │       │   └── dist/
│   │       │       ├── css/
│   │       │       │   ├── bootstrap.min.css
│   │       │       │   └── bootstrap.min.css.map
│   │       │       └── js/
│   │       │           ├── bootstrap.bundle.min.js
│   │       │           └── bootstrap.bundle.min.js.map
│   │       ├── jquery/
│   │       │   ├── LICENSE.txt
│   │       │   └── dist/
│   │       │       ├── jquery.min.js
│   │       │       └── jquery.min.map
│   │       ├── jquery-validation/
│   │       │   ├── LICENSE.md
│   │       │   └── dist/
│   │       │       ├── jquery.validate.min.js
│   │       │       └── additional-methods.min.js
│   │       └── jquery-validation-unobtrusive/
│   │           ├── LICENSE.txt
│   │           └── dist/
│   │               ├── jquery.validate.unobtrusive.min.js
│   │               └── jquery.validate.unobtrusive.js
├── PessoaCad.sln
└── README.md
```

## Funcionamento dos Componentes

### App Móvel (`apppessoa`)
- **Modelos (`Models/`)**: Estrutura de dados do aplicativo.
- **ViewModels (`ViewModels/`)**: Lógica de negócios e comunicação com a interface.
- **Views (`Views/`)**: Interfaces de usuário para interação com o usuário final.
- **Resources (`Resources/`)**: Arquivos de suporte, como imagens e fontes.

### API REST (`apipessoa`)
- **Controllers (`Controllers/`)**: Controladores responsáveis pelas rotas de API, como `PessoaController` para gerenciar solicitações HTTP relacionadas a pessoas.
- **Programação em ASP.NET Core**: Uso do padrão MVC e dependências configuradas em `Startup.cs` e `Program.cs`.
- **Serialização de JSON**: Implementada com `Newtonsoft.Json`.

### Interface Web (`webpessoa`)
- **Páginas (`Pages/`)**: Páginas Blazor para exibir, editar e detalhar informações de pessoas.
- **Componentes (`Components/`)**: Elementos reutilizáveis na interface.
- **Arquivos Estáticos (`wwwroot/`)**: CSS e JavaScript para melhorar a experiência do usuário.
- **Framework Blazor**: Para criar aplicações interativas com C# no frontend.

## Tutorial de Uso no Visual Studio 2022

### 1. Pré-requisitos
- Visual Studio 2022 com os workloads:
  - Desenvolvimento de Aplicativos Móveis e Desktop com .NET
  - ASP.NET e Desenvolvimento Web
  - Blazor Development

### 2. Clonando o Repositório
```bash
git clone https://github.com/seu-usuario/PessoaCad.git
cd PessoaCad
```

### 3. Abrindo o Projeto
1. Abra o **Visual Studio 2022**.
2. Selecione **Arquivo > Abrir > Projeto/Solução**.
3. Navegue até a pasta clonada e selecione `PessoaCad.sln`.

### 4. Configuração de Build
- Verifique as configurações de build para os projetos `apppessoa`, `apipessoa` e `webpessoa`.
- Atualize as dependências NuGet conforme necessário.

### 5. Executando a Aplicação
- **App Móvel**: Execute a partir de um dispositivo ou emulador.
- **API REST**: Inicie a `apipessoa` e verifique as rotas disponíveis.
- **Interface Web**: Execute o projeto `webpessoa` e acesse a interface pelo navegador.

## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

## Licença
Distribuído sob a licença MIT. Consulte `LICENSE` para mais informações.

---

Feito com ❤️ por Rodney Victor, utilizando .NET MAUI, ASP.NET Core e Blazor.
