using aplicativo_wb_co.Models.Request;
using aplicativo_wb_co.Models.response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace aplicativo_wb_co.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //[controller]
    public class AlunoController : ControllerBase
    {




        private readonly string _alunoCaminhoArquivo;

        public AlunoController()
        {
            _alunoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "aluno.json");
        }

        #region Métodos arquivo
        private List<AlunoViewModel> LerAlunos()
        {
            if (!System.IO.File.Exists(_alunoCaminhoArquivo))
            {
                return new List<AlunoViewModel>();
            }

            string json = System.IO.File.ReadAllText(_alunoCaminhoArquivo);
            if (string.IsNullOrEmpty(json))
            {


                return new List<AlunoViewModel>();
            }




            return JsonConvert.DeserializeObject<List<AlunoViewModel>>(json);
        }

        private int ObterProximoCodigoDisponivel()
        {
            List<AlunoViewModel> alunos = LerAlunos();
            if (alunos.Any())
            {
                return alunos.Max(p => p.Codigo) + 1;
            }
            else
            {
                return 1;
            }
        }

        private void EscreverAlunos(List<AlunoViewModel> alunos)
        {
            string json = JsonConvert.SerializeObject(alunos);
            System.IO.File.WriteAllText(_alunoCaminhoArquivo, json);
        }
        #endregion



        [HttpGet("{codigo}")]

        public IActionResult Get(int codigo)
        {
            List <AlunoViewModel> listaTodosAlunos

                = LerAlunos();
            var alunoProcurado = listaTodosAlunos.Where(p => p.Codigo == codigo);

            if (!alunoProcurado.Any()) return NotFound();



            return Ok(alunoProcurado.First());
        }


        [HttpGet]

        public IActionResult Get()
        {
            List<AlunoViewModel> listaTodosAlunos
                = LerAlunos();

            return Ok(listaTodosAlunos);
        }


        [HttpPost]


        public IActionResult Post([FromBody] NovoAlunoViewModel novoAlunoViewModel)
        {
            if (novoAlunoViewModel == null)
            {
                return BadRequest();
            }

            List<AlunoViewModel> alunos = LerAlunos();
            int proximoCodigo = ObterProximoCodigoDisponivel();

            AlunoViewModel novoAluno = new AlunoViewModel()
            {
                Codigo = proximoCodigo,
                Nome = novoAlunoViewModel.Nome,
                RA = novoAlunoViewModel.RA,
                Email = novoAlunoViewModel.Email,
                CPF = novoAlunoViewModel.CPF,
                Ativo = novoAlunoViewModel.Ativo
            };

            alunos.Add(novoAluno);
            EscreverAlunos(alunos);

            return CreatedAtAction(nameof(Get), new { codigo = novoAluno.Codigo }, novoAluno);
        }


        [HttpPut("{codigo}")]

        public IActionResult Put(int codigo, 
            [FromBody] EditAlunoViewModel editarAlunoViewModel)

        {
            return Ok();



        }

        [HttpDelete("{codigo}")]

        public IActionResult Delete(int codigo)
        {



            return Ok();
        }



        

    }
}
