using Microsoft.EntityFrameworkCore;
using OurSpotKC_Blazor_Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurSpotKC_Blazor_Server.Data.Services
{
    public interface IResourceService
    {
        Task<List<Resource>> Get();
        Task<Resource> Get(int id);
        Task<Resource> Add(Resource resource);
        Task<Resource> Update(Resource resource);
        Task<Resource> Delete(int id);
    }
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _context;

        public ResourceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Resource>> Get()
        {
            List<Resource> resources = await _context.Resources.ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            resources.ForEach(x => x.Category = categories.SingleOrDefault(y => y.Id == x.CategoryId));
            return resources;
        }

        public async Task<Resource> Get(int id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task<Resource> Add(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<Resource> Update(Resource resource)
        {
            _context.Entry(resource).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<Resource> Delete(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return resource;
        }
    }
}
