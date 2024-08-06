using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EspacioPersonaje
{
    public class HistorialJson
    {
        // GuardarGanador: Guarda el personaje ganador y la información relevante en un archivo JSON
        // public static void GuardarGanador(Personaje ganador, string nombreArchivo)
        // {
        //     var ganadores = LeerGanadores(nombreArchivo) ?? new List<Personaje>();

        //     ganadores.Add(ganador);

        //     // Serializar la lista de ganadores en JSON y guardar en archivo
        //     var opciones = new JsonSerializerOptions { WriteIndented = true };
        //     string jsonString = JsonSerializer.Serialize(ganadores, opciones);
        //     File.WriteAllText(nombreArchivo, jsonString);
        // }

        // // LeerGanadores: Lee el archivo JSON y retorna la lista de personajes ganadores
        // public static List<Personaje> LeerGanadores(string nombreArchivo)
        // {
        //     if (!File.Exists(nombreArchivo) || new FileInfo(nombreArchivo).Length == 0)
        //         return new List<Personaje>();

        //     // Leer el archivo JSON y deserializar en una lista de personajes
        //     string jsonString = File.ReadAllText(nombreArchivo);
        //     return JsonSerializer.Deserialize<List<Personaje>>(jsonString);
        // }

        // // Existe: Verifica si el archivo existe y contiene datos
        // public static bool Existe(string nombreArchivo)
        // {
        //     return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        // }
    }
}
