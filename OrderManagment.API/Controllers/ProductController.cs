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
    public class ProductController : ApiController
    {
        private OrderManagmentContext context;
        private IUnitOfWork _uow;
        private IRepository<Product> _productRepository;
        private IRepository<Category> _categoryRepository;

        public ProductController()
        {

            context = new OrderManagmentContext();
            _uow = new EFUnitOfWork(context);
            _productRepository = new EFRepository<Product>(context);
            _categoryRepository = new EFRepository<Category>(context);

        }
        [Route("product/getall")]
        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetAll();
        }
        [Route("product/createproduct")]
        public void Create(Product product)
        {
            _productRepository.Insert(product);
            _uow.SaveChanges();
        }
        [Route("product/updateproduct")]
        public void Update(Product product)
        {
            _productRepository.Update(product);
            _uow.SaveChanges();
        }
        [Route("product/GetProductById/{id}")]
        public Product GetProductById(int id)
        {
             return _productRepository.GetById(id);
        }
        [Route("product/delete/{id}")]
        public void Delete(int id)
        {
            _productRepository.Delete(id);
            _uow.SaveChanges();
        }

        [Route("product/getproductdatalist")]
        public IEnumerable<ProductDetailList> GetProductDataList()
        {
            var names = _productRepository.GetAll().ToList();
            var DataList = (from p in _productRepository.GetAll().ToList()
                            join c in _categoryRepository.GetAll().ToList() on p.CategoryID equals c.id
                            select new ProductDetailList
                            {
                                CategoryName = c.name,
                                id = p.id,
                                CreateDate = p.CreateDate,
                                Price = p.Price,
                                name = p.name
                            }).ToList();

            return DataList;
        }


    }
}
