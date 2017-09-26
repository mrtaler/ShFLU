using System;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;
using ShFLY.DataBase.Models;

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
            return x => x.Nwag == this.WagonNum;
        }
    }
}

