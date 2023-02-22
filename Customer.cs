using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarkeAndWrightVRP
{
    //Müşteri Nesnesi
    public class Customer:ICloneable
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Demand { get; set; }
        public Boolean Visit { get; set; }

        //Nesneleri kopyalama gerekebilir diye önceden Clone methodu eklendi lazım olunca kullanılacak
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Customer()
        {

        }
        //Müşteri Nesnelerinin gerekli özellikleri gelen veriler üzerinden hafızada tutulacak.
        public Customer(int ID,string Name,int X,int Y,int Demand,Boolean Visit)
        {
            this.ID = ID;
            this.Name=Name;
            this.X = X;
            this.Y = Y;
            this.Demand = Demand;
            this.Visit = Visit;
        }

       

    }
}
