using HSEApiTraining.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #region Get
        [HttpGet]
        public GetCustomersResponse GetCustomers([FromQuery]int count)
        {
            var result = _customerService.GetCustomers(count);
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpGet("GetAll")]
        public GetCustomersResponse GetAll()
        {
            var result = _customerService.GetAll();
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public DeleteCustomerResponse DeleteCustomer(int id)
        {
            return new DeleteCustomerResponse
            {
                Error = _customerService.DeleteCustomer(id)
            };
        }

        [HttpDelete("All")]
        public DeleteCustomerResponse DeleteAll()
        {
            return new DeleteCustomerResponse
            {
                Error = _customerService.DeleteAll()
            };
        }
        #endregion
    }
}
