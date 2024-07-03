using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UBC.Core.Data.Context;
using UBC.Core.Domain.Interfaces.Repositories;

namespace UBC.Core.Data.Repository
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class, new()
    {
        #region Properties

        protected readonly MeuContexto DbContext;
        protected readonly IdentityContext DbIdentityContext;
        protected readonly DbSet<TEntity> DbSet;

        #endregion

        #region Constructor

        public RepositoryBase(MeuContexto meuContexto)
        {
            DbContext = meuContexto;
            DbSet = DbContext.Set<TEntity>();
        }

        public RepositoryBase(IdentityContext contextIdentity)
        {
            DbIdentityContext = contextIdentity;
            DbSet = DbIdentityContext.Set<TEntity>();
        }

        #endregion

        #region Methods Commons

        /// <summary>
        /// ObterPorCodigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByCodeAsync(int code)
            => await DbSet.FindAsync(code);

        public async Task<TEntity> GetByName(string nome)
            => await DbSet.FindAsync(nome);

        /// <summary>
        /// Existe
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exist(int code) => await GetByCodeAsync(code) != null;

        /// <summary>
        /// Buscar
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
            => await DbSet.AsNoTracking().Where(predicate).ToListAsync();

        /// <summary>
        /// ListarTodos
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> ListAll()
            => await DbSet.ToListAsync();

        /// <summary>
        /// Obter
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> ToObtain() => DbSet;

        /// <summary>
        /// Adicionar
        /// </summary>
        /// <param name="entity"></param>
        public virtual void ToAdd(TEntity entity)
        {
            // entity.DataCadastro = DateTime.Now;
            DbSet.Add(entity);
        }

        /// <summary>
        /// Adicionar
        /// </summary>
        /// <param name="entities"></param>
        public virtual void ToAdd(IEnumerable<TEntity> entities) => DbSet.AddRange(entities.ToArray());

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="entity"></param>
        public virtual void ToUpdate(TEntity entity) => DbSet.Update(entity);

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="entities"></param>
        public virtual void ToUpdate(IEnumerable<TEntity> entities) => DbSet.UpdateRange(entities.ToList());

        public virtual void ToRemove(int codigo)
        {
            var entity = DbSet.Find(codigo);

            if (entity != null)
                DbSet.Remove(entity);
        }

        public virtual void ToRemove(TEntity entity) => DbSet.Remove(entity);

        /// <summary>
        /// Salvar
        /// </summary>
        /// <returns></returns>
        public int ToSave() => DbContext.SaveChanges();

        public async Task<int> ToSaveChangesAsync() => await DbContext.SaveChangesAsync();

        #endregion

        #region Identity

        public virtual void UpdateIdentity(TEntity entity)
        {
            var entry = DbIdentityContext.Entry(entity);

            DbSet.Attach(entity);

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// SalvarIdentity
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveIdentity() => await DbIdentityContext.SaveChangesAsync();

        #endregion

        public void Dispose()
        {
            if (DbContext != null)
                DbContext.Dispose();

            if (DbIdentityContext != null)
                DbIdentityContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
