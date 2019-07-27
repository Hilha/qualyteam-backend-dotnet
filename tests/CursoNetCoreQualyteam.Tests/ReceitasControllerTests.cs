using System;
using Xunit;
using FluentAssertions;
using CursoNetCoreQualyteam.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using CursoNetCoreQualyteam.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace CursoNetCoreQualyteam.Controllers.Tests
{
    public class ReceitasControllerTests
    {

        private ReceitasContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<ReceitasContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ReceitasContext(options);
        }

        [Fact]
        public void GetAll_TodasReceitasCadastradas()
        {
            var escondidinho = new Receita("Escondidinho","De batata com carne moída","carne, batata, margarina, leite",
            "merge de tudo no liquidificador",
            "https://guiadacozinha.com.br/wp-content/uploads/2017/01/escondidinho-arroz-carne-moida-pure.jpg"){
                Id = 3
            };

            var batataFrita = new Receita("Batata Frita","batata frita vegana","batata, oleo, bacon",
             "Jogue tudo no oleo","https://rechlanches.com.br/wp-content/uploads/2017/06/porcao-de-batatas-fritas-grande-com-bancon-rechlanches-joinville.jpg")
            {
                Id = 4
            };
            //insere as receitas

            var context = CreateTestContext();
            context.AddRange(escondidinho, batataFrita);
            context.SaveChanges();

            var controller = new ReceitasController(context);
            var receitas = controller.GetAll();

            var viewModel1 = new ReceitaViewModel(){
                Id = 3,
                Title = "Escondidinho",
                Description = "De batata com carne moída",
                Ingredients = "carne, batata, margarina, leite",
                Preparation = "merge de tudo no liquidificador",
                ImageUrl = "https://guiadacozinha.com.br/wp-content/uploads/2017/01/escondidinho-arroz-carne-moida-pure.jpg"
            };
        
            var viewModel2 = new ReceitaViewModel(){
                Id = 4,
                Title = "Batata Frita",
                Description = "batata frita vegana",
                Ingredients = "batata, oleo, bacon",
                Preparation =  "Jogue tudo no oleo",
                ImageUrl = "https://rechlanches.com.br/wp-content/uploads/2017/06/porcao-de-batatas-fritas-grande-com-bancon-rechlanches-joinville.jpg"
            };

            receitas.Value.Should().BeEquivalentTo(
                new List<ReceitaViewModel>(){
                    viewModel1, viewModel2
                }
            );
        }

    [Fact]
    public void Get_UmaReceita()
    {   
            var escondidinho = new Receita("Escondidinho","De batata com carne moída","carne, batata, margarina, leite",
            "merge de tudo no liquidificador",
            "https://guiadacozinha.com.br/wp-content/uploads/2017/01/escondidinho-arroz-carne-moida-pure.jpg")
            {
                Id = 1
            };

            var batataFrita = new Receita("Batata Frita","batata frita vegana","batata, oleo, bacon",
             "Jogue tudo no oleo","https://rechlanches.com.br/wp-content/uploads/2017/06/porcao-de-batatas-fritas-grande-com-bancon-rechlanches-joinville.jpg"){
                 Id = 2
             };
            //insere as receitas

            var context = CreateTestContext();
            context.AddRange(escondidinho, batataFrita);
            context.SaveChanges();

            var controller = new ReceitasController(context);
            var receitas = controller.Get(1);

            var viewModel1 = new ReceitaViewModel(){
                Id = 1,
                Title = "Escondidinho",
                Description = "De batata com carne moída",
                Ingredients = "carne, batata, margarina, leite",
                Preparation = "merge de tudo no liquidificador",
                ImageUrl = "https://guiadacozinha.com.br/wp-content/uploads/2017/01/escondidinho-arroz-carne-moida-pure.jpg"
            };

            receitas.Value.Should().BeEquivalentTo(viewModel1);
    }   


      [Fact]
      public void Insert_DeveInserir(){
          // Arrange
          var receitaViewModel = new ReceitaViewModel(7,"Arroz","Arroz","Arroz","Cozinha ele","2wCEAAkGBxMTEhUSExMWFhUVFhUVGBUYFxUWFRUVFxUXFxUVFRYYHSggGBolHRUVITEhJSkrLi4uFx8zODMtNygtLisBCgoKDg0OGxAQGy0lHyUtLS0tLS0tLS0tLS0tLS0tLSstLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf");
          var context = CreateTestContext();
          var controller = new ReceitasController(context);

          // Act
          var result = controller.Insert(receitaViewModel);
          var receitaDoBanco = context.Receitas.FirstOrDefaultAsync(receita => receita.Title == receitaViewModel.Title);
          
          //result.Value.Should().BeEquivalentTo(receitaViewModel);
          
          //Assert
          receitaDoBanco.Should().NotBeNull("Por que deve ter algo no banco");
          receitaDoBanco.Result.Should().BeEquivalentTo(receitaViewModel, c => c.Excluding(r => r.Id));
          
      }

       [Fact]
      public void Insert_DeveLancarException_QuandoExcederLimiteDeCaracteres(){
          // Arrange
          var receitaViewModel = new ReceitaViewModel(7,"Arrozzzzzzzzzzzzz","Arroz","Arroz","Cozinha ele","2wCEAAkGBxMTEhUSExMWFhUVFhUVGBUYFxUWFRUVFxUXFxUVFRYYHSggGBolHRUVITEhJSkrLi4uFx8zODMtNygtLisBCgoKDg0OGxAQGy0lHyUtLS0tLS0tLS0tLS0tLS0tLSstLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf");
          var context = CreateTestContext();
          var controller = new ReceitasController(context);

          // Act
          Action acao = () => controller.Insert(receitaViewModel);
          acao.Should().Throw<Exception>().WithMessage("PODE MAIOR QUE 10 NÃO MEU PARÇA");
        
      }

    [Fact]
    public void Update_DeveEditarReceita(){
        // Arrange
        var receita = new Receita()
        {
            Id = 10,
            Title = "velho",
            Description = "Receita baita",
            ImageUrl = "saas",
            Ingredients = "Varias paradas",
            Preparation = "Something"
        };
        var context = CreateTestContext();

        context.Receitas.Add(receita);
        context.SaveChanges();

        var receitaViewModel = new ReceitaViewModel()
        {
            Title = "novo",
            Description = "Nova descricao"
        };
        var controller = new ReceitasController(context);
        
        // Act
        var result = controller.Update(10, receitaViewModel);

    // Assert

        var receitaNoBanco = context.Receitas.FirstOrDefault(r => r.Id == receita.Id);

        receitaNoBanco.Title.Should().Be(receitaViewModel.Title);
        receitaNoBanco.Description.Should().Be(receitaViewModel.Description);

        // Atualizar alguma coisa 
        // Ter salvo no banco - done
        // Front passa o id, e os campos que serão atualizados
        // Fazer asserção se o  campo foi atualizado

        
    
    }
    }
}
