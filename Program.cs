using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fabrica;
using EspacioPersonaje;

namespace ProyectoFinal
{
    class Program
    {
        private const string ArchivoPersonajes = "personajes.json";
        private const string ArchivoGanadores = "historial.json";
       static async Task Main(string[] args)
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

            // Verificar si hay suficientes personajes para combatir
            if (personajes.Count >= 2)
            {
                Random random = new Random();
                Personaje personaje1 = personajes[random.Next(personajes.Count)];
                Personaje personaje2;

                do
                {
                    personaje2 = personajes[random.Next(personajes.Count)];
                } while (personaje2 == personaje1);

                // Iniciar el combate
                Combate combate = new Combate(personaje1, personaje2);
                Personaje ganador = combate.IniciarCombate();

                // Guardar el ganador en el historial
                if (ganador != null)
                {
                    HistorialJson.GuardarGanador(ganador, "historial.json");
                    Console.WriteLine($"El ganador {ganador.Datos.Nombre} ha sido guardado en el historial.");
                }
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
