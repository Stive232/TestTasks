using System.Collections.Concurrent;
using TestProject.Repositories.Entities;

namespace TestProject.Repositories
{
    public class DocumentsData
    {
        private ulong _count;
        private object _locker = new();

        public ConcurrentDictionary<ulong, DbDocument> Data = new();

        public void Add(DbDocument document)
        {
            lock (_locker)
            {
                if (Data.TryAdd(_count, document))
                {
                    _count++;
                }
                else
                {
                    throw new Exception("Запись данных в базу прошла не успешно.");
                }
            }
        }
    }
}