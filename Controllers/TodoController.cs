using Microsoft.AspNetCore.Mvc;
using test_ai.Models;

namespace test_ai.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static readonly List<TodoItem> _todos =
    [
        new TodoItem { Id = 1, Title = "Buy groceries", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Read a book", IsCompleted = true },
        new TodoItem { Id = 3, Title = "Write some code", IsCompleted = false },
    ];

    [HttpGet]
    [ProducesResponseType<IEnumerable<TodoItem>>(StatusCodes.Status200OK)]
    public IEnumerable<TodoItem> GetAll() => _todos;

    [HttpGet("{id:int}")]
    [ProducesResponseType<TodoItem>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [ProducesResponseType<TodoItem>(StatusCodes.Status201Created)]
    public IActionResult Create(TodoItem item)
    {
        item.Id = _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1;
        _todos.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, TodoItem updated)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item is null) return NotFound();

        item.Title = updated.Title;
        item.IsCompleted = updated.IsCompleted;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item is null) return NotFound();

        _todos.Remove(item);
        return NoContent();
    }
}
