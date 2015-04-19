﻿using System;
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
                else
                    grep = new Grep(args[0], args[1]);
                
                grep.Matches();
            }
            catch (Exception e)
            {
                Console.WriteLine("".PadLeft(50,'=')+ 
                    "\nERROR: "+ e.Message + "\n".PadRight(51, '='));
            }
            Console.ReadLine();
        }
    }
}