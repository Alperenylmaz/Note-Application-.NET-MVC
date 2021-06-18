using MyEveryNote.DataAccessLayer;

namespace MyEveryNote.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lockSync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        public static void CreateContext()
        {
            if (db == null)
            {
                lock (_lockSync)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();

                    }
                }
            }


        }
    }
}
