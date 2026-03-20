using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_ai.Data;
using test_ai.Models;

namespace test_ai.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<TodoItem>>(StatusCodes.Status200OK)]
    public async Task<IEnumerable<TodoItem>> GetAll() =>
        await db.Todos.ToListAsync();

    [HttpGet("{id:int}")]
    [ProducesResponseType<TodoItem>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await db.Todos.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [ProducesResponseType<TodoItem>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(TodoItem item)
    {
        db.Todos.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, TodoItem updated)
    {
        var item = await db.Todos.FindAsync(id);
        if (item is null) return NotFound();

        item.Title = updated.Title;
        item.IsCompleted = updated.IsCompleted;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Todos.FindAsync(id);
        if (item is null) return NotFound();

        db.Todos.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
