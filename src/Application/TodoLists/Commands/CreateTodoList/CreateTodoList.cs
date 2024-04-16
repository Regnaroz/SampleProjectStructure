using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();
       
        entity.Title = request.Title;
        entity.UserId = "3251b05f-f115-4ad5-85c8-9f5cdad308bb";
        _context.TodoLists.Add(entity);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception eee )
        {
            var t = eee;
            throw;
        }
        var test = _context.TodoLists.Include(c=>c.User)
                                     .FirstOrDefault(t => t.Title == request.Title);
        return entity.Id;
    }
}
