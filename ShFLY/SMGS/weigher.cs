namespace ShFLY.SMGS
{
    using System;

    using ShFLY.DataBase.Models;

    /// <summary>
    /// The weigher.
    /// </summary>
    public class Weigher
    {
        /// <summary>
        /// The weight.
        /// </summary>
        private readonly string weight;

        /// <summary>
        /// The tarapr.
        /// </summary>
        private readonly string tarapr;

        /// <summary>
        /// The weightb.
        /// </summary>
        private readonly string weightb;

        /// <summary>
        /// Initializes a new instance of the <see cref="Weigher"/> class.
        /// </summary>
        /// <param name="wagInSmgs">
        /// The wag in smgs.
        /// </param>
        public Weigher(WagInSmgs wagInSmgs)
        {
            // : base(wagInSmgs)
            this.tarapr = wagInSmgs.Tarapr;
            this.weightb = wagInSmgs.Weightb;
            this.weight = wagInSmgs.Weight;
        }

        /// <summary>
        /// Gets or sets the weigher tara.
        /// </summary>
        public int WeigherTara { get; set; }

        /// <summary>
        /// Gets or sets the weigher brutto.
        /// </summary>
        public int WeigherBrutto { get; set; }

        /// <summary>
        /// Gets or sets the weigher diff.
        /// </summary>
        public int WeigherDiff { get; set; }

        /// <summary>
        /// The get diff.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDiff()
        {
            int neettoSmgs = 0;
            int taraprSmgs = 0;
            int bruttoSmgs = 0;
            if (this.weight!="")
            {
                neettoSmgs = Convert.ToInt32(this.weight);
            }
            if (this.tarapr != "")
            {
                 taraprSmgs = Convert.ToInt32(this.tarapr);
            }
            if (this.weightb != "")
            {
                 bruttoSmgs = Convert.ToInt32(this.weightb);
            }

            while (!this.LessThenOnePeccent(taraprSmgs, this.GetTara()))
            {
            }

            while (!this.LessThenOnePeccent(bruttoSmgs, this.GetBrutto()))
            {
            }

            return this.LessThenOnePeccent(neettoSmgs, this.GetNetto()).ToString();
        }

        /// <summary>
        /// The get netto.
        /// </summary>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal GetNetto()
        {
            this.WeigherDiff = this.WeigherBrutto - this.WeigherTara;
            return this.WeigherDiff;
        }

        /// <summary>
        /// The get tara.
        /// </summary>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal GetTara()
        {
            decimal taraprSmgs = 0;
            if (this.tarapr!="")
            {
                taraprSmgs = Convert.ToDecimal(this.tarapr);
            }   
            
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var per = Convert.ToDecimal(rnd.Next(-30, 25)) / 1000M;
            var ret = (taraprSmgs * per) + taraprSmgs;
            this.WeigherTara = Convert.ToInt32(this.RoundTo200Up(ret));
            return ret;
        }

        /// <summary>
        /// The get brutto.
        /// </summary>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal GetBrutto()
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var per = Convert.ToDecimal(rnd.Next(-30, 25)) / 1000M;
            var bruttoSmgs = Convert.ToDecimal(this.weightb);
            var ret = (bruttoSmgs * per) + bruttoSmgs;
            this.WeigherBrutto = Convert.ToInt32(this.RoundTo200Up(ret));
            return ret;
        }

        /// <summary>
        /// The less then one peccent.
        /// </summary>
        /// <param name="wgthFromSmgs">
        /// The wgth from smgs.
        /// </param>
        /// <param name="wgthFromMatrix">
        /// The wgth from matrix.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LessThenOnePeccent(decimal wgthFromSmgs, decimal wgthFromMatrix)
        {
            decimal diffPer=0;
            if (wgthFromSmgs!=0)
            {
                 diffPer = (wgthFromMatrix * 100) / wgthFromSmgs;
            }
         

            decimal per;
            if (diffPer > 100)
            {
                per = diffPer - 100;
            }
            else
            {
                per = 100 - diffPer;
            }

            return -1 <= per && per <= 1;
        }

        /// <summary>
        /// The round to 200 up.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal RoundTo200Up(decimal d)
        {
            d = Math.Floor(d);

            var i = Convert.ToInt32(d);
            if (i % 20 != 0)
            {
                i = (i / 20) * 20 + 20;
            }

            return i;
        }
    }
}
