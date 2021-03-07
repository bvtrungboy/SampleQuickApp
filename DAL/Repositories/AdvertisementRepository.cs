using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(ApplicationDbContext context) : base(context)
        { }

    }
}
