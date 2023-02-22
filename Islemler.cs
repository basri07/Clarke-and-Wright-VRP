using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarkeAndWrightVRP
{
    public  class Islemler
    {
        /*public List<Customer> Customers = new List<Customer>();
        public List<Customer> RandomCreateCustomer (int CustomerCount,int CoordinateRange)
        {
            //0 ID'ye sahip (0,0) kordinatlarında depo eklendi
            Customer warehouse = new Customer();
            warehouse.ID = 0;
            warehouse.Name = "DEPO";
            warehouse.X = 0;
            warehouse.Y = 0;
            warehouse.Demand = 0;
            warehouse.Visit = false;
            Customers.Add(warehouse);

            string Name = "Customer";

            Random random = new Random();
            for (int i = 0; i < CustomerCount; i++)
            {
                //Her döngüde müşterilerin bilgileri için gerekli değişkenler üretildi.
                int ID = i + 1;
                string Cname = Name + ID.ToString();
                //Next metodu minumum içerir maksimum içermez ondan dolayı maksimum sayıya artı bir ekledik 
                int X = random.Next(-CoordinateRange, CoordinateRange+1);
                int Y = random.Next(-CoordinateRange, CoordinateRange+1);
                int rndDemand = random.Next(20, 41);
                //Rastsal Oluşturulan talep eğer 5'in katı değilse. Rastsal Talepten , Rastsal talebi 5'e bölümünden kalan miktarı çıkartılır. (Yani Rastsal Talep - Rastsal Talebin MOD 5'i)
                int Demand = rndDemand - (rndDemand % 5);
                //Oluşturduğumuz müşteri bilgilerini Nesne olarak ekleyelim
                Customer client = new Customer();
                client.ID = ID;
                client.Name = Cname;
                client.X = X;
                client.Y = Y;
                client.Demand = Demand;
                client.Visit = false;
                Customers.Add(client);
            }
            return Customers;
        }*/
    }
}
