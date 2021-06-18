using MyEveryNote.Common;
using MyEveryNote.Core.DataAccess;
using MyEveryNote.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNote.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T:class
    {
        private DbSet<T> _obj;

        public Repository()
        {
            _obj = db.Set<T>();
        }
        
        public List<T> List()
        {
            return _obj.ToList();
        }

        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _obj.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _obj.Add(obj);
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.CreationOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername();
            }
            
            return Save();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername();
            }
            return Save();
        }

        public int Delete(T obj)
        {
            _obj.Remove(obj);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _obj.FirstOrDefault(where);
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        int IDataAccess<T>.Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ListQueryable()
        {
            return _obj.AsQueryable<T>();
        }
    }

    
}
