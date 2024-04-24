using API_Labb3.DTOs;
using API_Labb3.Entity;
using API_Labb3.Filter;
using API_Labb3.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace API_Labb3.Endpoints
{
    public static class HobbiesEndpoints
    {
        public static RouteGroupBuilder MapHobbySearch(this RouteGroupBuilder builder)
        {
            builder.MapGet("/title", GetByTitle).WithOpenApi(options =>
            {
                options.Summary = "Search by Title -  /api/hobbies?title=    ,Response includes Links";
                return options;
            });
            return builder;
        }

        public static RouteGroupBuilder MapHobbies(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("hobbies-get"))
                .WithOpenApi(options =>
                {
                    options.Summary = "Get all hobbies for a person";
                    return options;
                });

            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CreateHobbyDTO>>().WithOpenApi(options =>
            {
                options.Summary = "Assign hobby to a person";
                return options;
            });
            group.MapPut("/{id:int}", Update).AddEndpointFilter<ValidationFilter<CreateHobbyDTO>>();
            group.MapDelete("/{id:int}", Delete);

            return group;
        }

        static async Task<Results<Ok<List<HobbyDTO>>, NotFound>> GetAll(int personId,
            IHobbiesRepository hobbiesRepository, IPeopleRepository peopleRepository)
        {
            if (!await peopleRepository.Exists(personId))
            {
                return TypedResults.NotFound();
            }

            var hobbies = await hobbiesRepository.GetAll(personId);

            var hobbiesDTO = hobbies.Select(h => new HobbyDTO
            {
                Id = h.Id,
                Title = h.Title,
                Description = h.Description,
                PersonId = personId,
                Links = h.Links

            }).ToList();

            return TypedResults.Ok(hobbiesDTO);

        }

        static async Task<Results<Ok<HobbyDTO>, NotFound>> GetById(int personId, int id,
            IHobbiesRepository hobbiesRepository, IPeopleRepository peopleRepository)
        {
            if (!await peopleRepository.Exists(personId))
            {
                return TypedResults.NotFound();
            }

            var hobby = await hobbiesRepository.GetById(id);

            if (hobby is null)
            {
                return TypedResults.NotFound();
            }

            var hobbyDTO = new HobbyDTO
            {
                Id = hobby.Id,
                Title = hobby.Title,
                Description = hobby.Description,
                PersonId = personId,
                Links = hobby.Links
            };

            return TypedResults.Ok(hobbyDTO);

        }


        static async Task<Results<Created<HobbyDTO>, NotFound>> Create(int personId, CreateHobbyDTO createHobbyDTO,
            IHobbiesRepository hobbiesRepository, IPeopleRepository peopleRepository,
            IOutputCacheStore outputCacheStore)
        {
            var person = await peopleRepository.Exists(personId);
            if (!person)
            {
                return TypedResults.NotFound();
            }

            var hobby = new Hobby
            {
                Title = createHobbyDTO.Title,
                Description = createHobbyDTO.Description,
                PersonId = personId,
            };

            var id = await hobbiesRepository.Create(hobby);

            var hobbyDTO = new HobbyDTO
            {
                Id = id,
                Title = hobby.Title,
                Description = hobby.Description,
                PersonId = personId
            };

            await outputCacheStore.EvictByTagAsync("hobbies-get", default);
            return TypedResults.Created($"/api/person/{personId}/hobby/{id}", hobbyDTO);
        }


        static async Task<Results<NoContent, NotFound>> Update(int personId, int id,
            CreateHobbyDTO createHobbyDTO, IHobbiesRepository hobbiesRepository,
            IPeopleRepository peopleRepository, IOutputCacheStore outputCacheStore)
        {
            if (!await peopleRepository.Exists(personId))
            {
                return TypedResults.NotFound();
            }

            var hobbyFromDb = await hobbiesRepository.GetById(id);

            if (hobbyFromDb is null)
            {
                return TypedResults.NotFound();
            }

            hobbyFromDb.Title = createHobbyDTO.Title;
            hobbyFromDb.Description = createHobbyDTO.Description;
            hobbyFromDb.PersonId = personId;

            await hobbiesRepository.Update(hobbyFromDb);
            await outputCacheStore.EvictByTagAsync("hobbies-get", default);

            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Delete(int personId, int id,
            IHobbiesRepository hobbiesRepository, IPeopleRepository peopleRepository,
            IOutputCacheStore outputCacheStore)
        {
            if (!await peopleRepository.Exists(personId))
            {
                return TypedResults.NotFound();
            }

            if (!await hobbiesRepository.Exists(id))
            {
                return TypedResults.NotFound();
            }

            await hobbiesRepository.Delete(id);
            await outputCacheStore.EvictByTagAsync("hobbies-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<Ok<List<HobbyDTO>>, NotFound>> GetByTitle([FromQuery] string title,
            IHobbiesRepository hobbiesRepository, IPeopleRepository peopleRepository)
        {
            var hobbies = await hobbiesRepository.GetByTitle(title);

            if (hobbies == null)
            {
                return TypedResults.NotFound();
            }

            var hobbiesDTO = hobbies.Select(h => new HobbyDTO
            {
                Id = h.Id,
                Title = h.Title,
                Description = h.Description,
                Links = h.Links,
                PersonId = h.PersonId

            }).ToList();


            return TypedResults.Ok(hobbiesDTO);

        }



    }
}
