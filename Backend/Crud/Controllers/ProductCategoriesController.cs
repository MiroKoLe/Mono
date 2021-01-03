using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Crud.Models;
using Crud.Repository;
using Crud.Service;
using Crud.UOW;

namespace Crud.Controllers
{
    public class ProductCategoriesController : Controller
    {
        public static Sample6Entities context = new Sample6Entities();

        CategoryRepository categoryRepository = new CategoryRepository();
        CategoryService categoryService = new CategoryService(); 
        UnitOfWork unitOfWork = new UnitOfWork(context); 
        
        public ProductCategoriesController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
       
        

        // GET: ProductCategories
        public ActionResult Index()
        {
            var Category = unitOfWork.CategoryRepository.Get(includeProperties: "ProductTable"); 

            List<ProductCategoryRestModel> productCategoryRests = new List<ProductCategoryRestModel>();

            foreach (var category in Category)
            {
                ProductCategoryRestModel productCategoryRest = new ProductCategoryRestModel();

                productCategoryRest.ProductId = category.ProductId;
                productCategoryRest.Name = category.Name;
               


                foreach (var product in category.ProductTable)
                {
                    productCategoryRest.ProductName = productCategoryRest.ProductName + product.Name + ", ";

                }

                productCategoryRests.Add(productCategoryRest);

            }


            return View(productCategoryRests);

            
        }

        // GET: ProductCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = unitOfWork.CategoryRepository.GetDetails(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name")] ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CategoryRepository.Create(productCategory);

                    return RedirectToAction("Index");
                }               
            }
            catch (DataException)
            {
             ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
           

            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = categoryService.GetDetailForCategory(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name, ProductTable, IsActive")] ProductCategory productCategory)
        {

            if (ModelState.IsValid)
            {
                
                    categoryService.categoryEdit(productCategory);
                    return RedirectToAction("Index");

               
            }
            
         
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             ProductCategory productCategory = unitOfWork.CategoryRepository.GetDetails(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ProductCategory productCategory)
        {

            unitOfWork.CategoryRepository.GetDetails(productCategory);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CreateCategoryWithProduct()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategoryWithProduct([Bind(Include = "Name, ProductName")] ProductCategoryRestModel productCategoryRest)
        {
            ProductCategory productCategory = new ProductCategory();

     
                ProductTable product = new ProductTable();

                productCategory.Name = productCategoryRest.Name;
                product.Name = productCategoryRest.ProductName;


            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CategoryRepository.Create(productCategory); 
                    product.ProductCategoryId = productCategory.ProductId; 

                    unitOfWork.ProductRepository.Create(product);
                    var unitCommit = unitOfWork.Commit(); 

                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
        

            ViewBag.ProductCategoryId = new SelectList(context.ProductCategory, "Name", "ProductName", product.ProductCategoryId);

            return View(productCategoryRest);
        }

    
    }


}
