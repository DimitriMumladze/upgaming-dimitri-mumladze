using MediatR;

namespace Application.Features.AuthorFeatures.Commands.DeleteAuthor;

public class DeleteAuthorCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteAuthorCommand(int id)
    {
        Id = id;
    }
}
