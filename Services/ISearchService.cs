using opti.Models;
using opti.Responses;

namespace opti.Services;

public interface ISearchService
{
    Task<CommandResponse> Search(string query);
    string ShowCommandSelectList(IEnumerable<Command> commands);
}