using Flock.Models;
using Flock.Responses;

namespace Flock.Services;

public interface ISearchService
{
    Task<CommandResponse> Search(string query);
    string ShowCommandSelectList(IEnumerable<Command> commands);
}