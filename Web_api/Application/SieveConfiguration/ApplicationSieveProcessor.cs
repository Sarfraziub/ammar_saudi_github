﻿using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.SieveConfiguration
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {

        }
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.ApplyConfigurationsFromAssembly(typeof(ApplicationSieveProcessor).Assembly);

            return mapper;
        }
    }
}
