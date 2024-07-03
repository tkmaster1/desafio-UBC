using UBC.Core.Service.DTO;
using UBC.Core.Service.DTO.Filters;
using UBC.Core.Service.DTO.Students;

namespace UBC.Core.Service.Facades.Interfaces
{
    public interface IStudentFacade : IDisposable
    {
        /// <summary>
        /// ObterPorCodigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<StudentDTO> ObterPorCodigo(int codigo);

        Task<IList<StudentDTO>> ListarTodos();

        /// <summary>
        /// ListarPorFiltros
        /// </summary>
        /// <param name="filterDTO"></param>
        /// <returns></returns>
        Task<PaginationDTO<StudentDTO>> ListarPorFiltros(StudentFilterDTO filterDTO);

        /// <summary>
        /// CriarMenuSistema
        /// </summary>
        /// <param name="studentRequestDTO"></param>
        /// <returns></returns>
        Task<int> CriarEstudante(StudentRequestDTO studentRequestDTO);

        /// <summary>
        /// AtualizarEstudante
        /// </summary>
        /// <param name="studentRequestDTO"></param>
        /// <returns></returns>
        Task<bool> AtualizarEstudante(StudentRequestDTO studentRequestDTO);

        /// <summary>
        /// DeletarEstudante
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<bool> DeletarEstudante(int codigo);
    }
}
