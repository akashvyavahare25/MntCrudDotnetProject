using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MntCrudProject.Models;

namespace MntCrudProject.Controllers
{
    public class CustomerController : ApiController
    {
        //GET Data

        public IHttpActionResult GetAllCustomer()
        {
            IList<CustomerViewModel> customers = null;
            using (var x = new Webapi_DbEntities())
            {
                customers = x.Customers.Select(c => new CustomerViewModel()
                {
                    Id = c.id,
                    Name = c.name,
                    Email = c.email,
                    Address = c.address,
                    Phone = c.phone
                }
                ).ToList<CustomerViewModel>();
            }

            if (customers.Count == 0)
                return NotFound();
             
            return Ok(customers);
        }


        //POST Data  = Insert  new Record

        public IHttpActionResult PostNewCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data ,Please recheck");

            using (var x = new Webapi_DbEntities())
            {
                x.Customers.Add(new Customer()
                {
                    name = customer.Name,
                    email = customer.Email,
                    address = customer.Address,
                    phone = customer.Phone

                });

                x.SaveChanges();
            }
            return Ok();
        }

        // PUT -- Update data


        public IHttpActionResult PutCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is invalid model,Please recheck");

            using (var x = new Webapi_DbEntities())
            {
                var checkExistingCustomer = x.Customers.Where(c => c.id == customer.Id).FirstOrDefault<Customer>();

                if (checkExistingCustomer != null)
                {
                    checkExistingCustomer.name = customer.Name;
                    checkExistingCustomer.address = customer.Address;
                    checkExistingCustomer.phone = customer.Phone;

                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok(); 

        }

        //DELETE  --delete Data

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Please Enter valid Customer ");

            using (var x = new Webapi_DbEntities() )
            {
                var customer = x.Customers.Where(c => c.id == id).FirstOrDefault();

                x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }

            return Ok(); 
        }
    }
}
