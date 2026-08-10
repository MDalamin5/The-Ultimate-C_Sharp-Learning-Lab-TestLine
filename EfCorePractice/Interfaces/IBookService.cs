using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCorePractice.Interfaces
{
    public interface IBookServices
    {
        Task<string> GetAllBooks();
        Task<string> GetASingeBook(int Id);
        Task<string> UpdateBook(int Id);
        Task<bool> DeleteBook(int Id);
    }
}