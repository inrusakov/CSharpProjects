using HSEApiTraining.Models.Customer;
using System;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public interface ICustomerService
    {
        (IEnumerable<Customer> Customers, string Error) GetCustomers(int count);
        (IEnumerable<Customer> Customers, string Error) GetAll();
        string DeleteCustomer(int id);
        string DeleteAll();
    }

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) 
            => _customerRepository = customerRepository;

        public (IEnumerable<Customer> Customers, string Error) GetCustomers(int count) 
            => _customerRepository.GetCustomers(count);

        public (IEnumerable<Customer> Customers, string Error) GetAll()
            => _customerRepository.GetAll();

        public string DeleteCustomer(int id) 
            => _customerRepository.DeleteCustomer(id);

        public string DeleteAll()
            => _customerRepository.DeleteAll();
    }
}
