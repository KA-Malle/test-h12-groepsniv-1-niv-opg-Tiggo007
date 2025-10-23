using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_H12_groepsniv_1_NIV_OPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * NAAM => Tiggo Vinnis 6ADB 23/10/2025
             */

            // Consolekleuren instellen.
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            // Variable declareren
            string volledigelijn, vertrekcode, vertrekplaats, aantalvorige = "", rapport;
            string[] lijngesplit;
            int maxbezet, aantalg = 0, count = 0, aantvliegh = 0, aantrap = 0;
            double gembezet = 0, bezet;
            double[] abezet; 

            StreamReader streamvliegtuig = new StreamReader("vliegtuigbezetting.txt");
            StreamReader streamvlieghaven = new StreamReader("Rapport - vlieghaven.txt");

            Console.WriteLine("Bezettingen vliegteugen per vertrekpunt");
            Console.WriteLine("=======================================");
            Console.WriteLine("");


            while (streamvliegtuig.Peek() != -1)
            {
                volledigelijn = streamvliegtuig.ReadLine();
                lijngesplit = volledigelijn.Split(',');
                vertrekcode = lijngesplit[0];
                vertrekplaats = lijngesplit[1];
                bezet = Convert.ToInt32(lijngesplit[2]);
                //abezet[count] = bezet;
                maxbezet = Convert.ToInt32(lijngesplit[3]);
                if (aantalvorige == vertrekcode)
                {
                    aantalg ++;
                }
                else
                {
                    aantalg = 0;
                    aantalvorige = vertrekcode;
                    aantvliegh++;
                }


                gembezet = Math.Round(bezet /aantalg * 100 ,2, MidpointRounding.AwayFromZero);
                if (gembezet >= 75)
                {
                    while (streamvlieghaven.Peek() != -1)
                    {
                        rapport = streamvlieghaven.ReadLine();
                        rapport = rapport.Replace("XXXX", vertrekplaats);
                        rapport = rapport.Replace("XXX", Convert.ToString(gembezet));
                        aantrap++;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(vertrekplaats + "\t" + gembezet);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(vertrekplaats + "\t" + gembezet);
                }
                Console.ForegroundColor = ConsoleColor.Black;
                count++;
            } // while

            Console.WriteLine("Gecontroleerde vlieghavens: " + aantvliegh );
            Console.WriteLine("Aantal rapporten verzonden: " + aantrap);
            Console.WriteLine("Gemmidelde Bezettingen:" + gembezet);

            // Wachten op enter.
            Console.WriteLine();
            Console.WriteLine("Druk op enter om te eindigen.");
            Console.ReadLine();
        }
    }
}
