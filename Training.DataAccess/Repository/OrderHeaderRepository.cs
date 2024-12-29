using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly AppDbContext _db;

        public OrderHeaderRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader orderHeader)
        {
            //var objFromDb = _db.OrderHeader.FirstOrDefault(x => x.Id == orderHeader.Id);
            //objFromDb.Address = orderHeader.Address;
            //objFromDb.City = orderHeader.City;
            //objFromDb.State = orderHeader.State;
            //objFromDb.Comments = orderHeader.Comments;
            //objFromDb.PhoneNumber = orderHeader.PhoneNumber;
            _db.OrderHeader.Update(orderHeader);
        }

        public void UpdateStatus(int id, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.Status = status;//updatam statusul
            }
        }
    }
}
