# Mediator Pattern
The essence of the Mediator Pattern, originally introduced in the 1994 book ["Design Patterns: Elements of Reusable Object-Oriented Software"](https://en.wikipedia.org/wiki/Design_Patterns), is to define an object that encapsulates how a set of objects interact. It assists loose coupling by not having direct referrals between objects.

 We make use of [Mediatr](https://github.com/jbogard/MediatR), a .NET implementation of the Mediator pattern, to communicate between Lambda's in our presentation layer, and business logic in the Application layer.

## Sample code

#### 1. Inject Mediatr.
``` csharp
services.AddMediatR(Assembly.GetExecutingAssembly());
```

#### 2. Create a *Request* and *Handler*.

```` csharp
public class CreateConcertCommand : IRequest<Guid>
{

    public string Name { get; set; }
    public Guid VenueId { get; set; }
    public string ImageUrl { get; set; }
    public string Date { get; set; }
    
    public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, Guid>
    {

        private readonly IRepository<Concert> _repository;

        public CreateConcertCommandHandler(IRepository<Concert> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateConcertCommand request)
        {   
            
            var concert = new Concert(request.Name, request.VenueId, request.ImageUrl);
            
            await _repository.SaveAsync(concert);
            
            return concert.Id; 
        }
    }
}
````
#### 3. Send a request and wait for it's response.
``` csharp
public class CreateConcertFunction : FunctionBase
{
    
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
    public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
    {
        // Deserialize the request body to a mediatr request (CreateConcertCommand)
        var requestModel = JsonConvert.DeserializeObject<CreateConcertCommand>(request.Body);

        try
        {
            // Send the request with Mediator
            var result = await Mediator.Send(requestModel); 
            // Return the response             
            return GenerateResponse(201, result);
        }
        catch (Exception ex)
        {
            return GenerateResponse(400, ex.Message);
        }            
        
    }
}
```
## Further reading

* [Mediator Pattern](https://en.wikipedia.org/wiki/Mediator_pattern) - Wikipedia.org
* [Using CQRS pattern with MediatR](https://medium.com/@ducmeit/net-core-using-cqrs-pattern-with-mediatr-part-1-55557e90931b) - by Duc Ho