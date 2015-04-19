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
        private string _rutaFichero;
        private string _frase;

        public string Frase
        {
            get { return _frase; }
            set { _frase = value.ToLowerInvariant(); }
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
            string temp = string.Empty;
            int ocurrencias = 0;
            int numero = 0, nLineas = 0, unaLinea = 0;
            string linea = string.Empty, regex = string.Empty;           
            Match match;
            MatchCollection matches;
            bool matched = false;
            StreamReader sr = new StreamReader(this.RutaFichero);

            //se crea la expresion regular
            foreach (char item in this.Frase)
                regex += "[" + item + "]"; 

            /*Se lee el fichero linea a linea buscando
             *coincidencias con la expresion regular*/ 
            while (!sr.EndOfStream)
            {
                linea = sr.ReadLine();
                temp = linea.ToLowerInvariant();
                numero++;
                match = Regex.Match(temp, regex);

                matched = match.Success;
                if (matched)
                {
                    matches = Regex.Matches(temp, regex);
                    ocurrencias += matches.Count;
                }
                else
                    continue;

                /*se imprimen las lineas con coincidencias.*/
                Console.WriteLine("\nLINEA {0}", numero);
                Console.WriteLine("".PadLeft(Console.WindowWidth, '='));

                /*Se imprime caracter a caracter cada linea
                 *Si se llega a un caracter cuya posición
                 *coincide con el indice de alguna ocurrencia
                 *se pintará en color amarillo */
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
                unaLinea = (int)Math.Floor(linea.Length + 2 > Console.WindowWidth ? 
                    (double)(linea.Length / Console.WindowWidth) : 1);
                nLineas += unaLinea;

                Console.WriteLine();
                Console.WriteLine("".PadLeft(Console.WindowWidth, '='));
                Console.WriteLine();

                if (nLineas >= 22)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPresiona una tecla para ver mas ocurrencias");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    nLineas = 0;
                }
            }
            sr.Close();

            if (ocurrencias == 0)
                Console.WriteLine("No se han encontrado coincidencias");
            else
                Console.WriteLine("Ocurrencias encontradas: {0}", ocurrencias);
        }
    }
}
