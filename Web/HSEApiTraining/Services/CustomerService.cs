using HSEApiTraining.Models.Customer;
using System;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public interface ICustomerService
    {
        (IEnumerable<Customer> Customers, string Error) GetCustomers(int count);
        (IEnumerable<Customer> Customers, string Error) GetAll();
        (IEnumerable<Customer> Customers, string Error) GetBanned();
        (IEnumerable<Customer> Customers, string Error) GetNotBanned();
        (IEnumerable<Customer> Customers, string Error) SearchByName(string name);
        (IEnumerable<Customer> Customers, string Error) SearchBySurname(string surname);
        string AddCustomer(AddCustomerRequest request);
        string GenerateRandomCustomers(int count);
        string UpdateCustomer(int id, UpdateCustomerRequest request);
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

        public (IEnumerable<Customer> Customers, string Error) GetBanned()
            => _customerRepository.GetBanned();

        public (IEnumerable<Customer> Customers, string Error) GetNotBanned()
            => _customerRepository.GetNotBanned();

        public (IEnumerable<Customer> Customers, string Error) SearchByName(string name)
            => _customerRepository.SearchByName(name);

        public (IEnumerable<Customer> Customers, string Error) SearchBySurname(string surname)
            => _customerRepository.SearchBySurname(surname);

        public string AddCustomer(AddCustomerRequest request) 
            => _customerRepository.AddCustomer(request);

        public string GenerateRandomCustomers(int count)
            => _customerRepository.GenerateRandomCustomers(count);

        public string DeleteCustomer(int id) 
            => _customerRepository.DeleteCustomer(id);

        public string DeleteAll()
            => _customerRepository.DeleteAll();

        public string UpdateCustomer(int id, UpdateCustomerRequest request) 
            => _customerRepository.UpdateCustomer(id, request);
    }
}
