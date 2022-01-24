using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SogetiAssessment.Models;
using SogetiAssessment.Contexts;

namespace SogetiAssessment.DataServices
{
    public class OrderDataService : DataService<Order>, IDataService<Order>
    {
        public OrderDataService(IContextWrapper<Order> contextWrapper): base(contextWrapper)
        {
            _contextWrapper = contextWrapper;
        }

        public async Task<ActionResult<List<Order>>> GetByCustomer(int id)
        {
            return await _contextWrapper.PrimarySet.Where(order => order.CustomerId == id).ToListAsync();
        }
    }
}
