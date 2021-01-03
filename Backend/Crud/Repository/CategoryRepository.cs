using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Threading.Tasks;

namespace Crud.Repository
{
    public class CategoryRepository: IDisposable
    {
        public Sample6Entities context = new Sample6Entities();

        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            var Category =  await Task.Run(() => context.ProductCategory.AsQueryable());
            var productCategoryList = await Task.Run(()=> context.ProductCategory.Include(m=>m.ProductTable));

            return Category.ToList(); 
        }

        public async Task CreateCategoryAsync()
        {
            ProductCategory productCategory = new ProductCategory();
            ProductTable product = new ProductTable(); 

            await Task.Run(()=> context.ProductTable.Add(product)); 
            await Task.Run(()=> context.ProductCategory.Add(productCategory));
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ProductCategory GetDetails(int? id)
        {
           

            return context.ProductCategory.Find(id);
        
        }


         public void EditCategory(ProductCategory productCategory)
        {
            
            context.Entry(productCategory).State = EntityState.Modified;
            context.SaveChanges();


        }

        public void DeleteCategory(ProductCategory productCategory)
        {
            context.ProductCategory.Remove(productCategory);
        
        }
       
       
    }
    
}