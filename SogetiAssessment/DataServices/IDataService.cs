using Microsoft.AspNetCore.Mvc;


namespace SogetiAssessment.DataServices
{
    public interface IDataService<T>
    {
        public Task<ActionResult<T>> Get(int id);

        public Task<T> Create(T obj);

        public Task<T> Update(T obj);

        public Task<T> Delete(int id);

        public Task<ActionResult<List<T>>> GetByCustomer(int id);
    }
}
