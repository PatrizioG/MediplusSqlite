using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Piranha;
using Piranha.AspNetCore.Services;
using Piranha.Mailer.Interfaces;
using PiranhaMVC.Models;
using TryPiranha.Models;

namespace TryPiranha.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CmsController : Controller
    {
        private readonly IApi _api;
        private readonly IModelLoader _loader;
        private readonly IMailer _mailer;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public CmsController(IApi api, IModelLoader loader, IMailer mailer, IConfiguration configuration)
        {
            _api = api;
            _loader = loader;
            _mailer = mailer;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("page")]
        public async Task<IActionResult> Page(Guid id, bool draft = false)
        {
            try
            {
                var model = await _loader.GetPageAsync<StandardPage>(id, HttpContext.User, draft);

                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    DepartmentPage model2 = await _loader.GetPageAsync<DepartmentPage>(id, HttpContext.User, draft);
                    return View("DepartmentPage", model2);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Form([Bind(Prefix = "block.ContactForm")] ContactFormDTO contactForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*
                     * Email per l'owner del sito
                     */
                    await _mailer.SendEmailAsync(_configuration["ContactForm:ContactRequestEmail"], $"Richiesta di contatto ricevuta dal sito",
    @$"
Nuovo contatto ricevuto<br>
Nome: {contactForm.Name}<br>
Email: {contactForm.Email}<br>
Messagio: {contactForm.Message}
");
                    /*
                     * Email per l'utilizzatore del sito
                     */
                    await _mailer.SendEmailAsync(contactForm.Email,
                        _configuration["ContactForm:ContactRequestSubject"],
                        _configuration["ContactForm:ContactRequestBody"]);

                    return new RedirectResult(Url.Action(nameof(FormSubmitted)) + "#formsubmitted");
                }
                else
                {
                    return new RedirectResult(Url.Action(nameof(FormError)) + "#formerror");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return new RedirectResult(Url.Action(nameof(FormError)) + "#formerror");
            }
        }

        public IActionResult StatusCode(int code)
        {
            return View(code);
        }

        public IActionResult FormSubmitted()
        {
            return View();
        }

        public IActionResult FormError()
        {
            return View();
        }
    }
}
