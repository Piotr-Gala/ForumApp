using System;
using RepositoryContracts;

namespace InMemory;

public class InMemoryComment : CommentInterface
{
    public List<Comment>? comments { get; set;}
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
        ? comments.Max(p => p.Id) + 1
        : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        return Task.CompletedTask;;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? givenComment = comments.SingleOrDefault(p => p.Id == id);
        if (givenComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        return Task.FromResult(givenComment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id} ain't found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }
}
