using Dapper;
using DapperCrudTutorial.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CustomerController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<Customer> customers = await SelectAllCustomers(connection);
            return Ok(customers);
        }

       
        [HttpGet("{city}")]
        public async Task<ActionResult<Customer>> GetCustomer(string city)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            //var customer = await connection.QueryFirstAsync<Customer>("select * from customers where City = @City", new { City = city}); //Return 1 record
            IEnumerable<Customer> customer = await SelectCustomer(city, connection); //Return 5 record
            return Ok(customer);
        }

       

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> CreateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into customers (customerID , companyName , contactName , contactTitle , address , city , region , postalCode , country , phone , fax) values " +
                "(@CustomerID , @CompanyName , @ContactName , @ContactTitle , @Address , @City , @Region , @PostalCode , @Country , @Phone , @Fax)", customer);
            //return Ok(await SelectAllCustomers(connection));
            return Ok(await SelectCustomerByCustomerId(customer.CustomerID,connection));
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("update customers set customerId = @CustomerId, companyName = @CompanyName, contactName = @ContactName, contactTitle = @ContactTitle, address = @Address, city = @City, region = @Region, postalCode = @PostalCode, country = @Country, phone = @Phone, fax = @Fax where customerId = @CustomerId",customer);
            //return Ok(await SelectAllCustomers(connection));
            return Ok(await SelectCustomerByCustomerId(customer.CustomerID, connection));
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult<List<Customer>>> DeleteCustomer(string customerId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from customers where customerId = @CustomerId", new { CustomerId = customerId });
            return Ok("Deleted");
        }

        private static async Task<IEnumerable<Customer>> SelectAllCustomers(SqlConnection connection)
        {
            return await connection.QueryAsync<Customer>("select * from customers");
        }

        private static async Task<IEnumerable<Customer>> SelectCustomer(string city, SqlConnection connection)
        {
            return await connection.QueryAsync<Customer>("select * from customers where City = @City", new { City = city });
        }

        private static async Task<IEnumerable<Customer>> SelectCustomerByCustomerId(string customerId, SqlConnection connection)
        {
            return await connection.QueryAsync<Customer>("select * from customers where CustomerId = @CustomerId", new { CustomerId = customerId });
        }

    }
}
