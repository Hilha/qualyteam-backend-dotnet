namespace CursoNetCoreQualyteam.Dominio
{
    public class Receita
    {

        const int LimiteDeCaracter = 10;
        public Receita(string title, string description, string ingredients, string preparation, string imageUrl)
        {
            if(!CaracteresDoTitulo(title)){
                throw new System.Exception("PODE MAIOR QUE 10 NÃO MEU PARÇA");
            }
            Title = title;
            Description = description;
            Ingredients = ingredients;
            Preparation = preparation;
            ImageUrl = imageUrl;
        }

         public Receita()
        {
            
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description  { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string ImageUrl { get; set; }

        public bool CaracteresDoTitulo(string titulo){
            return titulo.Length >= 0 && titulo.Length <= LimiteDeCaracter;
        }
    }
}