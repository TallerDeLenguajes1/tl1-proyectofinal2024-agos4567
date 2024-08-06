using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EspacioPersonaje;
using Fabrica;

namespace ProyectoFinal
{
    public class Gameplay
    {
        
        private const string ArchivoPersonajes = "personajes.json";
        private const string ArchivoGanadores = "ganadores.json";

        public static async Task IniciarJuego()
        {
            // Cargar personajes desde el archivo o la API
            List<Personaje> personajes;
            if (PersonajesJson.ExisteArchivo(ArchivoPersonajes))
            {
                personajes = PersonajesJson.LeerPersonajes(ArchivoPersonajes);
            }
            else
            {
                List<Character> personajesApi = await FabricaDePersonajes.ObtenerPersonajesDesdeApi();
                personajes = FabricaDePersonajes.ConvertirAReturnPersonajes(personajesApi);
                PersonajesJson.GuardarPersonajes(personajes, ArchivoPersonajes);
            }

            // Mostrar los personajes
            Console.WriteLine("Personajes disponibles:");
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
                if (ganador != null)
                {
                    Console.WriteLine($"Ganador: {ganador.Datos.Nombre}");

                    // Guardar el ganador en el historial
                    HistorialJson.GuardarGanador(ganador, ArchivoGanadores);
                    Console.WriteLine($"El ganador ha sido guardado en el historial.");
                }
                else
                {
                    Console.WriteLine("No se pudo determinar un ganador.");
                }
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
