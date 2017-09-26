using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.DataBase.DAL.Specifications.Interfaces;
using ShFLY.DataBase.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ShFLY.SMGS.Specifications
{
    public class IncludeForWagon : IIncludeSpecification<Wagon>
    {
       public IncludeForWagon(){
        
            AddInclude(p => p.Include("City"));
            AddInclude(p => p.Include("Events").Include("Tickets"));
            Includes = new List<Func<IQueryable<Wagon>, IQueryable<Wagon>>>();
       }

        public List<Func<IQueryable<Wagon>, IQueryable<Wagon>>> Includes { get;set; } 

        public void AddInclude(Func<IQueryable<Wagon>, IQueryable<Wagon>> includeExpression)
        {
            Includes.Add(includeExpression);
        }     
    }
}
