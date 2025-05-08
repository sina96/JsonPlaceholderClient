using JsonPlaceholderClient.Models;
using JsonPlaceholderClient.Services;
using JsonPlaceholderClient.Utilities;

internal class Program
{
    private static readonly PostService PostService = new();

    public static async Task Main()
    {
        try
        {
            while (true)
            {
                ConsoleUtil.WriteHeader("JSONPlaceholder Client");
                Console.WriteLine("1. List Posts");
                Console.WriteLine("2. Create Post");
                Console.WriteLine("3. Update Post");
                Console.WriteLine("4. Delete Post");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await DisplayPosts();
                        break;
                    case "2":
                        await CreatePost();
                        break;
                    case "3":
                        await UpdatePost();
                        break;
                    case "4":
                        await DeletePost();
                        break;
                    case "0":
                        ConsoleUtil.WriteSuccess("Goodbye!");
                        return;
                    default:
                        ConsoleUtil.WriteWarning("Invalid input. Please try again.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            ConsoleUtil.WriteError($"Unhandled error: {ex.Message}");
        }
    }

    private static async Task DisplayPosts()
    {
        try
        {
            ConsoleUtil.WriteHeader("Top 5 Posts");
            var posts = await PostService.GetAllPostsAsync();
            posts.Take(5).ToList().ForEach(p => Console.WriteLine($"{p.Id}: {p.Title}"));
        }
        catch (Exception ex)
        {
            ConsoleUtil.WriteError($"Error fetching posts: {ex.Message}");
        }
    }

    private static async Task CreatePost()
    {
        try
        {
            ConsoleUtil.WriteHeader("Create New Post");
            string title = ConsoleUtil.ReadRequired("Enter title: ");
            string body = ConsoleUtil.ReadRequired("Enter body: ");

            var newPost = new Post
            {
                Title = title,
                Body = body,
                UserId = 1
            };

            var result = await PostService.CreatePostAsync(newPost);
            ConsoleUtil.WriteSuccess($"Post created with ID: {result.Id}");
        }
        catch (Exception ex)
        {
            ConsoleUtil.WriteError($"Error creating post: {ex.Message}");
        }
    }

    private static async Task UpdatePost()
    {
        try
        {
            ConsoleUtil.WriteHeader("Update Post");

            int id = ConsoleUtil.ReadIntInRange("Enter ID of post to update (1–100): ", 1, 100);
            string title = ConsoleUtil.ReadRequired("New title: ");
            string body = ConsoleUtil.ReadRequired("New body: ");

            var updated = new Post
            {
                Id = id,
                Title = title,
                Body = body,
                UserId = 1
            };

            var result = await PostService.UpdatePostAsync(id, updated);
            ConsoleUtil.WriteSuccess($"Updated post {result.Id}: {result.Title}");
        }
        catch (Exception ex)
        {
            ConsoleUtil.WriteError($"Error updating post: {ex.Message}");
        }
    }

    private static async Task DeletePost()
    {
        try
        {
            ConsoleUtil.WriteHeader("Delete Post");
            int id = ConsoleUtil.ReadIntInRange("Enter ID of post to delete (1–100): ", 1, 100);

            await PostService.DeletePostAsync(id);
            ConsoleUtil.WriteSuccess($"Post {id} deleted (mock).");
        }
        catch (Exception ex)
        {
            ConsoleUtil.WriteError($"Error deleting post: {ex.Message}");
        }
    }
}