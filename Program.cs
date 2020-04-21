using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2018MajEmeltTarsalgo
{
    class Tarsalgo
    {
        public static List<Tarsalgo> Adat = new List<Tarsalgo>();
        public int Sor;
        public int AthOra;
        public int AthPerc;
        public int Azon;
        public string BeKi;
        public string TelIdo;

        public static int megadottAzon;
        public static int bent;
        public static int kint;

        public Tarsalgo(int sor, int athOra, int athPerc, int azon, string beKi, string telIdo)
        {
            Sor = sor;
            AthOra = athOra;
            AthPerc = athPerc;
            Azon = azon;
            BeKi = beKi;
            this.TelIdo = telIdo;
        }

        public static void ElsoFeladat(string fajl)
        {
            int sor = 0;

            using (StreamReader olvas = new StreamReader(fajl))
            {
                while (!olvas.EndOfStream)
                {
                    sor++;

                    string[] split = olvas.ReadLine().Split(' ');
                    int ora = Convert.ToInt32(split[0]);
                    int perc = Convert.ToInt32(split[1]);
                    int azon = Convert.ToInt32(split[2]);
                    string beKi = split[3];
                    string tel = ora + ":" + perc;

                    Tarsalgo tarsalgo = new Tarsalgo(sor, ora, perc, azon, beKi, tel);

                    Adat.Add(tarsalgo);
                }
            }
        }

        public static void MasodikFeladat() => Console.WriteLine($"2. feladat: \nAz első belépő: {Adat.Where(x => x.BeKi == "be").Select(x => x.Azon).First()}\nAz utolsó belépő: {Adat.Where(x => x.BeKi == "ki").Select(x => x.Azon).Last()}");

        public static void HarmadikFeladat()
        {
            SortedDictionary<int, int> rendezes = new SortedDictionary<int, int>();

            for (int i = 0; i < Adat.Count; i++)
            {
                if (rendezes.ContainsKey(Adat[i].Azon))
                    rendezes[Adat[i].Azon]++;
                else
                    rendezes[Adat[i].Azon] = 1;
            }

            var rendezett = rendezes.OrderBy(x => x.Key);


            using (StreamWriter ki = new StreamWriter(@"athaladas.txt"))
            {
                foreach (var athaladasok in rendezett)
                {
                    ki.WriteLine($"{athaladasok.Key} {athaladasok.Value}");
                    ki.Flush();
                }
            }
        }

        public static void NegyedikFeladat()
        {
            int[] gyak = new int[101];
            for (int i = 0; i < 101; i++)
            {
                gyak[i] = 0;
            }

            for (int i = 0; i < Adat.Count; i++)
            {
                gyak[Adat[i].Azon]++;
            }

            Console.Write($"\n4. feladat:\nA végén a társalgóban voltak:");

            for (int i = 0; i < 101; i++)
            {
                if(gyak[i] != 0)
                    if(gyak[i] % 2 == 1)
                        Console.Write($" {i}");
            }
        }

        public static void OtodikFeladat()
        {
            var bentlevok = Adat.Where(x => x.BeKi == "be").ToList();

            SortedDictionary<string, int> rendezes = new SortedDictionary<string, int>();

            for (int i = 0; i < bentlevok.Count; i++)
            {
                if (rendezes.ContainsKey(bentlevok[i].TelIdo))
                    rendezes[bentlevok[i].TelIdo]++;
                else
                    rendezes[bentlevok[i].TelIdo] = 1;
            }

            var legtobben = rendezes.OrderByDescending(x => x.Value).Select(x => x.Key).First();

            Console.WriteLine($"\n5. feladat: \nPéldául {legtobben}-kor voltak a legtöbben a társalgóban.");
        }

        public static void HatodikFeladat()
        {
            Console.Write("\n6. feladat: \nAdja meg a személy azonosítóját! ");
            megadottAzon = Convert.ToInt32(Console.ReadLine());
        }

        public static void HetedikFeladat()
        {
            Console.WriteLine("\n7. feladat:");
            var meMe = Adat.Where(x => x.Azon == megadottAzon).ToList();

            for (int i = 0; i < meMe.Count; i++)
            {
                if (meMe[i].BeKi == "be")
                {
                    Console.Write(meMe[i].TelIdo + " - ");
                    kint = meMe[i].AthOra * 60 + meMe[i].AthPerc;
                }
                else
                {
                    Console.WriteLine(meMe[i].TelIdo);
                    bent += meMe[i].AthOra * 60 + meMe[i].AthPerc - kint;
                    kint = 0;
                }
            }
        }

        public static void NyolcadikFeladat()
        {
            if (kint != 0)
            {
                bent += 15 * 60 - kint;
                Console.WriteLine();
            }

            Console.WriteLine($"\n8. feladat: \nA(z) {megadottAzon}. személy összesen {bent} percet volt bent, a megfigyelés végén a társalóban volt.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Tarsalgo.ElsoFeladat(@"ajto.txt");
            Tarsalgo.MasodikFeladat();
            Tarsalgo.HarmadikFeladat();
            Tarsalgo.NegyedikFeladat();
            Tarsalgo.OtodikFeladat();
            Tarsalgo.HatodikFeladat();
            Tarsalgo.HetedikFeladat();
            Tarsalgo.NyolcadikFeladat();
        }
    }
}
