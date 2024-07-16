using System;
using EspacioPersonaje;  // Asegúrate de que esté incluido el namespace EspacioPersonaje
using Fabrica;

namespace ProyectoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un personaje aleatorio usando la fábrica de personajes
           Personaje personaje = FabricaDePersonajes.CrearPersonajeAleatorio();


            // Mostrar los detalles del personaje creado
            Console.WriteLine("Personaje creado:");
            Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
            Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
            Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
            Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento.ToShortDateString()}");
            Console.WriteLine($"Edad: {personaje.Datos.Edad}");
            Console.WriteLine($"Encantamientos - Aprendizaje y aplicación de hechizos.: {personaje.Caracteristicas.Encantamientos}");
            Console.WriteLine($"Defensa Contra las Artes Oscuras: {personaje.Caracteristicas.Defensa}");
            Console.WriteLine($"Pociones - Preparación y efectos de pociones mágicas: {personaje.Caracteristicas.Pociones}");
            Console.WriteLine($"Transformaciones - Cambio de formas y transformación de objetos: {personaje.Caracteristicas.Transformaciones}");
            Console.WriteLine($"Adivinación - Predicción del futuro mediante métodos mágicos : {personaje.Caracteristicas.Adivinacion}");
            Console.WriteLine($"Salud: {personaje.Caracteristicas.Salud}");

            Console.ReadLine();  // Espera a que el usuario presione Enter antes de salir
        }
    }
}
