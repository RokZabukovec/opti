using Flock.Dtos;
using Flock.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flock.Services
{
    public interface IAuthentication
    {
        User AskForCredentials();
        DirectoryInfo CreateCredentialsDirectory();
        FileStream CreateCredentialsFile(string filePath);
        bool PersistCredentials(UserDto user);
        public UserDto? ReadUserCredentials();
    }
}
