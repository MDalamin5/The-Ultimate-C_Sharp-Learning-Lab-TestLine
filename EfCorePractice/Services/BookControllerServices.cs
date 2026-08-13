using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EfCorePractice.Interfaces;

namespace EfCorePractice.Services
{
    public class BookControllerServices: IBookServices
    {
        public async Task<string> GetAllBooks()
        {
            return "Data Data Return.";
        }

        public async Task<string> GetASingeBook(int id)
        {
            return "Book ONly";
        }

        public async Task<string> UpdateBook(int Id)
        {
            return "Update BOOk";
        }

        public async Task<bool> DeleteBook(int Id)
        {
            return true;
        }
    }
}