using UBC.Core.Service.DTO;
using UBC.Core.Service.DTO.Filters;
using UBC.Core.Service.DTO.Students;

namespace UBC.Core.Service.Facades.Interfaces
{
    public interface IStudentsFacade : IDisposable
    {
        /// <summary>
        /// ObterPorCodigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<StudentsDTO> ObterPorCodigo(int codigo);

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns></returns>
        Task<IList<StudentsDTO>> ListarTodos();

        /// <summary>
        /// ListarPorFiltros
        /// </summary>
        /// <param name="filterDTO"></param>
        /// <returns></returns>
        Task<PaginationDTO<StudentsDTO>> ListarPorFiltros(StudentsFilterDTO filterDTO);

        /// <summary>
        /// CriarMenuSistema
        /// </summary>
        /// <param name="studentRequestDTO"></param>
        /// <returns></returns>
        Task<int> CriarEstudante(StudentsRequestDTO studentRequestDTO);

        /// <summary>
        /// AtualizarEstudante
        /// </summary>
        /// <param name="studentRequestDTO"></param>
        /// <returns></returns>
        Task<bool> AtualizarEstudante(StudentsRequestDTO studentRequestDTO);

        /// <summary>
        /// DeletarEstudante
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Task<bool> DeletarEstudante(int codigo);
    }
}