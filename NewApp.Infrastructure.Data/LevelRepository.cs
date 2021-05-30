using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp.Domain.Interfaces;
using NewApp.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Infrastructure.Data
{
    public class LevelRepository:ILevelRepository
    {
        private OnionAppContext db;
        public LevelRepository(OnionAppContext context)
        {
            db = new OnionAppContext();
        }
        public IEnumerable<Level> GetAll()
        {
            return db.Level;
        }
        public Level Get(int id)
        {
            return db.Level.Find(id);
        }
        public void Create(Level Level)
        {
            db.Level.Add(Level);
        }
        public void Update(Level Level)
        {
            db.Entry(Level).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Level Level = db.Level.Find(id);
            if (Level != null)
                db.Level.Remove(Level);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
