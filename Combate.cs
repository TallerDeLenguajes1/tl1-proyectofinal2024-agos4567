using System;
using System.Collections.Generic;
using EspacioPersonaje;
using Fabrica;

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
            // Espacio adicional antes del nuevo combate
            Console.Clear();
            Console.WriteLine();

            // Anuncio de comienzo del combate
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("══════════════════════════════════════════════");
            Console.WriteLine("          ¡¡¡¡ COMIENZA EL COMBATE !!!!");
            Console.WriteLine("══════════════════════════════════════════════\n");
            Console.ResetColor();

            MostrarDetallesPersonajes();

            int turno = 0;

            while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0)
            {
                // Alternar entre ataque y defensa en cada turno
                if (turno % 2 == 0)
                {
                    // Colores alternos para los turnos
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    RealizarTurno(personaje1, personaje2);
                    Console.ResetColor();
                    if (personaje2.Caracteristicas.Salud <= 0) break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    RealizarTurno(personaje2, personaje1);
                    Console.ResetColor();
                    if (personaje1.Caracteristicas.Salud <= 0) break;
                }

                turno++;

                // Esperar a que se presione una tecla antes de continuar con el siguiente ataque
                Console.WriteLine("Presiona cualquier tecla para continuar con el siguiente ataque...");
                Console.ReadKey();
            }

            Personaje ganador = personaje1.Caracteristicas.Salud > 0 ? personaje1 : personaje2;
            MejorarHabilidades(ganador);

            // Anuncio del ganador
            Console.Clear();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("══════════════════════════════════════════════");
            Console.WriteLine("        ¡¡¡¡ COMBATE FINALIZADO !!!!");
            Console.WriteLine($"       ¡¡ El ganador es: {ganador.Datos.Nombre} !!");
            Console.WriteLine("══════════════════════════════════════════════");
            Console.ResetColor();

            return ganador;
        }

        private void MostrarDetallesPersonajes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Estadísticas de los personajes:");
            Console.ResetColor();

            // Mostrar detalles de personaje 1
            Console.WriteLine("╔════════════════════════════════╗");
            MostrarDetallesPersonaje(personaje1);
            Console.WriteLine("╚════════════════════════════════╝");

            // Espacio adicional antes del separador VS
            Console.WriteLine();

            // VS Separator
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("            VS                       ");
            Console.ResetColor();

            // Espacio adicional después del separador VS
            Console.WriteLine();

            // Mostrar detalles de personaje 2
            Console.WriteLine("╔════════════════════════════════╗");
            MostrarDetallesPersonaje(personaje2);
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine();
        }

        private void MostrarDetallesPersonaje(Personaje personaje)
        {
            Console.WriteLine($"{personaje.Datos.Nombre} (Salud: {personaje.Caracteristicas.Salud})");
            Console.WriteLine($"  Pociones: {personaje.Caracteristicas.Pociones}");
            Console.WriteLine($"  Defensa: {personaje.Caracteristicas.Defensa}");
            Console.WriteLine($"  Hechizos: {personaje.Caracteristicas.Hechizos}");
            Console.WriteLine($"  Nivel de Conocimiento Mágico: {personaje.Caracteristicas.Nivel}");
            Console.WriteLine($"  Transfiguraciones: {personaje.Caracteristicas.Transformaciones}");
        }

        // private void RealizarTurno(Personaje atacante, Personaje defensor)
        // {
        //     // Cálculo del Ataque
        //     int ataque = atacante.Caracteristicas.Pociones * atacante.Caracteristicas.Hechizos * atacante.Caracteristicas.Nivel;

        //     // Cálculo de la Efectividad
        //     int efectividad = random.Next(1, 101); // Valor aleatorio entre 1 y 100

        //     // Cálculo del Escudo (Defensa)
        //     int escudo = defensor.Caracteristicas.Defensa * defensor.Caracteristicas.Hechizos;

        //     // Constante de Ajuste
        //     const int constanteAjuste = 500;

        //     // Cálculo del Daño Provocado
        //     int danoProvocado = (ataque * efectividad - escudo) / constanteAjuste;

        //     // Asegúrate de que el daño sea al menos 2, ya que la constante de ajuste es muy grande y muchas veces solo me da 0 de daño 
        //     int MindanoProvocado = Math.Max(2, danoProvocado);

        //     // Actualizar la salud del defensor
        //     defensor.Caracteristicas.Salud -= MindanoProvocado;

        //     // Mostrar el ataque
        //     Console.WriteLine($"{atacante.Datos.Nombre} ataca a {defensor.Datos.Nombre} causando {MindanoProvocado} de daño.");
        //     // Console.WriteLine($"*** Ataque {ataque} y Efectividad {efectividad} ***");
        //     Console.WriteLine($"    Causa {MindanoProvocado} de daño. Salud restante de {defensor.Datos.Nombre}: {defensor.Caracteristicas.Salud}");

        //     // Calcular el daño que el defensor causa al atacante (opcional)
        //     // int danoContraAtacante = defensor.Caracteristicas.Defensa * defensor.Caracteristicas.Hechizos / 20; // Ajusta según tu lógica

        //     // Mostrar la preparación del defensor para el siguiente ataque
        //     Console.WriteLine($"{defensor.Datos.Nombre} se prepara...");
        // }
private void RealizarTurno(Personaje atacante, Personaje defensor)
{
    // Cálculo del Ataque
    int ataque = atacante.Caracteristicas.Pociones * atacante.Caracteristicas.Hechizos * atacante.Caracteristicas.Nivel;

    // Cálculo de la Efectividad
    int efectividad = random.Next(1, 101); // Valor aleatorio entre 1 y 100

    // Cálculo del Escudo (Defensa)
    int escudo = defensor.Caracteristicas.Defensa * defensor.Caracteristicas.Hechizos;

    // Constante de Ajuste
    const int constanteAjuste = 500;

    // Cálculo del Daño Provocado
    int danoProvocado = (ataque * efectividad - escudo) / constanteAjuste;

    // Asegúrate de que el daño sea al menos 2
    int MindanoProvocado = Math.Max(2, danoProvocado);

    // Actualizar la salud del defensor
    defensor.Caracteristicas.Salud -= MindanoProvocado;

    // Mostrar el ataque
    Console.Clear(); // Limpia la pantalla para una presentación más clara
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"🔥 {atacante.Datos.Nombre} lanza un ataque devastador contra {defensor.Datos.Nombre} causando {MindanoProvocado} de daño! 🔥");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"💥 Daño infligido: {MindanoProvocado} puntos.");
    Console.WriteLine($"❤️ Salud restante de {defensor.Datos.Nombre}: {defensor.Caracteristicas.Salud}");
    Console.ResetColor();

    // Mensaje divertido para la preparación del defensor
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n⚔️ {defensor.Datos.Nombre} está listo para el próximo movimiento. ¡No te descuides! ⚔️");
    Console.ResetColor();

    // Opcional: Pausa para ver el resultado
    // Console.ReadLine();
}
        private void MejorarHabilidades(Personaje ganador)
        {
            ganador.Caracteristicas.Salud += 10;
            ganador.Caracteristicas.Defensa += 5;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡{ganador.Datos.Nombre} ha ganado y mejora sus habilidades!");
            Console.WriteLine($"Salud: {ganador.Caracteristicas.Salud} (+10), Defensa: {ganador.Caracteristicas.Defensa} (+5)\n");
            Console.ResetColor();
        }
    }
}
