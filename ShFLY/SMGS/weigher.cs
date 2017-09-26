using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShFLY.SMGS
{
    using System.Security.Cryptography;

    using ShFLY.DataBase.Models;

    /// <summary>
    /// The weigher.
    /// </summary>
    public class Weigher// : WagInSmgs
    {
        private string Weight;

        private string Tarapr;

        private string Weightb;
        /// <summary>
        /// Initializes a new instance of the <see cref="Weigher"/> class.
        /// </summary>
        /// <param name="wagInSmgs">
        /// The wag in smgs.
        /// </param>
        public Weigher(WagInSmgs wagInSmgs)
        // : base(wagInSmgs)
        {
            this.Tarapr = wagInSmgs.Tarapr;
            this.Weightb = wagInSmgs.Weightb;
            this.Weight = wagInSmgs.Weight;
        }

        /// <summary>
        /// Gets or sets the weigher tara.
        /// </summary>
        public int WeigherTara { get; set; }

        public int WeigherBrutto { get; set; }

        /// <summary>
        /// Gets or sets the weigher diff.
        /// </summary>
        public int WeigherDiff { get; set; }

        public string getDiff()
        {
            var neettoSMGS = Convert.ToInt32(this.Weight);
            var taraprSMGS = Convert.ToInt32(this.Tarapr);
            var bruttoSMGS = Convert.ToInt32(this.Weightb);


            while (!lessThenOnePeccent((decimal)neettoSMGS, getNetto()))
            {
            }
            return "done";
        }

        public decimal getNetto()
        {
            var neettoSMGS = Convert.ToDecimal(this.Weight);
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var per = Convert.ToDecimal(rnd.Next(-49, 49)) / 1000M;
            var ret = (neettoSMGS * per) + neettoSMGS;
            WeigherDiff = Convert.ToInt32(ret);
            return ret;

        }
        public decimal getTara()
        {
            var taraprSMGS = Convert.ToDecimal(this.Tarapr);
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var per = Convert.ToDecimal(rnd.Next(-49, 49)) / 1000M;
            var ret = (taraprSMGS * per) + taraprSMGS;
            WeigherTara = Convert.ToInt32(ret);
            return ret;

        }
        public decimal getBrutto()
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var per = Convert.ToDecimal(rnd.Next(-49, 49)) / 1000M;
            var bruttoSMGS = Convert.ToDecimal(this.Weightb);
            var ret = (bruttoSMGS * per) + bruttoSMGS;
            WeigherBrutto = Convert.ToInt32(ret);
            return ret;

        }

        public bool lessThenOnePeccent(decimal wgthFromSmgs, decimal wgthFromMatrix)
        {



            var diffPer = (wgthFromMatrix * 100) / wgthFromSmgs;

            decimal per = 0;
            if (diffPer > 100)
            {
                per = diffPer - 100;
            }
            else
            {
                per =100- diffPer;
            }

            return -1 <= per && per <= 1;
        }
    }
}
