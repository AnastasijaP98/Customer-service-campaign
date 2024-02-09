using System;
using CsvHelper.Configuration.Attributes;

namespace ReportDow.Model
{
	public class CsvFile
	{
        [Name("customerId")]
        public string? CustomerId { get; set; }

        [Name("agentId")]
        public string? AgentId { get; set; }
    }
}

