// var check = document.getElementById("titulo_livro");
// check.onblur = test;

// function test()
// {
//     alert("teste");
// }


function check_cadastroLivro(){
    var submit = document.getElementById("submit_livro_cadastro");
    var nome = document.getElementById("titulo_livro");
    var autor = document.getElementById("autor_livro");
    var ano = document.getElementById("ano_livro");

    if(nome.value == 0 | autor.value == 0 | ano.value == 0)
    {
        submit.disabled = true;
    }
    else {
        submit.disabled = false;
    }
}
