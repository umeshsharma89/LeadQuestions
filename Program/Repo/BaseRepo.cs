using Program.Repo.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Data;
using System.Reflection;

namespace Program.Repo
{
    public class BaseRepo : IBase
    {

        public void Delete<T>(T entity)
        {
            string path = "";
            if (entity.GetType() == typeof(Customer))
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "//Customer.xml";
                if (File.Exists(path))
                {
                    List<Customer> list = new List<Customer>();
                    GetCustomerList(path, list);
                    Customer cust = (Customer)Convert.ChangeType(entity, typeof(Customer));
                    list.RemoveAll(q => q.Id == cust.Id);
                    CreateCustomerXML(list);
                }
            }
            else
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "//Company.xml";
                if (File.Exists(path))
                {
                    List<Company> list = new List<Company>();
                    GetComapnyList(path, list);
                    Company com = (Company)Convert.ChangeType(entity, typeof(Company));
                    list.RemoveAll(q => q.Id == com.Id);
                    CreateCompanyXML(list);
                }
            }
        }

        public T Find<T>(string id)
        {
            string path = "";

            if (typeof(T)==typeof(Customer))
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "//Customer.xml";
                List<Customer> list = new List<Customer>();
                if (File.Exists(path))
                {
                    GetCustomerList(path, list);
                }
                var result = list.Where(q => q.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(result, typeof(T));
            }
            else
            {

                path = System.AppDomain.CurrentDomain.BaseDirectory + "//Company.xml";
                List<Company> list = new List<Company>();
                if (File.Exists(path))
                {
                    GetComapnyList(path, list);
                }
                var result = list.Where(q => q.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(result, typeof(T));
            }           
        }

        public void Save<T>(T entity)
        {
            WriteXML(entity);
        }
        public void WriteXML<T>(T entity)
        {          
            string path = "";
            if (entity.GetType() == typeof(Customer))
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "//Customer.xml";
                List<Customer> list = new List<Customer>();
                if (File.Exists(path))
                {
                    GetCustomerList(path, list);
                }
                list.Add((Customer)Convert.ChangeType(entity, typeof(Customer)));
                CreateCustomerXML(list);
            }
            else
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "//Company.xml";
                List<Company> list = new List<Company>();
                if (File.Exists(path))
                {
                    GetComapnyList(path, list);
                }
                list.Add((Company)Convert.ChangeType(entity, typeof(Company)));
                CreateCompanyXML(list);
            }
        }

        private static void GetComapnyList(string path, List<Company> list)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var root = xmlDoc.SelectNodes("Root");
            foreach (XmlNode RootItem in root)
            {
                var itemNodes = RootItem.SelectNodes("Company");
                foreach (XmlNode node in itemNodes)
                {
                    Company cust = new Company();
                    cust.Id = node.SelectSingleNode("Id").InnerText;
                    cust.Name = node.SelectSingleNode("Name").InnerText;
                    var Address = node.SelectNodes("Address");
                    foreach (XmlNode loc in Address)
                    {
                        Address add = new Program.Address();
                        add.AddressLine = loc.SelectSingleNode("AddressLine").InnerText;
                        add.Country = loc.SelectSingleNode("Country").InnerText;
                        add.State = loc.SelectSingleNode("State").InnerText;
                        add.Pincode = loc.SelectSingleNode("Pincode").InnerText;
                        cust.Address = add;
                    }
                    list.Add(cust);
                }
            }
        }

        private static void GetCustomerList(string path, List<Customer> list)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var root = xmlDoc.SelectNodes("Root");
            foreach (XmlNode RootItem in root)
            {
                var itemNodes = RootItem.SelectNodes("Customer");
                foreach (XmlNode node in itemNodes)
                {
                    Customer cust = new Customer();
                    cust.Id = node.SelectSingleNode("Id").InnerText;
                    cust.FirstName = node.SelectSingleNode("FirstName").InnerText;
                    cust.LastName = node.SelectSingleNode("LastName").InnerText;
                    var Address = node.SelectNodes("Address");
                    foreach (XmlNode loc in Address)
                    {
                        Address add = new Program.Address();
                        add.AddressLine = loc.SelectSingleNode("AddressLine").InnerText;
                        add.Country = loc.SelectSingleNode("Country").InnerText;
                        add.State = loc.SelectSingleNode("State").InnerText;
                        add.Pincode = loc.SelectSingleNode("Pincode").InnerText;
                        cust.Address = add;
                    }
                    list.Add(cust);
                }
            }
        }      
        public void CreateCompanyXML(List<Company> list)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "//Company.xml";
            File.Delete(path);
            if (list.Count > 0)
            {
                XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("Root");
                foreach (var item in list)
                {
                    writer.WriteStartElement("Company");
                    writer.WriteStartElement("Id");
                    writer.WriteString(item.Id);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Name");
                    writer.WriteString(item.Name);
                    writer.WriteEndElement();
                    createNode(item.Address.AddressLine, item.Address.Country, item.Address.State, item.Address.Pincode, writer);
                    writer.WriteEndElement();

                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();                
            }
        }
        public void CreateCustomerXML(List<Customer> list)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "//Customer.xml";
            File.Delete(path);
            if (list.Count > 0)
            {
                XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("Root");

                foreach (var item in list)
                {
                    writer.WriteStartElement("Customer");
                    writer.WriteStartElement("Id");
                    writer.WriteString(item.Id);
                    writer.WriteEndElement();
                    writer.WriteStartElement("FirstName");
                    writer.WriteString(item.FirstName);
                    writer.WriteEndElement();
                    writer.WriteStartElement("LastName");
                    writer.WriteString(item.LastName);
                    writer.WriteEndElement();
                    createNode(item.Address.AddressLine, item.Address.Country, item.Address.State, item.Address.Pincode, writer);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();                
            }

        }

        private void createNode(string AddressLine, string Country, string State, string Pincode, XmlTextWriter writer)
        {
            writer.WriteStartElement("Address");
            writer.WriteStartElement("AddressLine");
            writer.WriteString(AddressLine);
            writer.WriteEndElement();
            writer.WriteStartElement("Country");
            writer.WriteString(Country);
            writer.WriteEndElement();
            writer.WriteStartElement("State");
            writer.WriteString(State);
            writer.WriteEndElement();
            writer.WriteStartElement("Pincode");
            writer.WriteString(Pincode);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}

