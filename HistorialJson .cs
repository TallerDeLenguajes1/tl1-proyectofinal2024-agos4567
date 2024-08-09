using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EspacioPersonaje;

namespace ProyectoFinal
{
    public static class HistorialJson
    {
        // Método para guardar el ganador en un archivo JSON
        public static void GuardarGanador(Personaje ganador, string nombreArchivo)
        {
            try
            {
                List<Personaje> ganadores = new List<Personaje>();

                if (Existe(nombreArchivo))
                {
                    ganadores = LeerGanadores(nombreArchivo);
                }

                ganadores.Add(ganador);

                string json = JsonSerializer.Serialize(ganadores, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(nombreArchivo, json);

                Console.WriteLine("Ganador guardado correctamente en el archivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el ganador: {ex.Message}");
            }
        }

        // Método para leer los ganadores desde un archivo JSON
        public static List<Personaje> LeerGanadores(string nombreArchivo)
        {
            try
            {
                string json = File.ReadAllText(nombreArchivo);
                // Asegúrate de que la deserialización no devuelva null
                return JsonSerializer.Deserialize<List<Personaje>>(json) ?? new List<Personaje>();
            }
            catch (Exception ex)
            {
                // Loguear el error (opcional) y devolver una lista vacía
                Console.WriteLine($"Error al leer los ganadores: {ex.Message}");
                return new List<Personaje>();
            }
        }

        // Método para verificar si el archivo existe y tiene datos
        public static bool Existe(string nombreArchivo)
        {
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
    }
}
