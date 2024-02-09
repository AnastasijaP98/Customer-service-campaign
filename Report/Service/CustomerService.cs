using System;
using CsvHelper;
using CsvHelper.Configuration;
using ReportDow.Model;
using System.Globalization;

namespace ReportDow.Service
{
    public class CustomerService
    {
        public void ExecuteService()
        {
            var filteredCustomers = new List<CsvFile>();

            ReadCsvFile(filteredCustomers);
            WriteCsvFile(filteredCustomers);
        }

        public void ReadCsvFile(List<CsvFile> filteredCustomers)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader("YOUR FILE DIRECTORY\\customers.csv"))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    var products = csv.GetRecords<CsvFile>().ToList();

                    foreach (var product in products)
                    {
                        var repeatCustomer = products.FindAll(p => p.CustomerId == product.CustomerId);
                        var filteredCustomerAlreadyContainsAgents = filteredCustomers.Where(p => p.CustomerId == product.CustomerId).ToList();

                        if (repeatCustomer.Count > 1 && !filteredCustomerAlreadyContainsAgents.Any())
                            filteredCustomers.Add(product);
                    }
                }
            }
        }

        public void WriteCsvFile(List<CsvFile> filteredCustomer)
        {
            using (var writer = new StreamWriter("YOUR FILE DIRECTORY\\CustomerFiltered.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvFileMap>();

                    csv.WriteHeader<CsvFile>();
                    csv.NextRecord();
                    csv.WriteRecords(filteredCustomer);
                }
            }
        }
    }
}

