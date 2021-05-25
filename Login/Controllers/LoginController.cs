using Login.Business;
using Login.Domain.Models.Request;
using Login.Domain.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers
{
    [ApiController]
    [Route("[Controller]")]  // conexão com servidor

    public class LoginController : Controller //Controller ou ControllerBase referencia uma biblioteca interna da Microsoft (padrão) 
    {
        private readonly LoginBL _loginBL;

        public LoginController(LoginBL loginBL)
        {
            _loginBL = loginBL;
        }

        /// <summary>
        /// Inserir os dados para popular a tab
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")] //Create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)] //em caso de erro retornará a mensagem de erro
        public IActionResult PostLogin([FromBody] LoginRequest loginRequest)
        {
            var idUsers = _loginBL.InsertDados(loginRequest);

            return Ok(new Response { Message = "Usuário cadastrado com sucesso." });
        }

        /// <summary>
        /// Fazer atualização dos dados de cadastro (nome, email ou senha) por Id
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")] //Update
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody] LoginUpdateRequest loginRequest)
        {
            var updateResponse = _loginBL.UpdateDados(loginRequest);

            if (updateResponse == 1)
            {
                return Ok( new Response { Message = "Usuário atualizado com sucesso."});
            }
            else
            {
                return BadRequest(new { message = "Erro ao atualizar. Contate o administrador"});
            }
        }

        /// <summary>
        /// Buscar dados de cadastro por ID do usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getLoginById")]//Read
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult GetLoginById(int id)
        {
            var loginResponse = _loginBL.GetById(id);
            if(loginResponse == null)
            {
                return BadRequest(new { message = "Nenhum cadastro encontrado a partir desses dados" });
            }
            return Ok(loginResponse);
        }

        /// <summary>
        /// Buscar todos os dados de cadastro existentes na tab
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]

        public IActionResult GetAll()
        {
            var loginResponse = _loginBL.GetAllDados();
            if (loginResponse.Any())
            {
                return Ok(loginResponse);
            }
            else
            {
                return NotFound(new { message = "Erro. Contate o administrador" });
            }
        }

        /// <summary>
        /// Deletar todos os dados de um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteDados")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteDados(int id)
        {
            var responseLogin = _loginBL.Delete(id);
            if(responseLogin != 0)
            {
                return Ok(new Response { Message = "Dados do usuário deletados." });
            }
            else
            {
                return BadRequest(new { message = "Não foi possível deletar dados do usuário. Conatte o administrador." });
            }
        }
    }


}
