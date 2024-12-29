using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Models;

namespace Training.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        //cream o noua metoda pentru schimbarea statusului (facem repository-ul mai dinamic)
        void UpdateStatus(int id,string status); //id-ul comenzii 
        
    }
}
