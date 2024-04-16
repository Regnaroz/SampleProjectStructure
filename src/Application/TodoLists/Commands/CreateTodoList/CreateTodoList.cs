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
        entity.UserId = "e59f6dac-feeb-4313-9ed9-d21310d63000";
        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        var test = _context.TodoLists.Include(c=>c.User)
                                     .FirstOrDefault(t => t.Title == request.Title);
        return entity.Id;
    }
}
