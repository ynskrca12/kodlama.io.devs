using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entites;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TeknologyRepository : EfRepositoryBase<Teknology, BaseDbContext>, ITeknologyRepository
    {
        public TeknologyRepository(BaseDbContext context) : base(context)
        {
        }

    }
}
