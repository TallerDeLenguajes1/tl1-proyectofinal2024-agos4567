using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EspacioPersonaje;
using Fabrica;

namespace ProyectoFinal
{
    public class Gameplay
    {
        public static async Task IniciarJuego()
        {
            // Obtener la lista de personajes desde la API
            List<Character> personajesApi = await FabricaDePersonajes.ObtenerPersonajesDesdeApi();

            // Convertir los personajes de la API a objetos Personaje
            List<Personaje> personajes = FabricaDePersonajes.ConvertirAReturnPersonajes(personajesApi);

            // Mostrar los personajes
            Console.WriteLine("Personajes creados:");
            MostrarPersonajes(personajes);

            // Verificar que hay al menos 2 personajes para el combate
            if (personajes.Count >= 2)
            {
                Random random = new Random();
                
                // Seleccionar dos personajes aleatorios (que no se repitan)
                Personaje personaje1 = personajes[random.Next(personajes.Count)];
                Personaje personaje2;

                do
                {
                    personaje2 = personajes[random.Next(personajes.Count)];
                } while (personaje2 == personaje1);

                // Iniciar el combate
                Combate combate = new Combate(personaje1, personaje2);
                Personaje ganador = combate.IniciarCombate();

                // Mostrar los detalles del ganador
                Console.WriteLine($"Ganador: {ganador.Datos.Nombre}");
            }
            else
            {
                Console.WriteLine("No hay suficientes personajes para iniciar el combate.");
            }

            Console.ReadLine();  // Espera a que el usuario presione Enter antes de salir
        }

        private static void MostrarPersonajes(List<Personaje> personajes)
        {
            foreach (var personaje in personajes)
            {
                Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
                Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
                Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
                Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento.ToString("dd-MM-yyyy")}");
                Console.WriteLine($"Género: {personaje.Datos.Gender}");
                Console.WriteLine($"Ancestry: {personaje.Datos.Ancestry}");
                Console.WriteLine($"Imagen: {personaje.Datos.Imagen}");
                Console.WriteLine();
            }
        }
    }
}
