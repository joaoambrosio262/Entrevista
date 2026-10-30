## Configuração e Restauração de Pacotes

Quando baixei o projeto e tentei inicializar, tive problemas com a pasta `bin/roslyn` e o executável `csc.exe` não estavam sendo gerados corretamente, mesmo após a execução do `nuget restore`.

Para garantir a restauração das dependências e a compilação correta da solução, execute os seguintes comandos no **PowerShell**, a partir da pasta raiz do projeto:

**1. Restaurar os pacotes NuGet via MSBuild:**

```powershell
msbuild .\FI.WebAtividadeEntrevista.sln /t:Restore
```

**2. Recompilar a solução em Debug:**

```powershell
msbuild .\FI.WebAtividadeEntrevista.sln /t:Rebuild /p:Configuration=Debug
```

---

## Implementação dos Requisitos do Teste Prático

### CPF do Cliente

- Adicionada a coluna `CPF` na tabela `CLIENTES`.
- Atualizadas as Stored Procedures de inclusão, alteração, consulta, pesquisa e verificação.
- Implementada validação do formato do CPF (`999.999.999-99`).
- Implementado cálculo oficial dos dígitos verificadores.
- Garantida a unicidade do CPF na base de dados.

### Beneficiários

- Adicionado botão para gerenciamento de beneficiários, com modal para inclusão, alteração e exclusão em grid.
- Implementada validação do CPF dos beneficiários.
- Implementada restrição de duplicidade de CPF para o mesmo cliente.
- Centralizada a persistência dos beneficiários na tabela `BENEFICIARIOS`, sendo realizada exclusivamente pelo botão **Salvar** do cadastro do cliente.

### Melhorias Técnicas

### Melhorias Técnicas

- Criadas as interfaces `IBoCliente` | `IBoBeneficiario` | `ICepValidate`.
- Desacoplado o Controller por meio de injeção de dependência via construtor.
- Implementada validação e consulta automática de CEP por meio da API **ViaCEP**, com preenchimento dos dados de endereço a partir do CEP informado.
- Adicionado campo específico para preenchimento do número do endereço.
- Padronizado o encoding dos arquivos para UTF-8 com BOM.
- Configurada a seção `<globalization>` no `Web.config`.
