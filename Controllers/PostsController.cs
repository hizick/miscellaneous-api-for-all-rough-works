using BookAppAPI.Model;
using BookAppAPI.Model.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookAppAPI.Controllers
{
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly List<Post> posts;

        //private readonly AppDbContext context;

        public PostsController(/*AppDbContext context*/)
        {
            //this.context = context;
            posts = Task.Run(async () => await readJsonFromLocal()).Result;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var posts = await context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpPost]
        public Post CreatePost([FromBody] Post post)
        {
            //context.Add(post);
            //var rows = await context.SaveChangesAsync();
            var prevPost = posts.Last().Id + 1;
            post.Id = prevPost;
            posts.Add(post);
            writeToLocalJson(posts);

            return post;
        }

        [HttpGet("{userId}")]
        public IActionResult GetPostByUser(int userId)
        {
            //var userGuid = Guid.Parse(userId);
            //var post = await context.Posts.Where(x => x.UserId == userGuid).ToListAsync();
            var post = posts.Where(x => x.UserId == userId).ToList();
            return Ok(post);
        }

        [HttpDelete("{Id}/{userId}")]
        public IActionResult DeletePostByUser(int Id, int userId)
        {
            //var userGuid = Guid.Parse(userId);
            //var post = await context.Posts.Where(x => x.UserId == userGuid).ToListAsync();
            var post = posts.FirstOrDefault(x => x.Id == Id && x.UserId == userId);
            if (post is null)
                return NotFound();
            posts.Remove(post);
            return Ok();
        }

        private static async Task<List<Post>> readJsonFromLocal()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Model", "Data", "Post.json");
            string jsonString = await System.IO.File.ReadAllTextAsync(path);
            var posts = JsonConvert.DeserializeObject<List<Post>>(jsonString);
            return posts;
        }

        private static async void writeToLocalJson(List<Post> posts)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Model", "Data", "Post.json");
            var postsToAdd = JsonConvert.SerializeObject(posts, Formatting.Indented);

            await System.IO.File.WriteAllTextAsync(path, postsToAdd);

            //string jsonString = await System.IO.File.ReadAllTextAsync(path);
            //var postNew = JsonConvert.DeserializeObject<List<Post>>(jsonString);
            //return postNew; 
        }
    }
}
