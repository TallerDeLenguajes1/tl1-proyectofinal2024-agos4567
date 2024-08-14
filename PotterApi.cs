using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fabrica
{
    public class PotterApi
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<Character>> ObtenerPersonajesAsync(string url)
        {
            try
            {
                // se realiza una solicitud HTTP a la URL proporcionada y deserializa la respuesta JSON en una lista de objetos Character.
                var response = await client.GetStringAsync(url);

                // Deserializar la respuesta JSON a una lista de objetos Character
                var personajes = JsonSerializer.Deserialize<List<Character>>(response);

                // Asegurarse de que no sea nulo y retornar una lista vacía si es nulo
                return personajes ?? new List<Character>();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en caso de errores de solicitud o deserialización
                Console.WriteLine($"Error al obtener personajes: {ex.Message}");
                return new List<Character>(); // Devuelve una lista vacía en caso de error
            }
        }
    }
}

