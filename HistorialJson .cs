using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EspacioPersonaje
{
    public class HistorialJson
    {


        // GuardarGanador: Guarda el personaje ganador y la información relevante en un archivo JSON
        public static void GuardarGanador(Personaje ganador, string nombreArchivo)
        {
            var ganadores = LeerGanadores(nombreArchivo) ?? new List<Personaje>();

            ganadores.Add(ganador);

            // Serializar la lista de ganadores en JSON y guardar en archivo
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(ganadores, opciones);
            File.WriteAllText(nombreArchivo, jsonString);
        }

 }
         
}
