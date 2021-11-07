
# Documents
- [https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/](https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/)
- [https://codewithmukesh.com/blog/specification-pattern-in-aspnet-core/](https://codewithmukesh.com/blog/specification-pattern-in-aspnet-core/)
- [https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/](https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/)
- [https://codewithmukesh.com/blog/repository-pattern-caching-hangfire-aspnet-core/](https://codewithmukesh.com/blog/repository-pattern-caching-hangfire-aspnet-core/)

# Observations
- Application project has a reference to Microsoft.EntityFramework.Core.
    - THis means that the applicaiton layer is not agnostic to db implementation
    - The ref is probably there to be able to use DbSet in IDbContext
- The project is using caching. This is handled in the command and query objects. We can probably remove this from our application.
- We can remove localization?


# Adding a Poll Question "Post" endpoint

## Creating the entity and add it to the DBContext
- Created entity PollQuestion.cs in Domain project /Entities/Vote
    - The model inherits the contract AuditableEntity
    - TId is the ID type and is set to int
    - Added `DbSet<PollQuestion>` to BlazorHeroContext in the infrastructure project

## Dependencies
- MediatR and automapper is already added in file src\Application\Extensions\ServiceCollectionExtensions.cs
    - Every project has a ServiceCollectionExtensions to add the required services

## Create a mapping profile
- Added class PollQuestionProfile and added mapping: "CreateMap<AddPollQuestionCommand, PollQuestion>().ReverseMap();"

## Add features (Commands and queries)
- Created "AddPollQuestionCommand" using "AddEditBrandCommand as guide"
    - Removed code relevant for editing since poll questions can not be edited
    - Commented out caching since this is not needed in our application
    - Added ""commit" call instead of commitandremovecache

## Created Poll Question controller

```

        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddPollQuestionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
```

## Update database and run application
- add-migration pollQuestion
- update-database

## Using Swagger to test API

Authenticate using Bearer:
Get token by using the toekn controller and

```
{
  "email": "mukesh@blazorhero.com",
  "password": "123Pa$$word!"
}
```

Add token to Bearer

# Client - List all poll question
- Using Brand page as guide
- Copy Brands.Razor and change namespace/classname etc. to PollQUestions
- Create PollQuestionManager

# Program flow
1. Http Post received by controller