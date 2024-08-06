using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EspacioPersonaje; // Asegúrate de tener el espacio de nombres correcto donde está definido Personaje

public static class PersonajesJson
{
    public static void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        try
        {
            // Convertir la lista de personajes a JSON
            string json = JsonSerializer.Serialize(personajes, new JsonSerializerOptions { WriteIndented = true });
            
            // Guardar el JSON en el archivo especificado
            File.WriteAllText(nombreArchivo, json);

            Console.WriteLine("Personajes guardados correctamente en el archivo.");
        }
        catch (Exception ex)
        {
            // Capturar cualquier excepción y mostrar un mensaje de error
            Console.WriteLine($"Error al guardar los personajes: {ex.Message}");
        }
    }

    public static bool ExisteArchivo(string nombreArchivo)
    {
        return File.Exists(nombreArchivo);
    }

    public static List<Personaje> LeerPersonajes(string nombreArchivo)
    {
        try
        {
            string json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Personaje>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer los personajes: {ex.Message}");
            return new List<Personaje>();
        }
    }
}
