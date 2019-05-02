using OrderManagment.API.Data.Repositories;
using OrderManagment.API.Models;
using OrderManagment.API.UnitOfWork;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderManagment.API.Controllers
{
    public class CategoryController : ApiController
    {
        private OrderManagmentContext context;
        private IUnitOfWork _uow;
        private IRepository<Category> _categoryRepository;

        public CategoryController()
        {

            context = new OrderManagmentContext();
            _uow = new EFUnitOfWork(context);
            _categoryRepository = new EFRepository<Category>(context);

        }
        [Route("category/getall")]
        public IEnumerable<Category> GetCategorys()
        {
            return _categoryRepository.GetAll();
        }
        [Route("category/createcategory")]
        public void Create(Category Category)
        {
            _categoryRepository.Insert(Category);
            _uow.SaveChanges();
        }
        [Route("category/GetCategoryById/{id}")]
        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }
        [Route("category/updatecategory")]
        public void Update(Category Category)
        {
            _categoryRepository.Update(Category);
            _uow.SaveChanges();
        }
        [Route("category/delete/{id}")]
        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
            _uow.SaveChanges();
        }
    }
}
