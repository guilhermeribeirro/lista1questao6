using aplicativo_wb_co.Models.response;
using Microsoft.AspNetCore.Mvc;

namespace aplicativo_wb_co.Controllers
{



    [ApiController]
    [Route("api/[controller]")] //[controller]
   // é uma variavel ele substitui pelo  nome do controller 
   //exemplo: rota / Teste
    public class TesteController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()

        //assinatura do metodo voce tem IActionResult
        //IActionResult --> Status do protcolo http


        {
            return Ok ("Minha primeira request");

            //Ok() é a função de status 200 do protocolo http

            //NotFound();
            //IActionResult do 404
        }



        [HttpPost]
        public IActionResult Post(AlunosViewModel alunosViewModel)
        {

            if (alunosViewModel.Idade < 0) {

                return BadRequest("Idade invalida!!");            
            }


            return Ok("Aluno criado com sucesso!!");
        }


    }
}
