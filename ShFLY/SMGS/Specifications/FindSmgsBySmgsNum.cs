using System;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;
using ShFLY.DataBase.Models;

namespace ShFLY.SMGS.Specifications
{
  public  class FindSmgsBySmgsNum : ISpecification<SmgsNakl>
    {
   
        private readonly int SmgsNum;
        public FindSmgsBySmgsNum(int SmgsNum)
        {
            this.SmgsNum = SmgsNum;
        }

        public Expression<Func<SmgsNakl, bool>> IsSatisifiedBy()
        {
            return x => x.Smgs == this.SmgsNum;
        }
    }
}

