namespace DapperCrudTutorial.Model
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; } = String.Empty;
        public string ContactTitle { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Region { get; set; } = String.Empty;
        public string PostalCode { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Fax { get; set; } = String.Empty;
    }
}
