
## Documents
- [https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/](https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/)
- [https://codewithmukesh.com/blog/repository-pattern-caching-hangfire-aspnet-core/](https://codewithmukesh.com/blog/repository-pattern-caching-hangfire-aspnet-core/)

## Observations
- Application project has a reference to Microsoft.EntityFramework.Core.
    - THis means that the applicaiton layer is not agnostic to db implementation
    - The ref is probably there to be able to use DbSet in IDbContext

## Creating the entity and add it to the DBCOntext
- Created entity PollQuestion.cs in Domain project /Entities/Vote
    - The model inherits the contract AuditableEntity
    - TId is the ID type and is set to int
    - Added DbSet<PollQuestion> to BlazorHeroContext in the infrastructure project

## Dependencies
- MediatR and automapper is already added in file src\Application\Extensions\ServiceCollectionExtensions.cs
    - Every project has a ServiceCollectionExtensions to add the required services


## Add features
- 