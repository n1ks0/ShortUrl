using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Data;
using ShortUrl.Models;

namespace ShortUrl.Controllers
{
    public class UrlsController : Controller
    {
        private readonly ShortUrlContext _context;

        public UrlsController(ShortUrlContext context)
        {
            _context = context;
        }

        // GET: Urls
        public async Task<IActionResult> Index()
        {
            return View(await _context.Url.ToListAsync());
        }

        // GET: Urls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Url
                .FirstOrDefaultAsync(m => m.Id == id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        // GET: Urls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Urls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LongUrl,ShortUrl,CreationDate,Counter")] Url url)
        {
            

            if (ModelState.IsValid)
            {
                var d  = from _ in _context.Url where _.LongUrl == url.LongUrl select _;

                if (await d.AnyAsync())
                {
                    return View("Success", await d.FirstOrDefaultAsync());
                }

                var hash = ShortUrl.Models.Logic.Hash.Hasher.GetShortHash(url.LongUrl);
                url.ShortUrl = hash;
                url.Counter = 0;

                _context.Add(url);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return View("Success", url);
            }
            return View(url);
        }

        // GET: Urls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Url.FindAsync(id);
            if (url == null)
            {
                return NotFound();
            }
            return View(url);
        }

        // POST: Urls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LongUrl,ShortUrl,CreationDate,Counter")] Url url)
        {
            if (id != url.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(url);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrlExists(url.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(url);
        }

        // GET: Urls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Url
                .FirstOrDefaultAsync(m => m.Id == id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        // POST: Urls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var url = await _context.Url.FindAsync(id);
            _context.Url.Remove(url);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrlExists(int id)
        {
            return _context.Url.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ToUrl(string key)
        {
            var url = await (from d in _context.Url where d.ShortUrl == key select d).FirstOrDefaultAsync();

            if (url == null)
                return View("Create");

            url.Counter++;

            _context.Update(url);
            await _context.SaveChangesAsync();

            return Redirect(url.LongUrl);
        }
    }
}
