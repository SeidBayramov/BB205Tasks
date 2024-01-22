using FirsAPIproject.DAL;
using FirsAPIproject.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ColorController : ControllerBase
{
    private readonly AppDbContext _context;

    public ColorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Color> colors = _context.Colors.ToList();
        return StatusCode(200, colors);
    }

    [HttpGet("{id}")]
    public ActionResult<Color> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid id value");

        var color = _context.Colors.Find(id);

        if (color == null)
            return NotFound();

        return Ok(color);
    }

    [HttpPost]
    public ActionResult Create(Color color)
    {
        if (color == null || color.Id != 0)
            return BadRequest("Invalid color object");

        _context.Colors.Add(color);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Color color)
    {
        if (id <= 0 || color == null || color.Id != id)
            return BadRequest("Invalid id or color object");

        var existingColor = _context.Colors.Find(id);
        if (existingColor == null)
            return NotFound();

        existingColor.Name = color.Name;

        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid id value");

        var existingColor = _context.Colors.Find(id);
        if (existingColor == null)
            return NotFound();

        _context.Colors.Remove(existingColor);
        _context.SaveChanges();
        return Ok();
    }
}
