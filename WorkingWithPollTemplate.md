
## Documents
- [https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/](https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/)
- [https://codewithmukesh.com/blog/specification-pattern-in-aspnet-core/](https://codewithmukesh.com/blog/specification-pattern-in-aspnet-core/)
- [https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/](https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/)
- [https://codewithmukesh.com/blog/repository-pattern-caching-hangfire-aspnet-core/](https://codewithmukesh.com/blog/repository-pattern-caching-hangfire-aspnet-core/)

## Observations
- Application project has a reference to Microsoft.EntityFramework.Core.
    - THis means that the applicaiton layer is not agnostic to db implementation
    - The ref is probably there to be able to use DbSet in IDbContext
- THe project is using caching. THis is handled in the command and query objects. We can probably remove this from our application.

## Creating the entity and add it to the DBCOntext
- Created entity PollQuestion.cs in Domain project /Entities/Vote
    - The model inherits the contract AuditableEntity
    - TId is the ID type and is set to int
    - Added DbSet<PollQuestion> to BlazorHeroContext in the infrastructure project

## Dependencies
- MediatR and automapper is already added in file src\Application\Extensions\ServiceCollectionExtensions.cs
    - Every project has a ServiceCollectionExtensions to add the required services

## Create a mapping profile
- Added class PollQuestionProfile and added mapping: "CreateMap<AddPollQuestionCommand, PollQuestion>().ReverseMap();"

## Add features (Commands and queries)
- Created "AddPollQuestionCommand" using "AddEditBrandCommand as guide"
    - Removed code relevant for editing a brand
    - Commented out caching
    - Added ""commit" call instead of commitandremovecache

##Update database
- add-migration pollQuestion
- update-database

# Program flow
1. Http Post received by controller