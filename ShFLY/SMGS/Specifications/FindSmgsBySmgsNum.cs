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
  public  class FindSmgsBySmgsNum : ISpecification<SmgsNakl>
    {
   
        private readonly int SmgsNum;
        public FindSmgsBySmgsNum(int SmgsNum)
        {
            this.SmgsNum = SmgsNum;
        }

        public Expression<Func<SmgsNakl, bool>> IsSatisifiedBy()
        {
            return x => x.Smgs == SmgsNum;
        }
    }
}

