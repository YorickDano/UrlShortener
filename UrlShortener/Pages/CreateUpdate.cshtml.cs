using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Pages
{
    public class CreateUpdateModel : PageModel
    {
        private readonly Context _context;
        private readonly UrlShorter _shorter;
        private readonly DataValidator _dataValidator;

        public UrlModel? UpdateModel;

        public CreateUpdateModel(Context context, UrlShorter shorter,
             DataValidator dataValidator)
        {
            _context = context;
            _shorter = shorter;
            _dataValidator = dataValidator;
        }

        public async Task<IActionResult> OnGetAsync(int? updateId)
        {
            if (updateId != null)
            {
                UpdateModel = await _context.Urls.FirstOrDefaultAsync(x => x.Id == updateId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(string longUrl)
        {
            if (!ValidateAndUpdateError(longUrl, _dataValidator.IsUrlValid, "Incorrect Url"))
            {
                return Page();
            }

            var urlModel = new UrlModel();
            urlModel.LongUrl = longUrl;
            urlModel.ShortUrl = _shorter.GetSortUrl();
            urlModel.CreationDate = DateTime.Now;

            await _context.Urls.AddAsync(urlModel);
            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(UrlModel updateModel)
        {
            if (!ValidateAndUpdateError(updateModel.LongUrl, _dataValidator.IsUrlValid, "Incorrect Url") 
                || !ValidateAndUpdateError(updateModel.ShortUrl, _dataValidator.IsShortUrlValid, "Incorrect Short Url or its not start with https://localhost:7230/")
                || !ValidateAndUpdateError(updateModel.CreationDate, _dataValidator.IsDateValid, "Incorrect Date")
                || !ValidateAndUpdateError(updateModel.TransitionsNumber, _dataValidator.IsTransitionsNumberValid, "Incorrect Transitions Number"))
            {
                UpdateModel = new UrlModel();
                UpdateModel.CopyFrom(updateModel);
                return Page();
            }

            if (_context.Urls.Any(x => (updateModel.Id != x.Id) && (x.ShortUrl == updateModel.ShortUrl)))
            {
                TempData["Error"] = "There are already such short url";
                UpdateModel = new UrlModel();
                UpdateModel.CopyFrom(updateModel);
                return Page();
            }

            _context.Urls.Update(updateModel);
            await _context.SaveChangesAsync();

            return Redirect("/");
        }

        private bool ValidateAndUpdateError<T>(T propertyValue, Func<T, bool> validationFunc, string errorMessage)
        {
            if (!validationFunc(propertyValue))
            {
                TempData["Error"] = errorMessage;
                return false;
            }

            return true;
        }
    }
}
