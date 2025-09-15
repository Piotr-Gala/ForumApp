namespace CLI.UI.ManagePosts;

using RepositoryContracts;

public class ManagePostsView
{
    private readonly IPostRepository _posts;
    private readonly ICommentRepository _comments;
    private readonly IUserRepository _users;

    public ManagePostsView(IPostRepository posts, ICommentRepository comments, IUserRepository users)
    { _posts = posts; _comments = comments; _users = users; }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("\n-- Posts --");
            Console.WriteLine("1) Create post");
            Console.WriteLine("2) List posts");
            Console.WriteLine("3) View single post");
            Console.WriteLine("4) Add comment to post");
            Console.WriteLine("B) Back");
            Console.Write("> ");
            var cmd = Console.ReadLine()?.Trim().ToLowerInvariant();

            switch (cmd)
            {
                case "1": await new CreatePostView(_posts, _users).ShowAsync(); break;
                case "2": new ListPostsView(_posts).Show(); break;
                case "3": await new SinglePostView(_posts, _comments).ShowAsync(); break;
                case "4": await new AddCommentView(_comments, _posts, _users).ShowAsync(); break;
                case "b": return;
                default: Console.WriteLine("Unknown."); break;
            }
        }
    }
}
