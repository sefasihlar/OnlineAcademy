﻿using NLayer.Core.Concrate;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class OptionRepository : GenericRepositoy<Option>, IOptionRepository
    {
        public OptionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
