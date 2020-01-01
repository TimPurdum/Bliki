using System.Threading.Tasks;

namespace Bliki.Interfaces
{
    public interface IGitManager
    {
        Task Commit(string fileName, string? userName, bool delete = false);
        string? FetchCommitLog(string? fileName);
    }
}
