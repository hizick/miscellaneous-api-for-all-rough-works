using BookAppAPI.Model;
using BookAppAPI.Model.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookAppAPI.Controllers
{
    [Route("api/Users")]
    public class UsersController: Controller
    {
        //private readonly AppDbContext context;
        private readonly DateTime dateNow = DateTime.Now;
        private readonly List<User> users;// = await readJsonFromLocal();
        public UsersController(/*AppDbContext context*/)
        {
            //this.context = context;
            users = Task.Run(async () => await readJsonFromLocal()).Result;
        }

        [HttpPost("/api/Users/Register")]
        public string Register([FromBody] User user)
        {
            string result;
            var prevUser = users.Last().UserId + 1;
            user.UserId = prevUser;
            user.DateCreated = dateNow;

            if (users.Any<User>(x => x.UserId == user.UserId))
                result = string.Format("user {0} already exists", user.Username);
            users.Add(user);
            writeToLocalJson(users);

            if (!users.Contains(user))
                result = "unknown error";
            result = "success";

            return result;
            //await context.AddAsync(user);
            //int row = await context.SaveChangesAsync();
            //return row;
        }

        [HttpPost("/api/Users/Login")]
        public IActionResult Login([FromBody] User user)
        {
            //var login = context.Users.FirstOrDefault(u => u.Username == user.Username
            //                && u.Password == user.Password);
            //if (login is null)
            //    return NotFound();
            var login = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (login is null)
                return NotFound();
            return Ok(login);
        }

        /*
         this is what you want to do:
        1. you want to read from the json file here
        2. initially there is nothing on the database, so if there is nothing there, you add something
        3. read and write to it according to what you want
         */
        private static async Task<List<User>> readJsonFromLocal()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Model", "Data", "User.json");
            string jsonString = await System.IO.File.ReadAllTextAsync(path);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return users;
        }

        private static async void writeToLocalJson(List<User> users)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Model", "Data", "User.json");
            var userToAdd = JsonConvert.SerializeObject(users, Formatting.Indented);

            await System.IO.File.WriteAllTextAsync(path, userToAdd);

            //string jsonString = await System.IO.File.ReadAllTextAsync(path);
            //var postNew = JsonConvert.DeserializeObject<List<Post>>(jsonString);
            //return postNew;
        }
    }
}
