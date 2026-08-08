using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCorePractice.Interfaces
{
    public interface IBookServices
    {
        Task<string> GetAllBooks();
        //define all CRUD api
    }
}