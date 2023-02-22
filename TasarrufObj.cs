using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarkeAndWrightVRP
{
    public class TasarrufObj:ICloneable
    {
 
        public int I { get; set; }
        public int J { get; set; }
        public int Amount { get; set; }

      
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public TasarrufObj()
        {

        }
        //Tasarruf Nesnelerinin gerekli özellikleri gelen veriler üzerinden hafızada tutulacak.
        public TasarrufObj(int I, int J, int Amount)
        {
            this.I = I;
            this.J = J;
            this.Amount = Amount;

        }


    }
}
