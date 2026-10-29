Implementação dos requisitos do teste prático para inclusão do CPF do cliente e manutenção de seus beneficiários:

CPF do Cliente:
Adicionada coluna CPF na tabela CLIENTES e atualizadas as stored procedures (Inc, Alt, Cons, Pesq, Verifica).
Validação de formato (999.999.999-99), cálculo oficial do dígito verificador e garantia de não duplicidade na base.
Campo reposicionado na interface conforme o layout do documento.

Beneficiários:
Adicionado botão e modal para gestão de beneficiários (inclusão, alteração e exclusão em grid).
Validação de CPF de beneficiário e restrição de duplicidade para o mesmo cliente.
Gravação unificada na tabela BENEFICIARIOS acionada apenas no botão "Salvar" do cadastro do cliente.

Melhorias Técnicas:
Criação de interfaces (IBoCliente, IBoBeneficiario) e configuração de Injeção de Dependência no MVC via IDependencyResolver.
Limpeza e modernização do acesso a dados (DaoCliente e DaoBeneficiario) com nameof(...) e inicializadores de coleção.
Padronização de encoding UTF-8 com BOM e configuração de <globalization> no Web.config.
