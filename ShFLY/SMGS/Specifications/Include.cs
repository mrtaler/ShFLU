using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using ShFLY.DataBase.DAL.Specifications.Interfaces;
using ShFLY.DataBase.Models;

namespace ShFLY.SMGS.Specifications
{
    public class IncludeForWagon : IIncludeSpecification<Wagon>
    {
       public IncludeForWagon(){
           this.AddInclude(p => p.Include("City"));
           this.AddInclude(p => p.Include("Events").Include("Tickets"));
           this.Includes = new List<Func<IQueryable<Wagon>, IQueryable<Wagon>>>();
       }

        public List<Func<IQueryable<Wagon>, IQueryable<Wagon>>> Includes { get;set; } 

        public void AddInclude(Func<IQueryable<Wagon>, IQueryable<Wagon>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }     
    }
}
