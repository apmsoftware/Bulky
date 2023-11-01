using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;
        public ICategoryRepository categoryRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext db, ICategoryRepository categoryRepository) 
        {
            _db = db;
            this.categoryRepository = categoryRepository;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
