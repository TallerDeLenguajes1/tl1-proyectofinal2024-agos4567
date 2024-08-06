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

            //Realiza una solicitud HTTP a la URL proporcionada y deserializa la respuesta JSON en una lista de objetos Character.
            var response = await client.GetStringAsync(url);
            List<Character> personajes = JsonSerializer.Deserialize<List<Character>>(response);
            return personajes;
        }
    }
}
