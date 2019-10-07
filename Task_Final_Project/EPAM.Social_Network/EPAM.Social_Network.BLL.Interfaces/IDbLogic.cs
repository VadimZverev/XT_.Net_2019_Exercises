using System.Collections.Generic;

namespace EPAM.Social_Network.BLL.Interfaces
{
    public interface IDbLogic<TEntity> where TEntity: class
    {
        int Add(TEntity entity);

        bool Delete(int id);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Update(TEntity entity);
    }
}
