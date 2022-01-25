using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SogetiAssessment.Contexts;

namespace SogetiAssessment.DataServices
{
    public abstract class DataService<T> where T : class
    {
        protected IContextWrapper<T> _contextWrapper;
        public DataService(IContextWrapper<T> contextWrapper) {
            _contextWrapper = contextWrapper;
        }

        public virtual async Task<ActionResult<T>> Get(int id)
        {
            return await _contextWrapper.PrimarySet.FindAsync(id);
        }

        public virtual async Task<T> Create(T obj)
        {
            _contextWrapper.PrimarySet.Add(obj);
            await _contextWrapper.Context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<T> Update(T obj)
        {
            _contextWrapper.Context.Entry(obj).State = EntityState.Modified;

            try
            {
                await _contextWrapper.Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_contextWrapper.PrimarySet.Where(o => o == obj).FirstOrDefault<T>() == null)
                {
                    return null;
                }
                else
                {
                    throw ex;
                }
            }

            return obj;
        }

        public virtual async Task<T> Delete(int id)
        {
            var obj = await _contextWrapper.PrimarySet.FindAsync(id);

            if (obj == null)
            {
                return obj;
            }

            _contextWrapper.PrimarySet.Remove(obj);
            await _contextWrapper.Context.SaveChangesAsync();

            return obj;
        }
    }
}
