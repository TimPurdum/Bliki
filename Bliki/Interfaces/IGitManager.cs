using System.Threading.Tasks;

namespace Bliki.Interfaces
{
    public interface IGitManager
    {
        Task Commit(string fileName);
    }
}
