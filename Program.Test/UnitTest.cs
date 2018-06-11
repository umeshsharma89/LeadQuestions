using System;
using NUnit.Framework;
using Program;

namespace Program.Test
{
   [TestFixture]
    public class UnitTest
    {
        [TestCase]        
        public void ProgrammerTest()
        {

            Address address = new Address("56 Main St", "Mesa", "AZ", "38574");
            Customer customer = new Customer("John", "Doe", address);
            Company company = new Company("Google", address);

            Assert.IsNotNull(customer.Id);
            customer.Save();
            Assert.IsNotNull(customer.Id);

            Assert.IsNotNull(company.Id);
            company.Save();
            Assert.IsNotNull(company.Id);

            Customer savedCustomer = (customer.Find<Customer>(customer.Id));
            Assert.IsNotNull(savedCustomer);
            Assert.AreSame(customer.Address, address);           
            Assert.AreEqual(customer.Id, savedCustomer.Id);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
            Assert.AreEqual(savedCustomer.Address.AddressLine, address.AddressLine);
            Assert.AreEqual(savedCustomer.Address.Country, address.Country);
            Assert.AreEqual(savedCustomer.Address.Pincode, address.Pincode);
            Assert.AreEqual(savedCustomer.Address.State, address.State);
            Assert.AreNotSame(customer, savedCustomer);

            Company savedCompany = company.Find<Company>(company.Id);
            Assert.IsNotNull(savedCompany);
            Assert.AreSame(company.Address, address);
            Assert.AreEqual(company.Id, savedCompany.Id);
            Assert.AreEqual(company.Name, savedCompany.Name);
            Assert.AreEqual(savedCompany.Address.AddressLine, address.AddressLine);
            Assert.AreEqual(savedCompany.Address.Country, address.Country);
            Assert.AreEqual(savedCompany.Address.Pincode, address.Pincode);
            Assert.AreEqual(savedCompany.Address.State, address.State);
            Assert.AreNotSame(company, savedCompany);

            customer.Delete();
            Assert.IsNotNull(customer.Id);
             Assert.IsNull(customer.Find<Customer>(customer.Id));

            company.Delete();
            Assert.IsNotNull(company.Id);
             Assert.IsNull(company.Find<Company>(company.Id));         
        }


    }
}
