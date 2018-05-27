using BizDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BizDataLibrary.Repositories
{
    public class BizRepository<T> where T:class
    {//    抽象類別
        // 因為 BizRepository 是抽象類別，建構式請宣告為 protected
        private BizModel _context;

        protected BizModel Context
        { get { return _context; } }

        // 抽象類別的建構式絕對不會是 public
        public BizRepository(BizModel context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Updata(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}