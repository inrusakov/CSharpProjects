using HSEApiTraining.Models.Calculator;
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

        [HttpGet("GetBanned")]
        public GetCustomersResponse GetBanned()
        {
            var result = _customerService.GetBanned();
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpGet("GetNotBanned")]
        public GetCustomersResponse GetNotBanned()
        {
            var result = _customerService.GetNotBanned();
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpGet("SearchByName/{searchTerm}")]
        public GetCustomersResponse SearchByName(string searchTerm)
        {
            var result = _customerService.SearchByName(searchTerm);
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpGet("SearchBySurname/{searchTerm}")]
        public GetCustomersResponse SearchBySurname(string searchTerm)
        {
            var result = _customerService.SearchBySurname(searchTerm);
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }
        #endregion

        #region Post
        [HttpPost]
        public AddCustomerResponse AddCustomer([FromBody] AddCustomerRequest request)
        {
            return new AddCustomerResponse
            {
                Error = _customerService.AddCustomer(request)
            };
        }

        [HttpPost("GenerateRandomCustomers")]
        public AddCustomerResponse GenerateRandomCustomers(int count)
        {
            return new AddCustomerResponse
            {
                Error = _customerService.GenerateRandomCustomers(count)
            };
        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public UpdateCustomerResponse UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
        {
            return new UpdateCustomerResponse
            {
                Error = _customerService.UpdateCustomer(id, request)
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
