
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

       //constructor de la clase combate
        public Combate(Personaje p1, Personaje p2)
        {
            personaje1 = p1;
            personaje2 = p2;
        }


   //maneja el combate en sí mismo, gestionando los turnos entre los personajes y determinando el ganador.
        public Personaje IniciarCombate()
        {
          




         //variable para llevar un control de los turnos
            int turno = 0;
          // Mientras ambos personajes tengan salud, el combate continúa.
            while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0)
            {
                // Alternar entre ataque y defensa en cada turno
                if (turno % 2 == 0)
                {
                    // Colores alternos para los turnos
                    Console.ForegroundColor = ConsoleColor.Magenta;


                      // Realiza un turno de combate donde personaje1 ataca a personaje2. 
                    RealizarTurno(personaje1, personaje2);
                    Console.ResetColor();


                     // Si el personaje2 ha quedado sin salud, el combate termina.
                    if (personaje2.Caracteristicas.Salud <= 0) break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                // Realiza un turno de combate donde personaje2 ataca a personaje1.
                    RealizarTurno(personaje2, personaje1);
                    Console.ResetColor();

                     // Si el personaje1 ha quedado sin salud, el combate termina.
                    if (personaje1.Caracteristicas.Salud <= 0) break;
                }


                   // se incrementa el número de turno.     
                turno++;

                // Esperar a que se presione una tecla antes de continuar con el siguiente ataque
                Console.WriteLine("Presiona cualquier tecla para continuar con el siguiente ataque...");
                Console.ReadKey();
            }
            

            // se determina el ganador basado en cuál personaje todavía tiene salud.
           //operador ternario, para ver quien de los personajes es el ganador
            Personaje ganador = personaje1.Caracteristicas.Salud > 0 ? personaje1 : personaje2;


            // Mejora las habilidades del personaje ganador
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






//se realizan los calculos, para daño 
private void RealizarTurno(Personaje atacante, Personaje defensor)
{
    // Cálculo del Ataque
    int ataque = atacante.Caracteristicas.Pociones * atacante.Caracteristicas.Hechizos * atacante.Caracteristicas.Nivel;

    // Cálculo de la Efectividad
    int efectividad = random.Next(1, 101); // Valor aleatorio entre 1 y 100

    // Cálculo del Escudo (Defensa)
    int escudo = defensor.Caracteristicas.Defensa * defensor.Caracteristicas.Hechizos;

    // Constante de Ajuste
    const int constanteAjuste = 700;

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
     Console.ReadKey();

    Console.WriteLine();
    Console.WriteLine($"{defensor.Datos.Nombre} 🛡️ LANZA UN HECHIZO PARA DEFENDERSE! 🔮");
    Console.ResetColor();
  
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"💥 Daño total infligido: {MindanoProvocado} puntos.");
    Console.WriteLine($"❤️ Salud restante de {defensor.Datos.Nombre}: {defensor.Caracteristicas.Salud}");
    Console.ResetColor();
    Console.ReadKey();

    // Mensaje divertido para la preparación del defensor
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n⚔️ {defensor.Datos.Nombre} está listo para el próximo movimiento. ¡No te descuides! ⚔️");
    Console.ResetColor();

    // Opcional: Pausa para ver el resultado
    // Console.ReadLine();
}



     
    private void MejorarHabilidades(Personaje ganador){
                    
     ganador.Caracteristicas.Salud += 10;
      ganador.Caracteristicas.Defensa += 5;
     Console.ForegroundColor = ConsoleColor.Green;
     Console.WriteLine($"¡{ganador.Datos.Nombre} ha ganado y mejora sus habilidades!");
     Console.WriteLine($"Salud: {ganador.Caracteristicas.Salud} (+10), Defensa: {ganador.Caracteristicas.Defensa} (+5)\n");
     Console.ResetColor();
     }





    }
}
