using System;
namespace proba
{
	public class CustomerItemDTO
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsLoyal { get; set; }
        public string? AgentName { get; set; }
        public bool Successfulpurchase { get; set; }

        public CustomerItemDTO() { }
        public CustomerItemDTO(Customer customerItem) =>
        (Id, Name, IsLoyal, AgentName, Successfulpurchase) = (customerItem.Id, customerItem.Name, customerItem.IsLoyal, customerItem.AgentName, customerItem.Successfulpurchase);
    }
}

