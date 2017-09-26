using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.DataBase.DAL.Specifications;
using ShFLY.DataBase.Models;
using System.Linq.Expressions;
using ShFLY.DataBase.DAL.Specifications.Interfaces;

namespace ShFLY.SMGS.Specifications
{
    class FindWagonByWagonNum : ISpecification<Wagon>
    {
        private readonly int WagonNum;
        public FindWagonByWagonNum(int WagonNum)
        {
            this.WagonNum = WagonNum;
        }

        public Expression<Func<Wagon, bool>> IsSatisifiedBy()
        {
            return x => x.Nwag == WagonNum;
        }
    }
}

