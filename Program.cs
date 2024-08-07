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
                MostrarHistorialGanadores();
                break;
            case "4":
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
    
    int anchoConsola = Console.WindowWidth;

    // Textos para el menú
    string encabezado = "     🌟 MENÚ PRINCIPAL 🌟";
    string borde = "═════════════════════════════════════════";
    string opcion1 = "1. 👤 Mostrar Personajes";
    string opcion2 = "2. ⚔️ Combatir";
    string opcion3 = "3. 🏆 Mostrar Historial de Ganadores";
    string opcion4 = "4. ❌ Salir";

    // Imprime el borde superior
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(CentrarTexto(borde, anchoConsola));
    Console.WriteLine(CentrarTexto(encabezado, anchoConsola));
    Console.WriteLine(CentrarTexto(borde, anchoConsola));
    Console.ResetColor();

    // Imprime las opciones con borde inferior
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine(CentrarTexto(opcion1, anchoConsola));
    Console.WriteLine(); // Espacio
    Console.WriteLine(CentrarTexto(opcion2, anchoConsola));
    Console.WriteLine(); // Espacio
    Console.WriteLine(CentrarTexto(opcion3, anchoConsola));
    Console.WriteLine(); // Espacio
    Console.WriteLine(CentrarTexto(opcion4, anchoConsola));
    Console.WriteLine(CentrarTexto(borde, anchoConsola));
    Console.ResetColor();
    
    Console.Write("Seleccione una opción: ");
}

private static string CentrarTexto(string texto, int ancho)
{
    int padding = (ancho - texto.Length) / 2;
    return new string(' ', padding) + texto + new string(' ', padding);
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
    Console.WriteLine("        🏰 ¡SELECCIONA UN PERSONAJE!  🏰");
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
             Console.Clear();
            MostrarInicioCombate();
             Console.ReadKey();

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
    
    // Calcular el ancho de la consola
    int consoleWidth = Console.WindowWidth;
    
    // Definir el contenido del banner
    string textoBanner = "⚔️ ¡¡¡¡ COMIENZA EL COMBATE !!!! ⚔️";
    int borderLength = consoleWidth;
    
    // Crear bordes
    string borde = new string('=', borderLength);
    
    // Centrar el banner
    string textoCentrado = textoBanner.PadLeft((borderLength + textoBanner.Length) / 2);

    // Mostrar el banner
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(borde);
    Console.WriteLine(textoCentrado);
    Console.WriteLine(borde);
    Console.ResetColor();
    Console.WriteLine(); // Espacio adicional después del banner
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


       private static void MostrarHistorialGanadores()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine("     🏆 RANKING HISTÓRICO DE GANADORES 🏆");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();
    
    List<Personaje> ganadores = HistorialJson.LeerGanadores("historial.json");
    
    if (ganadores.Count > 0)
    {     
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║   GANADOR(S) ANTERIOR(ES)               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.ResetColor();
        
        foreach (var ganador in ganadores)
        {   
            Console.WriteLine($"Nombre: {ganador.Datos.Nombre}");
            Console.WriteLine($"Casa: {ganador.Datos.Tipo}");
            Console.WriteLine($"Apodo: {ganador.Datos.Apodo}");
            // Console.WriteLine($"Fecha de Nacimiento: {ganador.Datos.FechaNacimiento:dd-MM-yyyy}");
            // Console.WriteLine($"Género: {ganador.Datos.Gender}");
            // Console.WriteLine($"Ancestry: {ganador.Datos.Ancestry}");
            // Console.WriteLine($"Imagen: {ganador.Datos.Imagen}");
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("No hay ganadores registrados.");
    }
    
    Console.WriteLine("Presione una tecla para volver al menú principal.");
    Console.ReadKey();
}



    }
}
