using System;
using CsvHelper;
using CsvHelper.Configuration;

namespace ReportDow.Model
{
	public class CsvFileMap : ClassMap<CsvFile>
    {
        public CsvFileMap() 
        {
            Map(m => m.CustomerId).Name("CustomerId");
            Map(m => m.AgentId).Name("AgentId");
        }
    }
}

