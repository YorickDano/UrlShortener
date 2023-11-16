using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Context _context;

        public IEnumerable<UrlModel> Urls { get; set; }

        public IndexModel(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string shortUrn)
        {
            if (shortUrn != null)
            {
                var urlModel = await _context.Urls.FirstOrDefaultAsync(x => x.ShortUrl.Contains(shortUrn));

                if (urlModel == null)
                {
                    Urls = _context.Urls.Select(x => x);
                    return Page();
                }

                ++urlModel.TransitionsNumber;

                _context.Urls.Update(urlModel);
                await _context.SaveChangesAsync();

                return Redirect(urlModel.LongUrl);
            }

            Urls = _context.Urls.Select(x => x);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var urlToDelete = _context.Urls.FirstOrDefault(x => x.Id == id);

            if (urlToDelete == null)
            {
                return NotFound();
            }

            _context.Remove(urlToDelete);
            await _context.SaveChangesAsync();

            return Redirect("/");
        }
    }
}