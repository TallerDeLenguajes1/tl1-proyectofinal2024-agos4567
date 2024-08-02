using System;
using System.Collections.Generic;
using EspacioPersonaje;
using Fabrica;

namespace ProyectoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear una lista de personajes
            List<Personaje> personajes = new List<Personaje>();

            // Añadir personajes aleatorios a la lista
            for (int i = 0; i < 5; i++)
            {
                personajes.Add(FabricaDePersonajes.CrearPersonajeAleatorio());
            }

            // Definir el nombre del archivo JSON
            string nombreArchivo = "personajes.json";

            // Guardar los personajes en un archivo JSON
            PersonajesJson.GuardarPersonajes(personajes, nombreArchivo);

            // Verificar si el archivo JSON existe y tiene datos
            bool archivoExiste = PersonajesJson.Existe(nombreArchivo);
            Console.WriteLine($"¿El archivo existe y tiene datos? {archivoExiste}");

            // Leer los personajes desde el archivo JSON
            List<Personaje> personajesLeidos = PersonajesJson.LeerPersonajes(nombreArchivo);

            // Mostrar los detalles de los personajes leídos
            // Console.WriteLine("Personajes leídos:");
            foreach (var personaje in personajesLeidos)
            {
                // Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
                // Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
                // Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
                // Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento.ToShortDateString()}");
                // Console.WriteLine($"Edad: {personaje.Datos.Edad}");
                // Console.WriteLine($"Encantamientos: {personaje.Caracteristicas.Encantamientos}");
                // Console.WriteLine($"Defensa: {personaje.Caracteristicas.Defensa}");
                // Console.WriteLine($"Pociones: {personaje.Caracteristicas.Pociones}");
                // Console.WriteLine($"Transformaciones: {personaje.Caracteristicas.Transformaciones}");
                // Console.WriteLine($"Adivinación: {personaje.Caracteristicas.Adivinacion}");
                // Console.WriteLine($"Salud: {personaje.Caracteristicas.Salud}");
                Console.WriteLine();
            }

            // Seleccionar dos personajes aleatorios que no sean el mismo
            Random random = new Random();
            Personaje personaje1 = personajesLeidos[random.Next(personajesLeidos.Count)];
            Personaje personaje2;

            do
            {
                personaje2 = personajesLeidos[random.Next(personajesLeidos.Count)];
            } while (personaje2 == personaje1);

            // Iniciar el combate
            Combate combate = new Combate(personaje1, personaje2);
            combate.IniciarCombate();

            Console.ReadLine();  // Espera a que el usuario presione Enter antes de salir
        }
    }
}
