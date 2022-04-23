using System;
using System.Collections.Generic;
using System.IO;
namespace RegistroPersonas
{
    class Program
    {
        public static string filename = "\\registro.txt";
        public static string directory = Directory.GetCurrentDirectory();
        public static string path = directory + filename;

        static void Main(string[] args)
        {
            int opt;
            do
            {
                Console.Clear();
                Menu();
                opt = int.Parse(Console.ReadLine());
                switch(opt)
                {
                    case 1:
                        RegistrarPersona();
                        Console.ReadKey();
                        break;
                    case 2:
                        ConsultaMayoresDeEdad();
                        Console.ReadKey();
                        break;
                    case 3:
                        ConsultaMenoresDeEdad();
                        Console.ReadKey();
                        break;
                    case 4:
                        VerEstadisticas();
                        Console.ReadKey();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Seleccione una opcion correcta");
                        Console.ReadKey();
                        break;
                }
            } while (opt != 5);
        }

        static void Menu()
        {
            Console.WriteLine("Registro de personas");
            Console.WriteLine("Opción 1: Registrar persona");
            Console.WriteLine("Opción 2: Consultar mayores de edad");
            Console.WriteLine("Opción 3: Consultar menores de edad");
            Console.WriteLine("Opción 4: Ver estadisticas");
            Console.WriteLine("Opción 5: Salir");
            Console.WriteLine("Seleccione una opción");
        }

        static void ValidarArchivo()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(path))
            {
                FileStream fileStream = File.Create(path);
                fileStream.Close();
            }
        }

        static void VerEstadisticas()
        {
            try {
                //contadores
                int menores = 0, mayores = 0, repetidos = 0;

                string[] lines = File.ReadAllLines(path);
                List<Modelos.Persona> personas = new List<Modelos.Persona>();


                foreach (string line in lines)
                {
                    Modelos.Persona persona = new Modelos.Persona();
                    persona.SetLine(line.Split(","));
                    personas.Add(persona);
                    if (persona.Edad() < 18)
                    {
                        menores++;
                    }
                    else
                    {
                        mayores++;
                    }
                }

                foreach (Modelos.Persona item in personas)
                {
                    repetidos += isRepeated(personas, item) ? 1 : 0;
                }

                Console.WriteLine("ESTADISTICAS");
                Console.WriteLine("---------------------------------------------- ");
                Console.WriteLine("Cantidad de personas menores de edad: " + menores);
                Console.WriteLine("Cantidad de personas mayores de edad: " + mayores);
                Console.WriteLine("Cantidad de personas repetidas: " + repetidos);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static bool isRepeated(List<Modelos.Persona> items, Modelos.Persona item)
        {
            bool cond = false;

            List<Modelos.Persona> matches = items.FindAll((element) => element.dpi == item.dpi);

            if (matches.Count > 1) cond = true;

            return cond;
        }

        static void RegistrarPersona()
        {
            try
            {
                ValidarArchivo();
                Modelos.Persona persona = new Modelos.Persona();
                Console.WriteLine("Ingrese no de DPI: ");
                persona.dpi = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese nombre completo: ");
                persona.nombre = Console.ReadLine();
                Console.WriteLine("Ingrese fecha de nacimiento(dd/mm/yyyy)");
                persona.setDateOfBirth(Console.ReadLine());

                StreamWriter stream = File.AppendText(path);

                stream.WriteLine(persona.GetLine());
                stream.Close();
                Console.WriteLine("Persona registrada!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ConsultaMenoresDeEdad()
        {
            try
            {
                string[] lines = File.ReadAllLines(path);

                Console.WriteLine("LISTADO MENORES DE EDAD");
                foreach(string line in lines)
                {
                    Modelos.Persona persona = new Modelos.Persona();
                    string[] value = line.Split(",");
                    persona.SetLine(value);

                    if (persona.Edad() < 18)
                    {
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("DPI: "+ persona.dpi);
                        Console.WriteLine("DPI: " + persona.nombre);
                        Console.WriteLine("DPI: " + persona.fechaNacimiento.ToShortDateString());
                        Console.WriteLine("-----------------------");
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ConsultaMayoresDeEdad()
        {
            try
            {
                string[] lines = File.ReadAllLines(path);

                Console.WriteLine("LISTADO MAYORES DE EDAD");
                foreach (string line in lines)
                {
                    Modelos.Persona persona = new Modelos.Persona();
                    string[] value = line.Split(",");
                    persona.SetLine(value);

                    if (persona.Edad() >= 18)
                    {
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("DPI: " + persona.dpi);
                        Console.WriteLine("DPI: " + persona.nombre);
                        Console.WriteLine("DPI: " + persona.fechaNacimiento.ToShortDateString());
                        Console.WriteLine("-----------------------");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
