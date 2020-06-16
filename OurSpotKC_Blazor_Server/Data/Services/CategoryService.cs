using Microsoft.EntityFrameworkCore;
using OurSpotKC_Blazor_Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurSpotKC_Blazor_Server.Data.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> Get();
        Task<Category> Get(int id);
        Task<Category> Add(Category resource);
        Task<Category> Update(Category resource);
        Task<Category> Delete(int id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Get()
        {
            var categoryList = await _context.Categories.ToListAsync();
            if (categoryList.Exists(x => x.Name == string.Empty))
            {
                Category noCategory = categoryList.SingleOrDefault(x => x.Name == string.Empty);
                noCategory.Name = "No Category Set";

                categoryList = categoryList.Where(x => x.Name != string.Empty).ToList();
                categoryList.Append(noCategory);
            }
            return categoryList;
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> Add(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
