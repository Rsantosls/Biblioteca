namespace Biblioteca.Models
{
    public class Login
    {
        public int Id { get; set;}
        public string Nome {get; set;}
        public string Usuario {get; set;}
        public string Senha {get; set;}

        // Essa string é utilizada criação de novas senhas. Ela é temporariamente preenchida com as informações escritas na edição, e nenhuma informação é enviada para a database.
        public string NovaSenha{get; set;}
    }
}