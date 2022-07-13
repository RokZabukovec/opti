using Flock.Dtos;
using Flock.Models;
using Newtonsoft.Json;
using Spectre.Console;
using System.Text;
using Flock.Exceptions;

namespace Flock.Services
{
    public class Authentication : IAuthentication
    {
        private string AskForEmail()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]email[/]?")
                .PromptStyle("green"));
        }

        private string AskForPassword()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]?")
                .PromptStyle("red")
                .Secret());
        }

        public User AskForCredentials()
        {
            var user = new User();
            user.Email = AskForEmail();
            user.Password = AskForPassword();
            return user;
        }

        public DirectoryInfo CreateCredentialsDirectory()
        {
            var dirName = ".flock";
            var userLocation = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var location = Path.Combine(userLocation, dirName);
            if (!Directory.Exists(location))
            {
               return Directory.CreateDirectory(location);
            }
            return new DirectoryInfo(location);
        }

        public void CreateCredentialsFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        public bool PersistCredentials(UserDto user)
        {
            var dir = CreateCredentialsDirectory();
            var filePath = Path.Combine(dir.ToString(), "Flock.json");
            using (FileStream fs = File.Create(filePath))
            {
                try
                {
                    fs.Close();
                    string json = JsonConvert.SerializeObject(user);
                    File.WriteAllText(filePath, json);
                    return true;
                }
                catch (Exception)
                {
                    // Log
                    return false;
                }
            }
        }

        public UserDto ReadUserCredentials()
        {
            var dir = CreateCredentialsDirectory();
            var filePath = Path.Combine(dir.ToString(), "Flock.json");

            if (File.Exists(filePath) == false)
            {
                throw new UnauthorizedException();
            }
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize(file, typeof(UserDto)) as UserDto;
            }
        }

        FileStream IAuthentication.CreateCredentialsFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
