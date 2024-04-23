using API_Labb3.DTOs;
using API_Labb3.Entity;
using API_Labb3.Filter;
using API_Labb3.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace API_Labb3.Endpoints
{
    public static class PeopleEndpoints
    {
        public static RouteGroupBuilder MapPeople(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("people-get"))
                .WithOpenApi(options =>
                {
                    options.Summary = "Get all people - Pagination- max is 50 per page";
                    return options;
                });
            group.MapGet("/{id:int}", GetById).WithOpenApi(options =>
            {
                options.Summary = "Get by ID - including Hobbies and Links";
                return options;
            });
            group.MapGet("/{name}", GetByName).WithOpenApi(options =>
            {
                options.Summary = "Search by Name - Response includes Hobbies and Links";
                return options;
            });
            group.MapGet("/{id:int}/links", GetLinks).WithOpenApi(options =>
            {
                options.Summary = "Get all links for a person";
                return options;
            });
            group.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CreatePersonDTO>>().WithOpenApi(options =>
            {
                options.Summary = "Create new person";
                return options;
            });
            group.MapPut("/{id:int}", Update).AddEndpointFilter<ValidationFilter<CreatePersonDTO>>();
            group.MapDelete("/{id:int}", Delete);

            return group;
        }

        static async Task<Ok<List<PersonDTO>>> GetAll(IPeopleRepository repository,
            int page = 1, int pageSize = 10)
        {
            var pagination = new PaginationDTO { Page = page, PageSize = pageSize };
            var people = await repository.GetAll(pagination);

            var personsDTO = people.Select(g => new PersonDTO
            {
                Id = g.Id,
                Name = g.Name,
                Email = g.Email,
                PhoneNumber = g.PhoneNumber,
                Hobbies = g.Hobbies,

            }).ToList();


            return TypedResults.Ok(personsDTO);
        }

        static async Task<Results<Ok<PersonDTO>, NotFound<string>>> GetById(int id, IPeopleRepository repository)
        {
            var person = await repository.GetById(id);

            if (person == null)
            {
                return TypedResults.NotFound($"Did not find person with id {id}");
            }
            var personDTO = new PersonDTO
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                Hobbies = person.Hobbies,

            };

            return TypedResults.Ok(personDTO);
        }

        static async Task<Ok<List<PersonDTO>>> GetByName(string name, IPeopleRepository peopleRepository)
        {
            var people = await peopleRepository.GetByName(name);
            var personsDTO = people.Select(p => new PersonDTO
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Hobbies = p.Hobbies

            }).ToList();
            return TypedResults.Ok(personsDTO);
        }

        static async Task<Created<PersonDTO>> Create(CreatePersonDTO createPersonDTO, IPeopleRepository repository, IOutputCacheStore outputCacheStore)
        {
            var person = new Person
            {
                Name = createPersonDTO.Name,
                Email = createPersonDTO.Email,
                PhoneNumber = createPersonDTO.PhoneNumber,
            };
            var id = await repository.Create(person);

            var personDTO = new PersonDTO
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
            };

            await outputCacheStore.EvictByTagAsync("people-get", default);
            return TypedResults.Created($"/api/people/{id}", personDTO);
        }

        static async Task<Results<NoContent, NotFound>> Update(int id, CreatePersonDTO createPersonDTO,
            IPeopleRepository repository, IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);

            if (!exists)
            {
                return TypedResults.NotFound();
            }

            var person = new Person
            {
                Id = id,
                Name = createPersonDTO.Name,
                Email = createPersonDTO.Email,
                PhoneNumber = createPersonDTO.PhoneNumber,
            };

            await repository.Update(person);
            await outputCacheStore.EvictByTagAsync("people-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Delete(int id, IPeopleRepository repository,
            IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);
            if (!exists)
            {
                return TypedResults.NotFound();
            }
            await repository.Delete(id);
            await outputCacheStore.EvictByTagAsync("people-get", default);
            return TypedResults.NoContent();
        }


        static async Task<Results<NotFound<string>, Ok<ShowAllLinksDTO>>> GetLinks(int id,
            IPeopleRepository peopleRepository)
        {
            var person = await peopleRepository.GetAllLinks(id);

            if (person == null)
            {
                return TypedResults.NotFound($"Person with id {id} not found");
            }

            if (person.Hobbies.Count == 0)
            {
                return TypedResults.NotFound("This person have no links");
            }

            var showLinks = new ShowAllLinksDTO();

            foreach (var item in person.Hobbies)
            {
                foreach (var link in item.Links)
                {
                    showLinks.Urls.Add(link);
                }
            }

            return TypedResults.Ok(showLinks);
        }

    }
}
