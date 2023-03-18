using System.Collections.Concurrent;
using TestProject.Repositories.Entities;

namespace TestProject.Repositories
{
    public class DocumentsData
    {
        public ConcurrentDictionary<ulong, DbDocument> Data { get; private set; } = new();
    }
}