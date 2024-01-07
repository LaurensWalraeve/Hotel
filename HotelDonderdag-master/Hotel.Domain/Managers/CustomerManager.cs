using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("GetCustomers");
            }
        }

        public void AddCustomers(Customer customer) 
        {
            try
            {
                _customerRepository.AddCustomer(customer);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("AddCustomer");
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            try
            {
                _customerRepository.DeleteCustomer(customer);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("DeleteCustomer");
            }
        }
        public void UpdateCustomers(Customer customer)
        {
            try
            {
                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception)
            {

                throw new CustomerManagerException("UpdateCustomers");
            }
        }


    }
}
