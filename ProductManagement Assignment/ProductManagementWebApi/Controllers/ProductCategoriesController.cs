using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductManagementWebApi.Models;
using ProductManagementWebApi.Security;

namespace ProductManagementWebApi.Controllers
{
    [BasicAuthentication]
    public class ProductCategoriesController : ApiController
    {
        
        private readonly DbProductManagementEntities db = new DbProductManagementEntities();

        //Disabled ProxyCreationEnabled in DbProductManagementEntities(Context) Class  Date: 6-1-2020
        //public ProductCategoriesController()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //}

        // GET: api/ProductCategories
        public IQueryable<ProductCategory> GetProductCategories()
        {
            
            return db.ProductCategories;
        }

        // GET: api/ProductCategories/5
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetProductCategory(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        // PUT: api/ProductCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductCategory(int id, ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(productCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductCategories
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult PostProductCategory(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductCategories.Add(productCategory);
            try{
                db.SaveChanges();
            }catch (Exception E)
            {
                return BadRequest(E.Message);
            }

            return CreatedAtRoute("DefaultApi", new { id = productCategory.Id }, productCategory);
        }

        // DELETE: api/ProductCategories/5
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult DeleteProductCategory(int id)
        {
            try
            {
                ProductCategory productCategory = db.ProductCategories.Find(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                db.ProductCategories.Remove(productCategory);
                db.SaveChanges();

                return Ok(productCategory);
            }catch(Exception E)
            {
                return BadRequest("There are Products With This Category,Before Deleting Category Change/Delete Those Products!");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductCategoryExists(int id)
        {
            return db.ProductCategories.Count(e => e.Id == id) > 0;
        }
    }
}