using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EspacioPersonaje;
using Fabrica;

namespace ProyectoFinal
{
    class Program
    {
        private const string ArchivoPersonajes = "personajes.json";
        private const string ArchivoHistorial = "historial.json";

        static async Task Main(string[] args)
        {
            List<Personaje> personajes = await Inicializar(); // Captura el valor devuelto por Inicializar

            while (true)
            {
                MostrarMenuPrincipal();

                string? opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        MostrarPersonajes(personajes);
                        break;
                    case "2":
                        SeleccionarPersonajeYCombatir(personajes);
                        break;
                    case "3":
                        return; // Salir del programa
                    default:
                        Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                        break;
                }
            }
        }

        private static async Task<List<Personaje>> Inicializar()
        {
            List<Personaje> personajes;

            if (PersonajesJson.ExisteArchivo(ArchivoPersonajes))
            {
                personajes = PersonajesJson.LeerPersonajes(ArchivoPersonajes);
            }
            else
            {
                List<Character> personajesApi = await FabricaDePersonajes.ObtenerPersonajesDesdeApi();
                personajes = FabricaDePersonajes.ConvertirAReturnPersonajes(personajesApi);
                PersonajesJson.GuardarPersonajes(personajes, ArchivoPersonajes);
            }

            return personajes;
        }

private static void MostrarMenuPrincipal()
{
    Console.Clear();

    // Definir el contenido del título y del menú
    string titulo = "🌟 MENÚ PRINCIPAL 🌟";
    string[] menuOptions = new[]
    {
        "1. 👤 Mostrar Personajes",
        "2. ⚔️ Combatir",
        "3. ❌ Salir"
    };

    // Calcular el ancho de la consola
    int consoleWidth = Console.WindowWidth;

    // Centrar el título del menú
    string tituloLinea = "═════════════════════════════════════════";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine();
    Console.WriteLine(tituloLinea.PadLeft((consoleWidth + tituloLinea.Length) / 2));
    Console.WriteLine(titulo.PadLeft((consoleWidth + titulo.Length) / 2));
    Console.WriteLine(tituloLinea.PadLeft((consoleWidth + tituloLinea.Length) / 2));
    Console.ResetColor();

    // Recuadro para las opciones del menú
    string frameTop = "╔═════════════════════════════════════════╗";
    string frameBottom = "╚═════════════════════════════════════════╝";
    string framePadding = "║";
    int frameWidth = frameTop.Length;

    // Imprimir el recuadro centrado
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine();
    Console.WriteLine(frameTop.PadLeft((consoleWidth + frameWidth) / 2));
    foreach (var option in menuOptions)
    {
        Console.WriteLine($"{framePadding} {option.PadRight(frameWidth - 4)}{framePadding}".PadLeft((consoleWidth + frameWidth) / 2));
    }
    Console.WriteLine($"{framePadding}{" ".PadRight(frameWidth - 4)}{framePadding}".PadLeft((consoleWidth + frameWidth) / 2)); // Espacio adicional al final
    Console.WriteLine(frameBottom.PadLeft((consoleWidth + frameWidth) / 2));
    Console.ResetColor();

    Console.Write("Seleccione una opción: ");
}




private static void MostrarPersonajes(List<Personaje> personajes)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine("          📜 LISTA DE PERSONAJES          ");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();

    foreach (var personaje in personajes)
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
         Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
        Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
        Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
        Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento:dd-MM-yyyy}");
        Console.WriteLine($"Género: {personaje.Datos.Gender}");
        Console.WriteLine($"Raza: {personaje.Datos.Ancestry}");
        // Console.WriteLine($"Imagen: {personaje.Datos.Imagen}");
        Console.WriteLine("─────────────────────────────────────────");
        Console.ResetColor();
    }

    Console.WriteLine("Presione una tecla para volver al menú principal.");
    Console.ReadKey();
}

private static void SeleccionarPersonajeYCombatir(List<Personaje> personajes)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine("       🔥 ¡SELECCIONA UN PERSONAJE! 🔥");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();

    for (int i = 0; i < personajes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {personajes[i].Datos.Nombre}");
    }

    Console.Write("Ingrese el número del personaje: ");
    if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion >= 1 && seleccion <= personajes.Count)
    {
        Personaje personajeElegido = personajes[seleccion - 1];
        Personaje personajeOponente = ObtenerOponenteAleatorio(personajeElegido, personajes);

        if (personajeOponente != null)
        {
            MostrarInicioCombate();

            Console.WriteLine($"Seleccionaste a {personajeElegido.Datos.Nombre} para combatir.");
            Console.WriteLine($"Tu oponente será {personajeOponente.Datos.Nombre}.");

            // Iniciar el combate
            Combate combate = new Combate(personajeElegido, personajeOponente);
            Personaje ganador = combate.IniciarCombate();

            if (ganador != null)
            {
                // Guardar el ganador en el historial
                HistorialJson.GuardarGanador(ganador, ArchivoHistorial);
                Console.WriteLine($"El ganador es {ganador.Datos.Nombre} y ha sido guardado en el historial.");
            }

            Console.WriteLine("Presione una tecla para volver al menú principal.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("No se pudo encontrar un oponente.");
            Console.ReadKey();
        }
    }
    else
    {
        Console.WriteLine("Selección no válida.");
        Console.ReadKey();
    }
}


 private static void MostrarInicioCombate()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine("      ⚔️ ¡¡¡¡ COMIENZA EL COMBATE !!!! ⚔️");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();
    Console.WriteLine();
}


        private static Personaje ObtenerOponenteAleatorio(Personaje personajeElegido, List<Personaje> personajes)
        {
            Random random = new Random();
            List<Personaje> posiblesOponentes = new List<Personaje>(personajes);
            posiblesOponentes.Remove(personajeElegido);

            if (posiblesOponentes.Count > 0)
            {
                return posiblesOponentes[random.Next(posiblesOponentes.Count)];
            }
            return null; // Aquí puedes manejar el caso de null en el llamado a este método
        }
    }
}
