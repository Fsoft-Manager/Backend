namespace Backend.Repository.BaseRepository
{

    public interface IBaseRepository<T> where T : class
    {

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// GetById 
        /// </summary>
        /// <returns></returns>
        Task<T> GetByIdAsync(int Id);

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void DeleteAsync(T entity);

        /// <summary>
        /// Save
        /// </summary>
        void Save();
    }

}
