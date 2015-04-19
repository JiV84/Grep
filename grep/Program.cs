using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GrepLike
{
    class Program
    {
        static void Main(string[] args)
        {      
            Grep grep;

            try
            {
                if (args == null || args.Length < 2)
                    throw new ArgumentException("Sintaxis: grep <rutaFichero> <textoAEncontrar>");
                else if (!File.Exists(args[0]))
                    throw new FileNotFoundException(string.Format("El fichero '{0}' no existe!", args[0]));
                else if (string.IsNullOrEmpty(args[1]))
                    throw new ArgumentException("El texto introducido no es válido");
                else
                    grep = new Grep(args[0], args[1]);
                
                grep.Matches();
            }
            catch (Exception e)
            {
                Console.WriteLine("".PadLeft(55,'=')+ 
                    "\nERROR: "+ e.Message + "\n".PadRight(56, '='));
            }
        }
    }
}
