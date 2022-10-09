function check_cadastroUsuario() {
    var submit = document.getElementById("submit_usuario_cadastro");
    var nome = document.getElementById("nome_login");
    var usuario = document.getElementById("usuario_login");
    var senha = document.getElementById("senha_login");

    if (nome.value == 0 && usuario.value == 0 && senha.value == 0) {
        submit.disabled = true;
    }
    else {
        submit.disabled = false;
    }
}
