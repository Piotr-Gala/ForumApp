using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> posts = new();

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        var existing = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existing is null) throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        posts.Remove(existing);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var existing = posts.SingleOrDefault(p => p.Id == id);
        if (existing is null) throw new InvalidOperationException($"Post with ID '{id}' not found");
        posts.Remove(existing);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        var existing = posts.SingleOrDefault(p => p.Id == id);
        if (existing is null) throw new InvalidOperationException($"Post with ID '{id}' not found");
        return Task.FromResult(existing);
    }

    public IQueryable<Post> GetManyAsync() => posts.AsQueryable();
}
