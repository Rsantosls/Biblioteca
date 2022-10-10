
function check_cadastroEmprestimo() {
    var submit = document.getElementById("submit_emprestimo_cadastro");
    var nome = document.getElementById("nome_cadastro");
    var tel = document.getElementById("tel_cadastro");

    if (nome.value == 0 && tel.value.length < 10) {
        submit.disabled = true;
    }
    else {
        submit.disabled = false;
    }
}
