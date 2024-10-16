using System;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts;

public interface ICustomerService
{
    public IEnumerable<Customer> GetAllCustomers();
    public Customer GetCustomer(int id);
    public Customer CreateCustomer(Customer customer);
    public Customer UpdateCustomer(Customer customer);
    public void DeleteCustomer(int id);
}
