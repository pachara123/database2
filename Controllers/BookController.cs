using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using databasr2.Data;
using databasr2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace databasr2.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.books.ToListAsync();
            return View(model);
        }
        [Authorize(Roles = "User")]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddBook(book model)
        {
            if (ModelState.IsValid)
            {
                var oldBook = await _context.books.AnyAsync(b => b.Name == model.Name);
                if (oldBook)
                {
                    return View();
                }
                else
                {
                    _context.books.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
            
        }
        [Authorize]
        public async Task<IActionResult> BookEdit(int Id)
        {
            var book1 = await _context.books.FirstOrDefaultAsync(prayuth => prayuth.Id == Id);
            if (book1 == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(book1);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditBook(book model)
        {
            if (ModelState.IsValid)
            {
                var book1 = await _context.books.FirstOrDefaultAsync(prayuth => prayuth.Id == model.Id);
                if (book1 != null)
                {
                    book1.Name = model.Name;
                    book1.Price = model.Price;
                    book1.BookType = model.BookType;

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(model);   
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> BookDelete(int Id)
        {
            var book1 = await _context.books.FirstOrDefaultAsync(prayuth => prayuth.Id == Id);
            if (book1 != null)
            {
             
                _context.books.Remove(book1);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }
    }
}