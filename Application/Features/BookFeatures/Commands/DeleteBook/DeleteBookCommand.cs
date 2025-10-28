using MediatR;

namespace Application.Features.BookFeatures.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteBookCommand(int id)
    {
        Id = id;
    }
}
