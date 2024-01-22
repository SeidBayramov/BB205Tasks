using Agency.Busines.Helpers;
using Agency.Busines.Services.Interface;
using Agency.Busines.ViewModel.Portfolio;
using Agency.Core.Entities;
using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Agency.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class PortFolioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPortfolioService _service;
        IWebHostEnvironment _env { get; set; }

        public PortFolioController(IMapper mapper, IPortfolioService service, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _service = service;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Portfolio> port = await _service.PortfolioGetAllAsync();
            return View(port);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PortfolioCreateVm portfolioCreate)
        {
            if (!ModelState.IsValid)
            {
                return View(portfolioCreate);
            }
            if (!portfolioCreate.ImageFile.CheckContent("image/"))
            {
                ModelState.AddModelError("Image", "Duzgun format daxil edin");
                return View();
            }

            Portfolio portfolio = new Portfolio()
            {
                Title = portfolioCreate.Title,
                SubTitle = portfolioCreate.SubTitle,
                ImageUrl = portfolioCreate.ImageFile.Upload(_env.WebRootPath, "/Upload/Project/"),
                CreateAt = DateTime.Now,
            };
            
            await _service.CreateAsync(portfolioCreate);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Portfolio port = await _service.PortGetAsync(id);
            PortfolioUpdateVm portfolio = _mapper.Map<PortfolioUpdateVm>(port);
            return View(portfolio);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PortfolioUpdateVm portfolioUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(portfolioUpdate);
            }
            Portfolio portfolio = new Portfolio()
            {
                Title = portfolioUpdate.Title,
                SubTitle = portfolioUpdate.SubTitle,
                ImageUrl = portfolioUpdate.ImageFile.Upload(_env.WebRootPath, "/Upload/Project/"),
                UpdateAt = DateTime.Now,
            };

            await _service.UpdateAsync(portfolioUpdate);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);


            return RedirectToAction("Index");
        }

    }

}

