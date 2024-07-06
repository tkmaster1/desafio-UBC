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

        private readonly IStudentsFacade _studentFacade;
        private readonly IUserLoginAppService _userLoginAppService;

        #endregion

        #region Constructor

        public StudentsController(IStudentsFacade studentFacade,
                            INotificador notificador,
                            IUserLoginAppService userLoginAppService) : base(notificador, userLoginAppService)
        {
            _studentFacade = studentFacade;
            _userLoginAppService = userLoginAppService;
        }

        #endregion

        #region Methods

        [HttpGet("getByCode/{code}")]
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

        [HttpGet("listAll")]
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

        [HttpPost("searchStudents")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<ActionResult<PaginationDTO<StudentsDTO>>> BuscarEstudantes([FromBody] StudentsFilterDTO studentFilterDTO)
        {
            var result = await _studentFacade.ListarPorFiltros(studentFilterDTO);

            return CustomResponse(result);
        }

        [HttpPost("include")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> SalvarNovoEstudante([FromBody] StudentsRequestDTO studentRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _studentFacade.CriarEstudante(studentRequestDTO);

            return CustomResponse(id);
        }

        [HttpPut("edit")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> AlterarEstudante([FromBody] StudentsRequestDTO studentRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return CustomResponse(await _studentFacade.AtualizarEstudante(studentRequestDTO));
        }

        [HttpDelete("delete/{codigo}")]
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
