using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkeAndWrightVRP
{
    public class Program
    {
        Islemler islemler = new Islemler();
        static void Main(string[] args)
        {
            List<Customer> Customers = new List<Customer>();
            List<TasarrufObj> tasarrufList = new List<TasarrufObj>();
            Console.WriteLine("Müşteri Sayısı : ");
            int CustomerCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Koordinat Aralığı : ");
            int CoordinateRange = Convert.ToInt32(Console.ReadLine());

            #region Müşteri Nesnesi için gerekli entityleri rastsal oluştur ve Customer Listesine ekle
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
            Console.WriteLine("MÜŞTERİ TALEPLERİ ve KOORDİNATLARI");
            Console.WriteLine("DEPO \t\t0\t(0,0)");
            for (int i = 0; i < CustomerCount; i++)
            {
                //Her döngüde müşterilerin bilgileri için gerekli değişkenler üretildi.
                int ID = i + 1;
                string Cname = Name + ID.ToString();
                //Next metodu minumum içerir maksimum içermez ondan dolayı maksimum sayıya artı bir ekledik 
                int X = random.Next(-CoordinateRange, CoordinateRange + 1);
                int Y = random.Next(-CoordinateRange, CoordinateRange + 1);
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
                
                Console.WriteLine(Cname + "\t" + Demand.ToString()+"\t"+"("+X.ToString()+","+Y.ToString()+")");
            }
            #endregion
            //Depo'da müşteri olarak eklendi
            CustomerCount = CustomerCount+1;
            //Uzaklık (öklid) Matrisi
            #region Müşterilerin birbirine olan öklid uzaklıklarını hesapla
            int[,] Distance = new int[CustomerCount, CustomerCount];
            Console.Write("\nUZAKLIK MATRISI\n");
            for (int i = 0; i < CustomerCount; i++)
            {
                for (int j = 0; j < CustomerCount; j++)
                {
                    
                    if (i==0)
                    { 
                       
                    }
                    //Eğer i eşit j ise hesaplama yapmasın uzaklık direkt olarak 0 alsın işlem yapmak için zaman harcamasın.
                    if (i==j)
                    {
                        Distance[i, j] = 0;
                       
                    }
                    else
                    {
                        //x2-x1
                        int XFark = Customers[j].X - Customers[i].X;
                        //y2-y1
                        int YFark = Customers[j].Y - Customers[i].Y;
                        //(x2-x1)^2
                        int Xkare = (int)Math.Pow(XFark, 2);
                        //(y2-y1)^2
                        int Ykare = (int)Math.Pow(YFark, 2);
                        //(x2-x1)^2+(y2-y1)^2
                        int FarkkareToplam = Xkare + Ykare;
                        //KAREKOK[(x2-x1)^2+(y2-y1)^2]
                        double Karekok = Math.Sqrt(Convert.ToDouble(FarkkareToplam));
                        //Math.Celling metodu çıkan double sayıyı bir üst tam sayıya yuvarlar (int) sayesinde integer türüne çevirir. Math.floor metodu ise bir alt tam sayıya yuvarlar.
                        Distance[i, j] = (int)Math.Ceiling(Karekok); 
                    }
                    Console.WriteLine("("+i.ToString() +","+ j.ToString()+ ")\t\t" + Distance[i,j].ToString());
                }
            }
            #endregion
            #region Tasarruf Matrisi Hesaplama
            //Tasarruf Matrisi Hesapla
            //Depo hariç diğer müşterilerin tasarrufunu hesaplayacağımız için i=1 den başlar yada döngü içine if(i!=0) da konulabilir
           int[,] TasarrufMatrisi = new int[CustomerCount, CustomerCount];
            Console.Write("\n TASARRUF MATRISI \n");

            for (int i = 0; i < CustomerCount; i++)
            {
                for (int j = 0; j < CustomerCount; j++)
                {
                    //Eğer i düğümü j düğümü ile aynı ise tasarruf yok ya da i düğümü depo düğü ise tasarruf yok
                    if (i==j||i==0)
                    {
                        TasarrufMatrisi[i, j] = 0;
                    }
                    else
                    {
                        //Müşteri i'in Depoya uzaklığı + Müşteri j'nin depoya uzaklığı eksi Müşteri i ile Müşteri j'nin birbirine olan uzaklığı
                        TasarrufMatrisi[i,j]= Distance[i, 0] + Distance[j,0] - Distance[i,j];
                    }
                }
            }
            #endregion
            #region Tasarruf Matrisi i'den j'ye olan tasarruf mikatrı aynı olduğu için j'den i'ye olanları silindi. Gereksiz kalabalıktan kurtulduk ve Büyükten Küçüğe Sıralama yaptık
            //Depo ve Aynı arc'lar matristen çıkarıldı
            ArrayList Atanabilir = new ArrayList();
            for (int i = 1; i < CustomerCount; i++)
            {
                Atanabilir.Add(i);
                for (int j = i+1; j < CustomerCount; j++)
                {
                    TasarrufObj tasarruf = new TasarrufObj();
                    tasarruf.I = i;
                    tasarruf.J = j;
                    tasarruf.Amount = TasarrufMatrisi[i, j];
                    tasarrufList.Add(tasarruf);
                    Console.WriteLine("("+i.ToString()+","+j.ToString()+")\t" + TasarrufMatrisi[i,j].ToString());
                }
            }
            //Linq kütüphanesi yardımı ile liste büyükten küçüğe sıralaması yapıldı
            var newTassList = tasarrufList.OrderByDescending(x => x.Amount).ToList();
            Console.WriteLine("TASARRUF MATRISI BÜYÜKTEN KÜÇÜĞE SIRALAMA");
            for (int i = 0; i < newTassList.Count; i++)
            {
                Console.WriteLine("("+newTassList[i].I.ToString() +"," +newTassList[i].J.ToString() + ") \t" + newTassList[i].Amount.ToString());
            }
            #endregion
            #region Rotalama iterasyonları

            ArrayList Atanan = new ArrayList();
            while (Atanabilir.Count > 0)
            {
                Atanan.Add(0);
                int Kapasite = 100;
                for (int i = 0; i < tasarrufList.Count; i++)
                {
                    int I = newTassList[i].I;
                    int J = newTassList[i].J;
                    if (Atanan.Contains(I) == false && Atanan.Contains(J) == false)
                    {
                        int ArcDemand = Customers[I].Demand + Customers[J].Demand;
                        Kapasite -= ArcDemand;
                        if (Kapasite >= 0)
                        {
                            Atanan.Add(I);
                            Atanan.Add(J);
                            Atanabilir.Remove(I);
                            Atanabilir.Remove(J);
                        }
                        else
                        {
                            Kapasite += ArcDemand;
                        }
                    }
                    if (Atanan.Contains(I) == true && Atanan.Contains(J) == false)
                    {
                        int Demand = Customers[J].Demand;
                        Kapasite -= Demand;
                        if (Kapasite >= 0)
                        {
                            Atanan.Add(J);
                            Atanabilir.Remove(J);
                        }
                        else
                        {
                            Kapasite += Demand;
                        }
                    }
                }
            }
            Atanan.Add(0);
            int ToplamUzaklık = 0;
            for (int i = 0; i < Atanan.Count; i++)
            {
                Console.WriteLine(Atanan[i].ToString() + "\t" + Customers[Convert.ToInt32(Atanan[i])].Name);
                int I = (int)Atanan[i];
                if (i != Atanan.Count - 1)
                {
                    int J = (int)Atanan[i + 1];
                    int Uzaklık = Distance[I, J];
                    ToplamUzaklık += Uzaklık;
                }
                else
                {
                    break;
                }
            }
            #endregion
            Console.WriteLine("TOPLAM UZAKLIK \t " + ToplamUzaklık.ToString());

            Console.ReadKey();
        }
    }
}
