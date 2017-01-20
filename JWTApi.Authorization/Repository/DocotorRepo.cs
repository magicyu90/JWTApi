using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using JWTApi.Authorization.Models;

namespace JWTApi.Authorization.Repository
{
    public class DocotorRepo
    {

        private static readonly iHealthProContext _context;

        static DocotorRepo()
        {
            _context= new iHealthProContext();
        }

        public static async Task<DoctorAccount> GetDoctorAccountAsync(string email)
        {
            return await _context.DoctorAccounts.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}