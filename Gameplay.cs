using System;
using System.Collections.Generic;
using EspacioPersonaje;
using Fabrica;

namespace ProyectoFinal
{
    public class Gameplay
    {
        public static void IniciarJuego()
        {
            // Crear una lista de personajes
            List<Personaje> personajes = new List<Personaje>();

            // Añadir personajes aleatorios a la lista
            for (int i = 0; i < 5; i++)
            {
                personajes.Add(FabricaDePersonajes.CrearPersonajeAleatorio());
            }

            // Simular un combate entre dos personajes
            if (personajes.Count >= 2)
            {
                Personaje p1 = personajes[0];
                Personaje p2 = personajes[1];

                Combate combate = new Combate(p1, p2);
                Personaje ganador = combate.IniciarCombate();

                // Mostrar los detalles del ganador
                Console.WriteLine($"Ganador: {ganador.Datos.Nombre}");
            }

            Console.ReadLine();  // Espera a que el usuario presione Enter antes de salir
        }
    }
}
