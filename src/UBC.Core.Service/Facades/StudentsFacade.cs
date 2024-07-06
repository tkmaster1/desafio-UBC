using AutoMapper;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Filters;
using UBC.Core.Domain.Interfaces.Services;
using UBC.Core.Service.DTO;
using UBC.Core.Service.DTO.Filters;
using UBC.Core.Service.DTO.Students;
using UBC.Core.Service.Facades.Interfaces;

namespace UBC.Core.Service.Facades
{
    public class StudentsFacade : IStudentsFacade
    {
        #region Properties

        private readonly IMapper _mapper;
        private readonly IStudentsAppService _studentAppService;

        #endregion

        #region Constructor

        public StudentsFacade(IMapper mapper, IStudentsAppService studentAppService)
        {
            _mapper = mapper;
            _studentAppService = studentAppService;
        }

        #endregion

        #region Methods

        public async Task<StudentsDTO> ObterPorCodigo(int codigo)
        {
            var menuSystemDomain = await _studentAppService.GetByCode(codigo);

            return _mapper.Map<StudentsDTO>(menuSystemDomain);
        }

        public async Task<IList<StudentsDTO>> ListarTodos()
        {
            var menuSystemResultDomain = await _studentAppService.ListAll();

            var menuSystemResult = _mapper.Map<IList<StudentsDTO>>(menuSystemResultDomain);

            return menuSystemResult;
        }

        public async Task<PaginationDTO<StudentsDTO>> ListarPorFiltros(StudentsFilterDTO filterDto)
        {
            var filter = _mapper.Map<StudentFilter>(filterDto);

            var result = await _studentAppService.ListByFilters(filter);

            var resultDto = _mapper.Map<PaginationDTO<StudentsDTO>>(result);

            return resultDto;
        }

        public async Task<int> CriarEstudante(StudentsRequestDTO studentRequestDTO)
        {
            var studentDomain = _mapper.Map<StudentEntity>(studentRequestDTO);

            int codigo = await _studentAppService.CreateStudent(studentDomain);

            return codigo;
        }

        public async Task<bool> AtualizarEstudante(StudentsRequestDTO studentRequestDTO)
        {
            var studentDomain = _mapper.Map<StudentEntity>(studentRequestDTO);

            return await _studentAppService.UpdateStudent(studentDomain);
        }

        public async Task<bool> DeletarEstudante(int code) =>
            await _studentAppService.RemoveStudent(code);

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion
    }
}
