var beneficiarios = [];
var editandoIndex = -1;

$(document).ready(function () {
    $('#CPF, #BenefCPF').on('input keyup', function () {
        $(this).val(mascaraCPF($(this).val()));
    });

    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #CPF').val(mascaraCPF(obj.CPF || ''));
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Telefone').val(obj.Telefone);

        if (obj.Beneficiarios && obj.Beneficiarios.length > 0) {
            beneficiarios = obj.Beneficiarios.map(function (b) {
                return {
                    Id: b.Id,
                    CPF: mascaraCPF(b.CPF || ''),
                    Nome: b.Nome,
                    IdCliente: b.IdCliente
                };
            });
            renderizarBeneficiarios();
        }
    }

    $('#btnSalvarBeneficiario').click(function () {
        var cpf = $('#BenefCPF').val().trim();
        var nome = $('#BenefNome').val().trim();

        if (!cpf) {
            ModalDialog("Atenção", "Informe o CPF do beneficiário.");
            return;
        }

        if (!validarCPF(cpf)) {
            ModalDialog("Atenção", "CPF de beneficiário inválido.");
            return;
        }

        if (!nome) {
            ModalDialog("Atenção", "Informe o Nome do beneficiário.");
            return;
        }

        var cpfLimpo = cpf.replace(/\D/g, "");
        var jaExiste = beneficiarios.some(function (item, idx) {
            return idx !== editandoIndex && item.CPF.replace(/\D/g, "") === cpfLimpo;
        });

        if (jaExiste) {
            ModalDialog("Atenção", "Não é permitido cadastrar mais de um beneficiário com o mesmo CPF para o mesmo cliente.");
            return;
        }

        if (editandoIndex >= 0) {
            beneficiarios[editandoIndex].CPF = cpf;
            beneficiarios[editandoIndex].Nome = nome;
            editandoIndex = -1;
            $('#btnSalvarBeneficiario').text('Incluir');
        } else {
            beneficiarios.push({
                Id: 0,
                CPF: cpf,
                Nome: nome,
                IdCliente: obj ? obj.Id : 0
            });
        }

        $('#BenefCPF').val('');
        $('#BenefNome').val('');
        renderizarBeneficiarios();
    });

    $('#gridBeneficiarios').on('click', '.btnAlterarBeneficiario', function () {
        var index = $(this).data('index');
        var item = beneficiarios[index];
        $('#BenefCPF').val(item.CPF);
        $('#BenefNome').val(item.Nome);
        editandoIndex = index;
        $('#btnSalvarBeneficiario').text('Salvar');
    });

    $('#gridBeneficiarios').on('click', '.btnExcluirBeneficiario', function () {
        var index = $(this).data('index');
        if (editandoIndex === index) {
            $('#BenefCPF').val('');
            $('#BenefNome').val('');
            editandoIndex = -1;
            $('#btnSalvarBeneficiario').text('Incluir');
        } else if (editandoIndex > index) {
            editandoIndex--;
        }
        beneficiarios.splice(index, 1);
        renderizarBeneficiarios();
    });

    $('#modalBeneficiarios').on('hidden.bs.modal', function () {
        $('#BenefCPF').val('');
        $('#BenefNome').val('');
        editandoIndex = -1;
        $('#btnSalvarBeneficiario').text('Incluir');
    });

    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        var cpfCliente = $(this).find("#CPF").val();
        if (!validarCPF(cpfCliente)) {
            ModalDialog("Atenção", "O CPF informado é inválido.");
            return;
        }

        var dados = {
            "Id": obj.Id,
            "NOME": $(this).find("#Nome").val(),
            "CEP": $(this).find("#CEP").val(),
            "Email": $(this).find("#Email").val(),
            "Sobrenome": $(this).find("#Sobrenome").val(),
            "Nacionalidade": $(this).find("#Nacionalidade").val(),
            "Estado": $(this).find("#Estado").val(),
            "Cidade": $(this).find("#Cidade").val(),
            "Logradouro": $(this).find("#Logradouro").val(),
            "Telefone": $(this).find("#Telefone").val(),
            "CPF": cpfCliente,
            "Beneficiarios": beneficiarios
        };

        $.ajax({
            url: urlPost,
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dados),
            error: function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success: function (r) {
                ModalDialog("Sucesso!", r);
                $("#formCadastro")[0].reset();
                setTimeout(function () {
                    window.location.href = urlRetorno;
                }, 1500);
            }
        });
    });
});

function renderizarBeneficiarios() {
    var $tbody = $('#gridBeneficiarios tbody');
    $tbody.empty();

    for (var i = 0; i < beneficiarios.length; i++) {
        var item = beneficiarios[i];
        var tr = '<tr>' +
            '<td>' + item.CPF + '</td>' +
            '<td>' + item.Nome + '</td>' +
            '<td>' +
            '<button type="button" class="btn btn-primary btn-sm btnAlterarBeneficiario" data-index="' + i + '" style="margin-right: 5px;">Alterar</button>' +
            '<button type="button" class="btn btn-primary btn-sm btnExcluirBeneficiario" data-index="' + i + '">Excluir</button>' +
            '</td>' +
            '</tr>';
        $tbody.append(tr);
    }
}

function mascaraCPF(cpf) {
    if (!cpf) return '';
    cpf = cpf.replace(/\D/g, "");
    if (cpf.length > 11) cpf = cpf.substring(0, 11);
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    return cpf;
}

function validarCPF(cpf) {
    if (!cpf) return false;
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) return false;

    var soma = 0;
    var resto;
    for (var i = 1; i <= 9; i++)
        soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.substring(9, 10))) return false;

    soma = 0;
    for (var i = 1; i <= 10; i++)
        soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);
    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.substring(10, 11))) return false;

    return true;
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var modalHtml = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->';

    $('body').append(modalHtml);
    $('#' + random).modal('show');
}
