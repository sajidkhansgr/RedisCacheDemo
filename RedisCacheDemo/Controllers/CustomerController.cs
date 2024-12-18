using Microsoft.AspNetCore.Mvc;
using RedisCacheDemo.Cache;
using RedisCacheDemo.Models;

namespace RedisCacheDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICacheService _cacheService;
        private readonly AdventureWorks2019Context _dbContext;
        public CustomerController(ICacheService cacheService, AdventureWorks2019Context dbContext) 
        { 
            _cacheService = cacheService;
            _dbContext = dbContext;
        }
        [HttpGet("GetCustomers")]
        public IEnumerable<Customer> Get() 
        {
            var cachedData = _cacheService.GetData<IEnumerable<Customer>>("Customer");
            if (cachedData != null) 
            {
                return cachedData;
            }
            var expirationTime = DateTime.Now.AddMinutes(5);
            cachedData = _dbContext.Customers.ToList();
            _cacheService.SetData<IEnumerable<Customer>>("Customer", cachedData, expirationTime);
            return cachedData;
        }
        [HttpPut("Update")]
        public void Update(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();
            _cacheService.RemoveData("Customer");
        }
        [HttpDelete("Delete")]
        public void Delete(string customerId)
        {
            var filteredData = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == customerId);
            _dbContext.Customers.Remove(filteredData);
            _dbContext.SaveChanges();
            _cacheService.RemoveData("Customer");
        }
    }
}
