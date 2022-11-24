using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Models;
using MovieRental.ViewModels;

namespace MovieRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var customer = _db.Customers.Include(c=>c.MembershipType).ToList();
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var customer = _db.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);        }

        [HttpGet]
        public IActionResult Create()
        {
            var membershipTypes = _db.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("Upsert", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var vieModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _db.MembershipTypes.ToList()
                };

                return View("Upsert", vieModel);
            }

            if (customer.Id == 0)
                _db.Customers.Add(customer);
            else
            {
                var customerInDb = _db.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == id);
            
            if(customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _db.MembershipTypes.ToList()
            };

            return View("Upsert", viewModel);
        }
    }
}
