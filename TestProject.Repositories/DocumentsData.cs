using System.Collections.Concurrent;
using TestProject.Repositories.Entities;

namespace TestProject.Repositories
{
    public class DocumentsData
    {

        public ConcurrentBag<DbDocument> Data = new();

        public void Add(DbDocument document)
        {
            Data.Add(document);
        }

        //public void Add(DbDocument document)
        //{
        //    if(Data.TryAdd(_count, document))
        //    {
        //        _count++;
        //    }
        //    else 
        //    {
        //        throw new Exception("Запись данных в базу прошла не успешно.");
        //    }
        //}

    }
}