using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CursoNetCoreQualyteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ReceitaViewModel>> GetAll()
        {
            return  new ReceitaViewModel[]{
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
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class ReceitaViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description  { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string ImageUrl { get; set; }
    }
}
