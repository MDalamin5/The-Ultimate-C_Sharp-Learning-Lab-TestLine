using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCorePractice.Interfaces;

namespace EfCorePractice.Services
{
    public class BookControllerServices: IBookServices
    {
        public async Task<string> GetAllBook()
        {
            return "Data Data Return.";
        }
    }
}