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

namespace AcmeWater.Controllers
{
    //
    //Billing API:  1. Generate Billing for given period (Month & Year)
    //              2. Get Billing Summary for given period (Month & Year)
    //              3. Get Billing Detials for given period (Month & Year)
    //
    public class Billing_TxnsController : ApiController
    {
        private BillingModels db_billing = new BillingModels();
        private CustomerModels db_customer = new CustomerModels();

        // Purpose: To generate the customer billing for given period (month & year).
        // Tables Used: Billing_Txns and Billibg_Log
        // GET: api/Billing/Generate/{YEAR}/{MONTH}
        public IHttpActionResult GenerateBilling(int year, int month)
        {
            //To be added - check user is authorized to generate the billing.
            
            try
            {
                int day, logid, generatedcount, emailedcount = 0;
                decimal amountdue, totalamount = 0;

                //Get Billing Day -- Current day for current month / Last day for prior months (to rerun in case any billings missed for the past).
                day = GetBillingDay(year, month);

                DateTime billingdate = new DateTime(year, month, day);

                //Add Billing Log (To capture the biling summary - billing generated count, emailed count and total amount).
                logid = InsertBillingLog(billingdate);

                //Get customers (who are not billed yet Today) with past due for the given period.
                List<Customer> customers = GetCustomersToBill(year, month, day);

                foreach (Customer item in customers)
                {
                    //To fetch customer due for given month thru API (Note: Due is as on current date).
                    amountdue = GetCustomerDueThruAPI(item.CustomerUUID, year, month);
                    
                    //Notify Customer and add billing txn.
                    if ((amountdue - item.PastDue) > 0)
                    {
                        //Notify to Customer (To be implemented yet)
                        //CustomerEmail = Notify.Email(item, amountdue);

                        Billing_Txns billing_txn = new Billing_Txns();

                        billing_txn.BillingDate = billingdate;
                        billing_txn.CustomerId = item.CustomerId;
                        billing_txn.BillingLogId = logid;
                        billing_txn.PastDue = item.PastDue;
                        billing_txn.CurrentDue = amountdue - item.PastDue;
                        billing_txn.EmailedTo = item.CustomerEmail;
                        billing_txn.EmailText = CustomerEmail.EmailText;
                        if (CustomerEmail.IsEmailed)
                        {
                            billing_txn.EmailedOn = currentdate;
                            emailedcount++;
                        }
                        generatedcount++;
                        totalamount += billing_txn.CurrentDue;
                        db_billing.Billing_Txns.Add(billing_txn);
                    }
                }

                //To update the billing log with summary info (for each batch / run).
                UpdateBillingLog(logid, generatedcount, emailedcount, totalamount);

                db_billing.SaveChanges();

                //Send Billing Summary Email (can be configured).
            }
            catch (Exception)
            {
                throw;  /* Log Error & Notify thru email */
            }

            return Ok();
        }

        // Purpose: To get the billing summary for given period (month & year).
        // Tables Used: Billing_Log
        // GET: api/Billing/Summary/{YEAR}/{MONTH}
        [ResponseType(typeof(List<Billing_Log>))]
        public IHttpActionResult GetBillingSummary(int year, int month)
        {
            Billing_Log billing_log;

            //To be implemented - Get Billing Summary like Generated Count, Emailed Count and Total Amount.

            if (billing_log == null)
            {
                return NotFound();
            }

            return Ok(billing_log);
        }

        // Purpose: To get the billing details for given period.
        // Tables Used: Billing_Txns and Customer
        // GET: api/Billing/Details/{YEAR}/{Month}
        [ResponseType(typeof(List<Billing_Txns>))]
        public IHttpActionResult GetBillingDetails(int year, int month)
        {
            Billing_Txns billing_txns;

            //To be implemented - Get Billing Dettails like Billing Date, Customer Name, Amount Due, Emailed To, Generated Date, etc.

            if (billing_txns == null)
            {
                return NotFound();
            }

            return Ok(billing_txns);
        }

        //To get Billing Day (Current Day for current month / last day for prior months).
        private int GetBillingDay(int year, int month)
        {
            int day = 0;
            DateTime currentdate = new DateTime();
            if (currentdate.Month == month && currentdate.Year == year)
            {
                day = currentdate.Day;
            } else
            {
                day = DateTime.DaysInMonth(year, month);
            }
            return day;
        }

        //To get the customers for billing (who are not billed yet) with past due (if there any) for the given period.
        private List<Customer> GetCustomersToBill(int year, int month, int day)
        {
            List<Customer> customers = new List<Customer>();

            //To be implemented - Get customers using the query who are not billed yet along with the past due if there any.

            return customers;
        }

        //To get the customer due for given month & year using API.
        private decimal GetCustomerDueThruAPI(string customeruuid, int year, int month)
        {
            decimal amount_due = 0;

            //To Be Implemented -- Call REST API and Get Customer Due -- (store API Base URL in config) --

            return amount_due;
        }

        //To insert the billing log - summary
        private int InsertBillingLog(DateTime billingdate)
        {
            int logid = 0;

            //To be implemented - Insert Billing Log

            return logid;
        }

        //To update the billing log - summary
        private void UpdateBillingLog(int logid, int generatedcount, int emailedcount, decimal totalamount)
        {
            //To be implemented - Update Billing Log - Summary Info.
        }
    }
}