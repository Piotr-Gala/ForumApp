namespace CLI.UI.ManageUsers;

using RepositoryContracts;

public class ManageUsersView
{
    private readonly IUserRepository _users;
    public ManageUsersView(IUserRepository users) => _users = users;


    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("\n-- Users --");
            Console.WriteLine("1) Create user");
            Console.WriteLine("2) List users");
            Console.WriteLine("B) Back");
            Console.Write("> ");
            var cmd = Console.ReadLine()?.Trim().ToLowerInvariant();

            switch (cmd)
            {
                case "1": await new CreateUserView(_users).ShowAsync(); break;
                case "2": new ListUsersView(_users).Show(); break;
                case "b": return;
                default: Console.WriteLine("Unknown."); break;
            }
        }
    }
}
