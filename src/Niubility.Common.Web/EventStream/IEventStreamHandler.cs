using System.Threading.Tasks;

namespace Niubility.Common.Web
{
    public interface IEventStreamHandler
    {
        bool IsCompleted { get; }

        ValueTask<string> Next();
    }
}