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
    public class MenteeRepository : IMenteeRepository
    {
        private OnionAppContext db;
        public MenteeRepository(OnionAppContext context)
        {
            this.db = new OnionAppContext();
        }
        public IQueryable<Mentee> GetSource()
        {
            return db.Mentee;
        }
        public IEnumerable<Mentee> GetAll()
        {
            return db.Mentee;
        }
        public Mentee Get(int id)
        {
            return db.Mentee.Find(id);
        }
        public void Create(Mentee Mentee)
        {
            db.Mentee.Add(Mentee);
        }
        public void Update(Mentee Mentee)
        {
            db.Entry(Mentee).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Mentee Mentee = db.Mentee.Find(id);
            if (Mentee != null)
                db.Mentee.Remove(Mentee);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
