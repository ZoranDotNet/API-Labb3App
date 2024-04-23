using API_Labb3.DTOs;
using API_Labb3.Entity;
using API_Labb3.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace API_Labb3.Endpoints
{
    public static class LinksEndpoints
    {
        public static RouteGroupBuilder MapLinks(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("links-get"))
                .WithOpenApi(options =>
                {
                    options.Summary = "Get all links for a hobby";
                    return options;
                });
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create).WithOpenApi(options =>
            {
                options.Summary = "Assign link to a hobby";
                return options;
            });
            group.MapPut("/{id:int}", Update);
            group.MapDelete("/{id:int}", Delete);
            return group;
        }

        static async Task<Results<Created<LinkDTO>, NotFound>> Create(int hobbyId, CreateLinkDTO createLinkDTO,
            ILinksRepository linksRepository, IHobbiesRepository hobbiesRepository,
            IOutputCacheStore outputCacheStore)
        {
            var hobby = await hobbiesRepository.Exists(hobbyId);

            if (!hobby)
            {
                return TypedResults.NotFound();
            }

            var link = new Link
            {
                Url = createLinkDTO.Url,
            };

            link.HobbyId = hobbyId;

            var id = await linksRepository.Create(link);

            var linkDTO = new LinkDTO { Id = id, Url = link.Url, HobbyId = hobbyId };
            await outputCacheStore.EvictByTagAsync("links-get", default);
            return TypedResults.Created($"/api/hobby/{hobbyId}/links/{id}", linkDTO);
        }

        static async Task<Results<Ok<List<LinkDTO>>, NotFound>> GetAll(int hobbyId,
        ILinksRepository linksRepository, IHobbiesRepository hobbiesRepository)
        {
            if (!await hobbiesRepository.Exists(hobbyId))
            {
                return TypedResults.NotFound();
            }

            var links = await linksRepository.GetAll(hobbyId);
            var linksDTO = links.Select(l => new LinkDTO { Id = l.Id, Url = l.Url, HobbyId = hobbyId }).ToList(); ;
            return TypedResults.Ok(linksDTO);

        }

        static async Task<Results<Ok<LinkDTO>, NotFound>> GetById(int hobbyId, int id,
            ILinksRepository linksRepository, IHobbiesRepository hobbiesRepository)
        {
            if (!await hobbiesRepository.Exists(hobbyId))
            {
                return TypedResults.NotFound();
            }

            var link = await linksRepository.GetById(id);

            if (link == null)
            {
                return TypedResults.NotFound();
            }

            var linkDTO = new LinkDTO { Id = link.Id, Url = link.Url, HobbyId = hobbyId };
            return TypedResults.Ok(linkDTO);
        }

        static async Task<Results<NoContent, NotFound>> Update(int hobbyId, int id, CreateLinkDTO createLinkDTO,
            ILinksRepository linksRepository, IHobbiesRepository hobbiesRepository,
            IOutputCacheStore outputCacheStore)
        {
            if (!await hobbiesRepository.Exists(hobbyId))
            {
                return TypedResults.NotFound();
            }

            var fromDb = await linksRepository.GetById(id);

            if (fromDb == null)
            {
                return TypedResults.NotFound();
            }

            fromDb.Url = createLinkDTO.Url;

            await linksRepository.Update(fromDb);
            await outputCacheStore.EvictByTagAsync("links-get", default);

            return TypedResults.NoContent();

        }

        static async Task<Results<NoContent, NotFound>> Delete(int hobbyId, int id,
            ILinksRepository linksRepository, IHobbiesRepository hobbiesRepository,
            IOutputCacheStore outputCacheStore)
        {
            if (!await hobbiesRepository.Exists(hobbyId))
            {
                return TypedResults.NotFound();
            }

            var exist = await linksRepository.Exists(id);

            if (!exist)
            {
                return TypedResults.NotFound();
            }

            await linksRepository.Delete(id);
            await outputCacheStore.EvictByTagAsync("links-get", default);
            return TypedResults.NoContent();
        }
    }
}
