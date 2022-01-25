using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SogetiAssessment.Models;
using SogetiAssessment.Contexts;

namespace SogetiAssessment.DataServices
{
    public class OrderDataService : DataService<Order>
    {
        public readonly IContextWrapper<Order> _contextWrapper;
        public OrderDataService(IContextWrapper<Order> contextWrapper): base(contextWrapper)
        {
            _contextWrapper = contextWrapper;
        }

        public virtual async Task<List<Order>> GetByCustomer(int id)
        {
            return await _contextWrapper.PrimarySet.Where(order => order.CustomerId == id).ToListAsync();
        }

    }
}
