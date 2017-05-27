using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AcmeWater.Models;
using System.IO;

namespace AcmeWater.Controllers
{
    //
    //Customer API: 1. To upload customer data file and import it to customers table.
    //              2. 
    //              3. 
    //

    public class CustomersController : ApiController
    {
        private CustomerModels db = new CustomerModels();

        /// <summary>
        /// This method can be used to import the customers from CSV file by passing the filename
        /// </summary>

        // Purpose: To upload customer data file (CSV) and import it.
        // Tables Used: Customers and Customer_Import_Log.
        // POST: api/Customers/Upload
        [ResponseType(typeof(UploadStatus))]
        public IHttpActionResult Upload()
        {
            // To be implemented - Customer File Upload...

            string file = "custdata.csv";       //store data folder path in config & use //
            int new_count, updated_count = 0;

            // To import customers.
            ImportCustomers(file, out new_count, out updated_count);

            // Insert Record in Customer Import Log details.
            Customer_Import_Log customerlog = new Customer_Import_Log();
            customerlog.ImportFileName = file;
            customerlog.TotalCustomers = new_count + updated_count;
            customerlog.CustomersNew = new_count;
            customerlog.CustomersUpdated = updated_count;
            db.Customer_Import_Log.Add(customerlog);
            db.SaveChanges();
        }

        //To import customers from file (CSV).
        private void ImportCustomers(string file, out int new_count, out int updated_count)
        {
            try
            {
                new_count = updated_count = 0;

                List<Customer> customers = GetCustomersFromFile(file);

                foreach (Customer item in customers)
                {
                    if (CustomerExists(item.CustomerUUID))  
                    {   
                        //Customer - Update.
                        db.Entry(item).State = EntityState.Modified;
                        updated_count++;
                    }
                    else
                    {
                        //Customer - Add.
                        db.Customers.Add(item);
                        new_count++;
                    }
                }

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //To check Customer exists or not.
        private bool CustomerExists(string CustomerUUID)
        {
            return db.Customers.Count(e => e.CustomerUUID == CustomerUUID) > 0;
        }

        //
        public List<Customer> GetCustomersFromFile(string file)
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                //To open and read - CSV File.
                if (Path.GetExtension(file).ToUpper() == ".CSV")
                {
                    using (var fs = File.OpenRead(file))
                    using (var reader = new StreamReader(fs))
                    {
                        var headerline = reader.ReadLine();

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            Customer customer = new Customer();
                            customer.CustomerUUID = values[0];
                            customer.CustomerName = values[1];
                            customer.CustomerEmail = values[2];
                            customer.CustomerAddress = values[3];
                            customer.CustomerCity = values[4];
                            customer.CustomerState = values[5];
                            customer.CustomerZip = values[6];

                            customers.Add(customer);
                        }                       
                    }
                } else
                {
                    //To be implemented for other file types.
                }
            }
            catch (Exception)
            {
                throw;
            }
            return customers;
        }
    }
}