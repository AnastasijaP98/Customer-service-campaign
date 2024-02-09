using Microsoft.EntityFrameworkCore;
using proba;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddDbContext<CustomerDb>(opt => opt.UseInMemoryDatabase("CustomerList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



var app = builder.Build();


RouteGroupBuilder customerItems = app.MapGroup("/customers");

customerItems.MapGet("/", GetAllCustomers);  
customerItems.MapGet("/loyal", GetLoyalCustomer); 
customerItems.MapGet("/successfulPurchase", GetSuccessfulPurchaseCustomer);
customerItems.MapGet("/{id}", GetCustomer); 
customerItems.MapPost("/", CreateCustomer); 
customerItems.MapPut("/{id}", UpdateCustomer); 
customerItems.MapDelete("/{id}", DeleteCustomer); 


app.Run();

static async Task<IResult> GetAllCustomers(CustomerDb db)
{
    return TypedResults.Ok(await db.Customers.Select(x => new CustomerItemDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetLoyalCustomer(CustomerDb db)
{
    return TypedResults.Ok(await db.Customers.Where(t => t.IsLoyal).Select(x => new CustomerItemDTO(x)).ToListAsync());
}

static async Task<IResult> GetSuccessfulPurchaseCustomer(CustomerDb db)
{
    return TypedResults.Ok(await db.Customers.Where(t => t.Successfulpurchase).Select(x => new CustomerItemDTO(x)).ToListAsync());
}

static async Task<IResult> GetCustomer(int id, CustomerDb db)
{
    return await db.Customers.FindAsync(id)
        is Customer customer
            ? TypedResults.Ok(new CustomerItemDTO(customer))
            : TypedResults.NotFound();
}


static async Task<IResult> CreateCustomer(CustomerItemDTO customerDTO, CustomerDb db)
{
    var customerItem = new Customer
    {
        IsLoyal = customerDTO.IsLoyal,
        Name = customerDTO.Name,
        AgentName = customerDTO.AgentName,
        Successfulpurchase = customerDTO.Successfulpurchase
    };

    db.Customers.Add(customerItem);
    await db.SaveChangesAsync();

    customerDTO = new CustomerItemDTO(customerItem);

    return TypedResults.Created($"/customers/{customerItem.Id}", customerDTO);
}

static async Task<IResult> UpdateCustomer(int id, CustomerItemDTO customerDTO, CustomerDb db)
{
    var customer = await db.Customers.FindAsync(id);

    if (customer is null) return TypedResults.NotFound();

    customer.Name = customerDTO.Name;
    customer.IsLoyal = customerDTO.IsLoyal;
    customer.AgentName = customerDTO.AgentName;
    customer.Successfulpurchase = customerDTO.Successfulpurchase;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteCustomer(int id, CustomerDb db)
{
    if (await db.Customers.FindAsync(id) is Customer customer)
    {
        db.Customers.Remove(customer);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}