using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fabrica;
using EspacioPersonaje;

namespace ProyectoFinal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Obtener la lista de personajes desde la API
            List<Character> personajesApi = await FabricaDePersonajes.ObtenerPersonajesDesdeApi();

            // Convertir los personajes de la API a objetos Personaje
            List<EspacioPersonaje.Personaje> personajes = FabricaDePersonajes.ConvertirAReturnPersonajes(personajesApi);

            // Mostrar los personajes (opcional)
            Console.WriteLine("Personajes creados:");
            MostrarPersonajes(personajes);

            // Guardar personajes en un archivo JSON
            string rutaArchivo = "personajes.json";
            PersonajesJson.GuardarPersonajes(personajes, rutaArchivo);

            // Verificar si el archivo fue creado y leer los personajes del archivo
            if (PersonajesJson.ExisteArchivo(rutaArchivo))
            {
                List<Personaje> personajesLeidos = PersonajesJson.LeerPersonajes(rutaArchivo);
                Console.WriteLine("Personajes leídos del archivo:");
                MostrarPersonajes(personajesLeidos);
            }
            else
            {
                Console.WriteLine("No se encontró el archivo de personajes.");
            }

            // Verificar que haya al menos dos personajes para realizar un combate
            if (personajes.Count >= 2)
            {
                // Seleccionar dos personajes aleatorios que no sean el mismo
                Random random = new Random();
                Personaje personaje1 = personajes[random.Next(personajes.Count)];
                Personaje personaje2;

                do
                {
                    personaje2 = personajes[random.Next(personajes.Count)];
                } while (personaje2 == personaje1);

                // Iniciar el combate
                Combate combate = new Combate(personaje1, personaje2);
                combate.IniciarCombate();
            }
            else
            {
                Console.WriteLine("No hay suficientes personajes para realizar un combate.");
            }

            Console.ReadLine();  // Espera a que el usuario presione Enter antes de salir
        }

        static void MostrarPersonajes(List<EspacioPersonaje.Personaje> personajes)
        {
            foreach (var personaje in personajes)
            {
                Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
                Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
                Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
                Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento:dd-MM-yyyy}");
                Console.WriteLine($"Género: {personaje.Datos.Gender}");
                Console.WriteLine($"Ancestry: {personaje.Datos.Ancestry}");
                Console.WriteLine($"Imagen: {personaje.Datos.Imagen}");
                Console.WriteLine();
            }
        }
    }
}
