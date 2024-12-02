using System;
using System.Linq.Expressions;

namespace Training.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    //The common methods could be GET ALL, GET by ID FIRST OR DEAFOULT, ADD, REMOVE, REMOVERANGE

    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);

    IList<T> GetAll(string? includeProperties=null);
    T GetFirstOrDefault(Expression<Func<T,bool>>?filter = null);

}
