using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using opti.Dtos;
using opti.Models;

namespace opti.Services
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
