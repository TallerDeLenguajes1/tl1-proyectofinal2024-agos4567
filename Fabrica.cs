using System;

namespace Fabrica
{
    public class FabricaDePersonajes
    {
        private static Random random = new Random();

        public static EspacioPersonaje.Personaje CrearPersonajeAleatorio()
        {
            string[] nombres = { "Harry Potter", "Hermione Granger", "Ron Weasley", "Draco Malfoy", "Severus Snape", "Tom Riddle (Lord Voldemort)", "Bellatrix Lestrange", "Luna Lovegood", "Cedric Diggory" };
            string nombre = nombres[random.Next(nombres.Length)].Trim(); // Trim para eliminar espacios en blanco

            // Determinación de la casa y asignación de características según el nombre seleccionado
            EspacioPersonaje.Hogwarts casa = DeterminarCasaYCaracteristicas(nombre);

            // Crear datos y características para el personaje
            EspacioPersonaje.Datos datos = new EspacioPersonaje.Datos
            {
                Tipo = casa.ToString(),
                Nombre = nombre,
                Apodo = ObtenerApodoAleatorio(),
                FechaNacimiento = GenerarFechaAleatoria()
            };

            datos.Edad = CalcularEdad(datos.FechaNacimiento);

            EspacioPersonaje.Caracteristicas caracteristicas = new EspacioPersonaje.Caracteristicas();
            AsignoCaracteristicas(caracteristicas);

            Console.WriteLine($"Nombre: {nombre}, Casa (después del switch): {casa}");

            return new EspacioPersonaje.Personaje(datos, caracteristicas);
        }

        private static EspacioPersonaje.Hogwarts DeterminarCasaYCaracteristicas(string nombre)
        {
            switch (nombre)
            {
                case "Harry Potter":
                case "Hermione Granger":
                case "Ron Weasley":
                    return AsignarCaracteristicasYDevolverCasa(EspacioPersonaje.Hogwarts.Gryffindor);
                case "Cedric Diggory":
                    return AsignarCaracteristicasYDevolverCasa(EspacioPersonaje.Hogwarts.Hufflepuff);
                case "Luna Lovegood":
                    return AsignarCaracteristicasYDevolverCasa(EspacioPersonaje.Hogwarts.Ravenclaw);
                case "Draco Malfoy":
                case "Severus Snape":
                case "Tom Riddle (Lord Voldemort)":
                    return AsignarCaracteristicasYDevolverCasa(EspacioPersonaje.Hogwarts.Slytherin);
                default:
                    throw new ArgumentException($"Nombre de personaje no válido: {nombre}");
            }
        }

        private static EspacioPersonaje.Hogwarts AsignarCaracteristicasYDevolverCasa(EspacioPersonaje.Hogwarts casa)
        {
            EspacioPersonaje.Caracteristicas caracteristicas = new EspacioPersonaje.Caracteristicas();
            AsignoCaracteristicas(caracteristicas);
            return casa;
        }

        private static DateTime GenerarFechaAleatoria()
        {
            DateTime inicio = new DateTime(1900, 1, 1);
            int rango = (DateTime.Today - inicio).Days;
            return inicio.AddDays(random.Next(rango));
        }

        private static int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }

        private static void AsignoCaracteristicas(EspacioPersonaje.Caracteristicas caracteristicas)
        {
            caracteristicas.Encantamientos = random.Next(20, 71);
            caracteristicas.Salud = 100;
            caracteristicas.Defensa = random.Next(20, 71);
            caracteristicas.Pociones = random.Next(20, 71);
            caracteristicas.Transformaciones = random.Next(20, 71);
            caracteristicas.Adivinacion = random.Next(20, 71);
        }

        private static string ObtenerApodoAleatorio()
        {
            string[] apodos = { "El Sabio", "El Valiente", "El Rápido", "El Poderoso", "El Justiciero" };
            return apodos[random.Next(apodos.Length)];
        }
    }
}
