namespace SampleProject.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}
