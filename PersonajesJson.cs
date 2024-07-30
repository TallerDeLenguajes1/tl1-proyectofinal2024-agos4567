using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EspacioPersonaje
{
    public class PersonajesJson
    {
        // Método para guardar una lista de personajes en un archivo JSON
        public static void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
        {
            try
            {
                // Serializar la lista de personajes en formato JSON
                string json = JsonSerializer.Serialize(personajes, new JsonSerializerOptions { WriteIndented = true });

                // Guardar el JSON en un archivo
                File.WriteAllText(nombreArchivo, json);
                // Console.WriteLine("Personajes guardados correctamente en el archivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar los personajes: {ex.Message}");
            }
        }

        // Método para leer una lista de personajes desde un archivo JSON
        public static List<Personaje> LeerPersonajes(string nombreArchivo)
        {
            try
            {
                // Leer el contenido del archivo
                string json = File.ReadAllText(nombreArchivo);

                // Deserializar el JSON en una lista de personajes
                List<Personaje> personajes = JsonSerializer.Deserialize<List<Personaje>>(json);
                return personajes;
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"Error al leer los personajes: {ex.Message}");
                return new List<Personaje>(); // Retorna una lista vacía en caso de error
            }
        }

        // Método para verificar si un archivo existe y tiene datos
        public static bool Existe(string nombreArchivo)
        {
            try
            {
                // Verificar si el archivo existe y tiene datos
                if (File.Exists(nombreArchivo))
                {
                    // Leer el contenido del archivo
                    string contenido = File.ReadAllText(nombreArchivo);

                    // Retornar true si el contenido no está vacío
                    return !string.IsNullOrWhiteSpace(contenido);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar el archivo: {ex.Message}");
                return false;
            }
        }
    }
}
