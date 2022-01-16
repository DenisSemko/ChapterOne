﻿using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        public new Task<User> GetById(Guid id);
    }
}
