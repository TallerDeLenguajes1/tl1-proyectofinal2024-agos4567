using System;
using EspacioPersonaje;

namespace ProyectoFinal
{
    public class Combate
    {
        private Personaje personaje1;
        private Personaje personaje2;
        private Random random = new Random();

        public Combate(Personaje p1, Personaje p2)
        {
            personaje1 = p1;
            personaje2 = p2;
        }

        public Personaje IniciarCombate()
        {
            Console.WriteLine("===== ¡Comienza el Combate! =====\n");

            MostrarDetallesPersonajes();

            string[] caracteristicas = { "Encantamientos", "Defensa", "Pociones", "Transformaciones", "Adivinacion" };
            int turno = 0;

            while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0)
            {
                string caracteristicaActual = caracteristicas[turno % caracteristicas.Length];
                RealizarTurno(personaje1, personaje2, caracteristicaActual);
                if (personaje2.Caracteristicas.Salud <= 0) break;

                RealizarTurno(personaje2, personaje1, caracteristicaActual);
                turno++;

                // Esperar a que se presione una tecla antes de continuar con el siguiente ataque
                Console.WriteLine("Presiona cualquier tecla para continuar con el siguiente ataque...");
                Console.ReadKey();
            }

            Personaje ganador = personaje1.Caracteristicas.Salud > 0 ? personaje1 : personaje2;
            MejorarHabilidades(ganador);

            Console.WriteLine("\n===== ¡Combate Finalizado! =====");
            Console.WriteLine($"¡El ganador es: {ganador.Datos.Nombre}!\n");

            return ganador;
        }

        private void MostrarDetallesPersonajes()
        {
            Console.WriteLine("Detalles de los personajes:");
            MostrarDetallesPersonaje(personaje1);
            Console.WriteLine($"VS");
            MostrarDetallesPersonaje(personaje2);
            Console.WriteLine();
        }

        private void MostrarDetallesPersonaje(Personaje personaje)
        {
            Console.WriteLine($"{personaje.Datos.Nombre} (Salud: {personaje.Caracteristicas.Salud})");
            Console.WriteLine($"  Encantamientos: {personaje.Caracteristicas.Encantamientos}");
            Console.WriteLine($"  Defensa: {personaje.Caracteristicas.Defensa}");
            Console.WriteLine($"  Pociones: {personaje.Caracteristicas.Pociones}");
            Console.WriteLine($"  Transformaciones: {personaje.Caracteristicas.Transformaciones}");
            Console.WriteLine($"  Adivinación: {personaje.Caracteristicas.Adivinacion}");
        }

        private void RealizarTurno(Personaje atacante, Personaje defensor, string caracteristica)
        {
            int habilidadAtacante = ObtenerValorCaracteristica(atacante, caracteristica);
            int habilidadDefensor = ObtenerValorCaracteristica(defensor, caracteristica);

            int danioBase = random.Next(10, 21);
            int danioModificado = danioBase + (habilidadAtacante / 10) - (habilidadDefensor / 10);

            // Asegurarse de que el daño esté entre 5 y 20
            int danioFinal = Math.Max(5, Math.Min(20, danioModificado));

            defensor.Caracteristicas.Salud -= danioFinal;

            Console.WriteLine($"--> {atacante.Datos.Nombre} ataca a {defensor.Datos.Nombre} usando {caracteristica} ({habilidadAtacante})");
            Console.WriteLine($"    Causa {danioFinal} de daño. Salud restante de {defensor.Datos.Nombre}: {defensor.Caracteristicas.Salud}");
        }

        private int ObtenerValorCaracteristica(Personaje personaje, string caracteristica)
        {
            switch (caracteristica)
            {
                case "Encantamientos":
                    return personaje.Caracteristicas.Encantamientos;
                case "Defensa":
                    return personaje.Caracteristicas.Defensa;
                case "Pociones":
                    return personaje.Caracteristicas.Pociones;
                case "Transformaciones":
                    return personaje.Caracteristicas.Transformaciones;
                case "Adivinacion":
                    return personaje.Caracteristicas.Adivinacion;
                default:
                    throw new ArgumentException($"Característica no válida: {caracteristica}");
            }
        }

        private void MejorarHabilidades(Personaje ganador)
        {
            ganador.Caracteristicas.Salud += 10;
            ganador.Caracteristicas.Defensa += 5;
            Console.WriteLine($"¡{ganador.Datos.Nombre} ha ganado y mejora sus habilidades!");
            Console.WriteLine($"Salud: {ganador.Caracteristicas.Salud} (+10), Defensa: {ganador.Caracteristicas.Defensa} (+5)\n");
        }
    }
}
