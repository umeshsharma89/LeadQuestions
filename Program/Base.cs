using Program.Repo;
using Program.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Program
{
    public class Base
    {
        private readonly IBase _base;
        public Base()
        {
            this._base = new BaseRepo();
        }
        public void Save()
        {
            _base.Save(this);
        }
        public void Delete()
        {
            _base.Delete(this);
        }
        public T Find<T>(string id)
        {
            return _base.Find<T>(id);   
        }
        public string GetID()
        {
            return Guid.NewGuid().ToString();
        }
        
    }

    public class Customer : Base
    {
        public Customer()
        {
        }
        public Customer(string firstName, string lastName, Address address)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Id = GetID();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Company : Base
    {
        public Company()
        {
        }
        public Company(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
            this.Id = GetID();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        public Address()
        {
        }
        public Address(string addressLine, string state, string country, string pincode)
        {
            this.AddressLine = addressLine;
            this.State = state;
            this.Country = country;
            this.Pincode = pincode;
        }
        public string AddressLine { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
    }
}
