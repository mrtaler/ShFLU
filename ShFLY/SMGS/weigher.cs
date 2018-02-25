namespace ShFLY.SMGS
{
    using System;
    using System.Windows;

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

        private WagInSmgs wagInSmgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Weigher"/> class.
        /// </summary>
        /// <param name="wagInSmgs">
        /// The wag in smgs.
        /// </param>
        public Weigher(WagInSmgs wagInSmgs)
        {
            this.wagInSmgs = wagInSmgs;
            // : base(wagInSmgs)
            this.tarapr = wagInSmgs.Tarapr;
            this.weightb = wagInSmgs.Weightb;
            this.weight = wagInSmgs.Weight;
        }

        /// <summary>
        /// Gets or sets the weigher tara.
        /// </summary>
        public int WeigherTara { get; set; }
        public decimal neettoSmgs { get; set; }
        /// <summary>
        /// Gets or sets the weigher brutto.
        /// </summary>
        public int WeigherBrutto { get; set; }

        /// <summary>
        /// Gets or sets the weigher diff.
        /// </summary>
        public int WeigherDiff { get; set; }

        public int WeigherDiffNotPer { get; set; }

        public decimal WeigherDiffPer { get; set; }

        /// <summary>
        /// The get diff.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public void GetDiff()
        {
            if (this.weight!="")
            {
                neettoSmgs = Convert.ToDecimal(this.weight);
            }
          
            if (this.tarapr != "")
            {
                var taraprSmgs = Convert.ToInt32(this.tarapr);
                int i = 0;
                while (!this.LessThenOnePeccent(taraprSmgs, this.GetTara()))
                {
                    i++;
                    if (i> 100_000)
                    {
                        //    MessageBox.Show("x");
                        break;

                    }
                }

            }
            if (this.weightb != "")
            {
                var bruttoSmgs = Convert.ToInt32(this.weightb);
                int i = 0;
                while (!this.LessThenOnePeccent(bruttoSmgs, this.GetBrutto()))
                {
                    i++;
                    if (i > 100_000)
                    {
                        //    MessageBox.Show("x");
                        break;

                    }
                }
            }

            this.GetNetto();

            if (this.tarapr == "")
            {
                int i = 0;
                while (!this.LessThenOnePeccent(neettoSmgs, this.getNetto1()))
                {
                    i++;
                    if (i > 100_000)
                    {
                    //    MessageBox.Show("x");
                        break;

                    }
                }
            }

            WeigherDiffNotPer = WeigherDiff - Convert.ToInt32(this.weight);
            WeigherDiffPer = WeigherDiffNotPer * 100 / Convert.ToDecimal(this.weight);

        }


        public decimal getNetto1()
        {
            decimal nettoSmgs = 0;
            if (this.weight != "")
            {
                nettoSmgs = Convert.ToDecimal(this.weight);
            }

            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var per = Convert.ToDecimal(rnd.Next(-20, 20)) / 1000M;
            var ret = (nettoSmgs * per) + nettoSmgs;
            this.WeigherDiff = Convert.ToInt32(this.RoundTo200Up(ret));
            return ret;
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
            var per = Convert.ToDecimal(rnd.Next(-20, 20)) /1000M;
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
            var per = Convert.ToDecimal(rnd.Next(-20, 20)) / 1000M;
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

            return -0.95M <= per && per <=0.95M;
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
