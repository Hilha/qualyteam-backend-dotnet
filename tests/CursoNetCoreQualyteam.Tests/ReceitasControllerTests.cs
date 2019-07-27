using System;
using Xunit;
using FluentAssertions;

namespace CursoNetCoreQualyteam.Controllers.Tests
{
    public class ReceitasControllerTests
    {
        [Fact]
        public void GetAll_TodasReceitasCadastradas()
        {
            //insere as receitas

            var controller = new ReceitasController();
            var receitas = controller.GetAll();
            receitas.Value.Should().BeEquivalentTo(new ReceitaViewModel[]{

                new ReceitaViewModel(){
                    Id = 1,
                    Title = "taaaa",
                    Description = "asdasdasd",
                    Ingredients = "asdasdasd",
                    Preparation = "dsfsdfsdf",
                    ImageUrl = "sdfsdf"
                },
                new ReceitaViewModel(){
                    Id = 2,
                    Title = "asda",
                    Description = "sdfsdf",
                    Ingredients = "dfgd",
                    Preparation = "ghjgh",
                    ImageUrl = "ertretf"
                }
            });
        }
    }
}
