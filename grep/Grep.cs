using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GrepLike
{
    public class Grep
    {
        string _rutaFichero;
        string _frase;

        public string Frase
        {
            get { return _frase; }
            set { _frase = value; }
        }

        public string RutaFichero
        {
            get { return _rutaFichero; }
            set { _rutaFichero = value; }
        }

        public Grep(string rutaFichero, string frase)
        {
            this.RutaFichero = rutaFichero;
            this.Frase = frase;
        }

        public void Matches()
        {
            int numero = 0,nLineas = 0,unaLinea = 0;
            string linea = string.Empty,regex = string.Empty;           
            Match match; MatchCollection matches;
            bool matched = false;
            StreamReader sr = new StreamReader(this.RutaFichero);

            //crear expresion regular
            foreach (char item in this.Frase)
                regex += "[" + item + "]";

            while (!sr.EndOfStream)
            {
                linea = sr.ReadLine();
                numero++;
                match = Regex.Match(linea, regex);

                matched = match.Success;
                if (matched)
                    matches = Regex.Matches(linea, regex);
                else
                    continue;

                /*se imprem las lineas con coincidencias.*/

                Console.WriteLine("\nLINEA {0}", numero);
                Console.WriteLine("".PadLeft(Console.WindowWidth, '='));
                //Console.WriteLine();
                for (int i = 0; i < linea.Length; i++)
                {
                    foreach (Match item in matches)
                    {
                        if (i == item.Index)
                        {
                            int len = 0;
                            while (len < this.Frase.Length)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(linea[i]);
                                len++;
                                i++;
                            }
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    //Si la ocurrencia es al final de la linea ya se ha escrito
                    if (i >= linea.Length)
                        continue;       

                    Console.Write(linea[i]);
                }
                /*Una linea puede ocupar varias en el buffer de la consola...
                 * TODO: Mejorar */
                unaLinea=(int)Math.Floor(linea.Length + 2 > Console.WindowWidth ? 
                    (double)(linea.Length / Console.WindowWidth) : 1);
                nLineas += unaLinea;

                Console.WriteLine();
                Console.WriteLine("".PadLeft(Console.WindowWidth, '='));
                Console.WriteLine();

                if (nLineas >= 22)
                {
                    Console.WriteLine("\nPresiona una tecla para ver mas ocurrencias");
                    Console.ReadKey();
                    nLineas = 0;
                }

            }
            sr.Close();
        }
    }
}
