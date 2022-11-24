using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Dtos;
using MovieRental.Models;

namespace MovieRental.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CustomerController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers(string? query = null)
        {
            var customersQuery = _db.Customers.Include(m => m.MembershipType).AsQueryable();
            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(q => q.Name.Contains(query));
            }
            var customerDtos = customersQuery
                            .ToList()
                            .Select(_mapper.Map<Customer, CustomerDto>);

            return (customerDtos);

            //return _db.Customers.Include(m => m.MembershipType)
            //    .ToList().Select(_mapper.Map<Customer, CustomerDto>);
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var customer = _db.Customers.Include(g => g.MembershipType).FirstOrDefault(m => m.Id == id);
            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _db.Customers.Add(customer);
            _db.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customerDto.Id }, customerDto);
        }

        [HttpPut("{id:int}", Name = "UpdateCustomer")]
        public ActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _db.Customers.FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _mapper.Map(customerDto, customer);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteCustomer")]
        public ActionResult DeleteCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var customer = _db.Customers.FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(customer);
            _db.SaveChanges();

            return NoContent();
        }
    }
}