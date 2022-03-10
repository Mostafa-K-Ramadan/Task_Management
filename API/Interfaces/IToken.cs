using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace API.Services
{
    public interface IToken
    {
        public string CreateToken(AppUser user);
    }
}