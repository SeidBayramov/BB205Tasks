using DianaTemp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SliderViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public SliderViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int key=1)
    {
        List<Slider> sliders = await _context.Sliders.Take(2).ToListAsync();
        switch (key)
        {
            case 1:
                sliders = _context.Sliders.OrderBy(p => p.Id).ToList();
                break;
      
      
            default:
                sliders = _context.Sliders.OrderByDescending(p => p.Id).ToList();
                break;
        }

                return View(sliders);
    }
}
