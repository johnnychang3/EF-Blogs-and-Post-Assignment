using EF_Blogs_and_Post_Assignment.Models;
using Helper;
using System.Data;

namespace EF_Blogs_and_Post_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("------------------------");
                Console.WriteLine("     Blogs App Menu     ");
                Console.WriteLine("------------------------");
                var userInput = Input.GetIntWithPrompt("1. Display Blogs\n2. Add Blog\n3. Display Posts\n4. Add Post\n\nSelect an option (0 to exit): ", "Select a valid option.");
                switch (userInput)
                {
                    case 0: // Exit
                        exit = true;
                        break;

                    case 1: // Display Blogs
                        Console.Clear();
                        Console.WriteLine("------------------------");
                        Console.WriteLine("        Blog List       ");
                        Console.WriteLine("------------------------");

                        using (var context = new BlogContext())
                        {
                            var blogsList = context.Blogs.ToList();

                            foreach (var blog in blogsList)
                            {
                                Console.WriteLine($"{blog.BlogId}. {blog.Name}");
                            }
                            Console.WriteLine("\nPress Enter to exit.");
                            Console.ReadLine();
                        }
                        break;

                    case 2: // Add Blog
                        Console.Clear();
                        Console.WriteLine("------------------------");
                        Console.WriteLine("        Add Blog        ");
                        Console.WriteLine("------------------------");

                        using (var context = new BlogContext())
                        {
                            var blogName = Input.GetStringWithPrompt("Enter Blog Name: ", "Blog Name required!\nEnter Blog Name: ");

                            var blog = new Blog();
                            blog.Name = blogName;

                            context.Blogs.Add(blog);
                            context.SaveChanges();
                        }
                        break;

                    case 3: // Display Posts                        
                        bool pExit = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("------------------------");
                            Console.WriteLine("      Display Posts     ");
                            Console.WriteLine("------------------------");
                            var counter = 0;
                            using (var context = new BlogContext())
                            {
                                var blogsList = context.Blogs.ToList();
                                Console.WriteLine("0. Posts from all blogs");
                                foreach (var blog in blogsList)
                                {
                                    counter++;
                                    Console.WriteLine($"{blog.BlogId}. Posts from {blog.Name}");
                                }
                            }

                            userInput = Input.GetIntWithPrompt("\nSelect an option: ", "Please select a valid option.");
                            if (userInput > counter)
                            {
                                Console.Clear();
                                Console.WriteLine("------------------------");
                                Console.WriteLine("      Display Posts     ");
                                Console.WriteLine("------------------------");

                                Console.WriteLine("Please select a valid option.");
                                Console.WriteLine("Press Enter.");
                                Console.ReadLine();
                            }
                            else if (userInput == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("------------------------");
                                Console.WriteLine("      Display Posts     ");
                                Console.WriteLine("------------------------");
                                using (var context = new BlogContext())
                                {
                                    var postsList = context.Posts.ToList();
                                    Console.WriteLine($"\nTotal # of Posts: {postsList.Count()}");
                                    foreach (var post in postsList)
                                    {
                                        Console.WriteLine($"\nBlog: {post.Blog.Name}");
                                        Console.WriteLine($"Title: {post.Title}");
                                        Console.WriteLine($"Content: {post.Content}");
                                    }
                                }
                                Console.WriteLine("\nPress Enter to exit.");
                                Console.ReadLine();
                                pExit = true;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("------------------------");
                                Console.WriteLine("      Display Posts     ");
                                Console.WriteLine("------------------------");
                                using (var context = new BlogContext())
                                {
                                    var postsList = context.Posts.Where(p => p.BlogId.Equals(userInput)).ToList();
                                    Console.WriteLine($"\nTotal # of Posts: {postsList.Count()}");
                                    foreach (var post in postsList)
                                    {
                                        Console.WriteLine($"\nBlog: {post.Blog.Name}");
                                        Console.WriteLine($"Title: {post.Title}");
                                        Console.WriteLine($"Content: {post.Content}");
                                    }
                                }
                                Console.WriteLine("\nPress Enter to exit.");
                                Console.ReadLine();
                                pExit= true;
                            }
                        } while(!pExit);

                        break;

                    case 4: // Add Post
                        bool addPExit = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("------------------------");
                            Console.WriteLine("        Add Post        ");
                            Console.WriteLine("------------------------");
                            var counter = 0;

                            using (var context = new BlogContext())
                            {
                                var blogsList = context.Blogs.ToList();

                                foreach (var blog in blogsList)
                                {
                                    counter++;
                                    Console.WriteLine($"{blog.BlogId}. {blog.Name}");
                                }
                            }

                            userInput = Input.GetIntWithPrompt("\nSelect a blog to post to: ", "Please select a valid option.");
                            if (userInput > counter || userInput == 0)
                            {
                                Console.WriteLine("Please select a valid option.");
                                Console.WriteLine("Press Enter.");
                                Console.ReadLine();
                            }
                            else
                            {
                                using (var context = new BlogContext())
                                {
                                    var title = Input.GetStringWithPrompt("Enter post title: ", "Post title required!");

                                    var content = Input.GetStringWithPrompt("Enter post content: ", "Post content required!");

                                    var post = new Post();
                                    post.Title = title;
                                    post.Content = content;
                                    post.BlogId = userInput;

                                    context.Posts.Add(post);
                                    context.SaveChanges();
                                }
                                Console.WriteLine("\nPress Enter to exit.");
                                Console.ReadLine();
                                addPExit = true;
                            }
                        } while (!addPExit);

                        break;

                    default: // Invalid Option
                        Console.Clear();
                        Console.WriteLine("Select a valid option.");
                        Console.WriteLine("Press Enter.");
                        Console.ReadLine();
                        break;

                }
            }
            while (!exit);
        }
    }
}