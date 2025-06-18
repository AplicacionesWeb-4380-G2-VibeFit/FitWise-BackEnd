namespace UPC.FitWisePlatform.API.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity>
{
    /// <summary>
    ///     Add a new entity to the repository.
    /// </summary>
    /// <param name="entity">Entity object to add</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    ///     Find entity by id.
    /// </summary>
    /// <param name="id">The Entity ID to Find</param>
    /// <returns>Entity object if found</returns>
    Task<TEntity?> FindByIdAsync(int id);
    
    /// <summary>
    ///     Get all entities from the repository.
    /// </summary>
    /// <returns>An Enumerable containing all entity objects</returns>
    Task<IEnumerable<TEntity>> ListAsync();
    
    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    void Update(TEntity entity);
    
    /// <summary>
    ///     Remove entity
    /// </summary>
    /// <param name="entity">The entity object to remove</param>
    void Remove(TEntity entity);
}