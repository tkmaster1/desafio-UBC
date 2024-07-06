using System.Linq.Expressions;

namespace UBC.Core.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// ObterPorCodigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<TEntity> GetByCodeAsync(int code);

        /// <summary>
        /// ObterPorNome
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<TEntity> GetByName(string name);

        /// <summary>
        /// Existe
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> Exist(int code);

        /// <summary>
        /// Buscar
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// ListarTodos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ListAll();

        /// <summary>
        /// Obter
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> ToObtain();

        /// <summary>
        /// Adicionar um Objeto
        /// </summary>
        /// <param name="entity"></param>
        void ToAdd(TEntity entity);

        /// <summary>
        /// Adicionar uma lista de objeto
        /// </summary>
        /// <param name="entities"></param>
        void ToAdd(IEnumerable<TEntity> entities);

        /// <summary>
        /// Atualizar um objeto
        /// </summary>
        /// <param name="entity"></param>
        void ToUpdate(TEntity entity);

        /// <summary>
        /// Atualizar uma lista de objeto
        /// </summary>
        /// <param name="entities"></param>
        void ToUpdate(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remover por código
        /// </summary>
        /// <param name="code"></param>
        void ToRemove(int code);

        /// <summary>
        /// Remover um objeto
        /// </summary>
        /// <param name="entity"></param>
        void ToRemove(TEntity entity);

        /// <summary>
        /// Salvar
        /// </summary>
        /// <returns></returns>
        int ToSave();

        Task<int> ToSaveChangesAsync();
    }
}