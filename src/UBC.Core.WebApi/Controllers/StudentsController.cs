using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Service.DTO;
using UBC.Core.Service.DTO.Filters;
using UBC.Core.Service.DTO.Students;
using UBC.Core.Service.Facades.Interfaces;
using UBC.Core.WebApi.Models.Responses;

namespace UBC.Core.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StudentsController : MainAPIController
    {
        #region Properties

        private readonly IStudentFacade _studentFacade;
        private readonly IUserLoginAppService _user;

        #endregion

        #region Constructor

        public StudentsController(IStudentFacade studentFacade,
                            INotificador notificador,
                            IUserLoginAppService user) : base(notificador, user)
        {
            _studentFacade = studentFacade;
            _user = user;
        }

        #endregion

        #region Methods

        [HttpGet("obterPorCodigo/{code}")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> ObterPorCodigo(int code)
        => CustomResponse(await _studentFacade.ObterPorCodigo(code));

        [HttpGet("listarTodos")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> ListarTodos()
        {
            var result = await _studentFacade.ListarTodos();

            return CustomResponse(result);
        }

        [HttpPost("buscarEstudantes")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<ActionResult<PaginationDTO<StudentDTO>>> BuscarEstudantes([FromBody] StudentFilterDTO studentFilterDTO)
        {
            var result = await _studentFacade.ListarPorFiltros(studentFilterDTO);

            return CustomResponse(result);
        }

        [HttpPost("incluir")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> SalvarNovoEstudante([FromBody] StudentRequestDTO studentRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _studentFacade.CriarEstudante(studentRequestDTO);

            return CustomResponse(id);
        }

        [HttpPut("alterar")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> AlterarEstudante([FromBody] StudentRequestDTO studentRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return CustomResponse(await _studentFacade.AtualizarEstudante(studentRequestDTO));
        }

        [HttpDelete("excluir/{codigo}")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> ExcluirEstudante(int codigo)
            => CustomResponse(await _studentFacade.DeletarEstudante(codigo));

        #endregion
    }
}
