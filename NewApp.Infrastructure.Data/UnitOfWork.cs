using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Infrastructure.Data
{
    public class UnitOfWork //: //IDisposable
    {
        private OnionAppContext db; //= new OnionAppContext();
        private MenteeRepository menteerep;
        private LevelRepository levelrep;
        public UnitOfWork()
        {
            db = new OnionAppContext();
        }
        public MenteeRepository Mentee
        {
            get
            {
                if (menteerep == null)
                    menteerep = new MenteeRepository(db);
                return menteerep;
            }
        }
        public LevelRepository Level
        {
            get
            {
                if (levelrep == null)
                    levelrep = new LevelRepository(db);
                return levelrep;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        //private bool disposed = false;
        //public virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        this.disposed = true;
        //    }
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
