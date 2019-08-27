using DapperExtensions;
using System.Collections.Generic;

namespace EPAS.Component
{
    public interface IDbOperation<TEntity> where TEntity : class
    {
        TEntity GetEntity(object id);

        object Insert(TEntity entity);

        void Inserts(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        bool Deletes(object predicate);

        IEnumerable<TEntity> GetList(object predicate, IList<ISort> sort = null);

        IEnumerable<TEntity> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage);


    }
}
