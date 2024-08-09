using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fabrica
{





    public class FabricaDePersonajes
    {
        private static readonly Random random = new Random();
        private static readonly PotterApi potterApi = new PotterApi();

        // Método para obtener los primeros 10 personajes desde la API
        public static async Task<List<Character>> ObtenerPersonajesDesdeApi()
        {
            string url = "https://hp-api.onrender.com/api/characters/students";
            List<Character> personajesApi = await potterApi.ObtenerPersonajesAsync(url);

            // Obtener los primeros 10 personajes
            var primeros10Personajes = personajesApi.Take(10).ToList();

            // Asignar una fecha aleatoria a los personajes que no tienen fecha de nacimiento
            foreach (var personaje in primeros10Personajes)
            {
                if (string.IsNullOrEmpty(personaje.DateOfBirth))
                {
                    personaje.DateOfBirth = ObtenerFechaAleatoria().ToString("dd-MM-yyyy");
                }
            }

            return primeros10Personajes;
        }


        // Método para convertir personajes de la API a objetos Personaje
       //

        public static List<EspacioPersonaje.Personaje> ConvertirAReturnPersonajes(List<Character> characterPersonajes)
        {
            var personajes = new List<EspacioPersonaje.Personaje>();

            foreach (var p in characterPersonajes)
            {
                if (!DateTime.TryParse(p.DateOfBirth, out DateTime fechaNacimiento))
                {
                    fechaNacimiento = DateTime.MinValue;
                }

                var datos = new EspacioPersonaje.Datos
                {
                    Tipo = p.House,
                    Nombre = p.Name,
                    Apodo = ObtenerApodoAleatorio(),
                    FechaNacimiento = fechaNacimiento,
                    Gender = p.Gender,
                    Ancestry = p.Ancestry,
                    Imagen = p.Image
                };

                var caracteristicas = CrearCaracteristicasAleatorias();

                personajes.Add(new EspacioPersonaje.Personaje(datos, caracteristicas));
            }

            return personajes;
        }

        private static DateTime ObtenerFechaAleatoria()
        {
            int year = random.Next(1980, 2010);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }




    private static EspacioPersonaje.Caracteristicas CrearCaracteristicasAleatorias()
{
    return new EspacioPersonaje.Caracteristicas
    {
        Pociones = random.Next(1, 6),
        Defensa = random.Next(1, 11),
        Hechizos = random.Next(1, 11),
        Nivel = random.Next(1, 11),
        Transformaciones = random.Next(1, 11),
        Salud = 100
    };
}



        private static string ObtenerApodoAleatorio()
        {
            string[] apodos = { "Sabio", "Valiente", "Poderoso", "Justiciero", "Audaz", "Noble" };
            return apodos[random.Next(apodos.Length)];
        }
    }
}
