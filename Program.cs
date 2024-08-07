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
    List<Personaje> personajes = await Inicializar();
    bool continuar = true;

    while (continuar)
    {
        MostrarMenuPrincipal();

        if (int.TryParse(Console.ReadLine(), out int opcion))
        {
            switch (opcion)
            {
                case 1:
                    MostrarPersonajes(personajes);
                    break;
                case 2:
                    SeleccionarPersonajeYCombatir(personajes);
                    break;
                case 3:
                    MostrarHistorialGanadores();
                    break;
                case 4:
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Entrada no válida.");
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

    string encabezado = "     🌟 MENÚ PRINCIPAL 🌟";
    string borde = "═════════════════════════════════════════";
    string opcion1 = "1. 👤 Mostrar Personajes";
    string opcion2 = "2. ⚔️ Combatir";
    string opcion3 = "3. 🏆 Mostrar Historial de Ganadores";
    string opcion4 = "4. ❌ Salir";

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(CentrarTexto(borde, anchoConsola));
    Console.WriteLine(CentrarTexto(encabezado, anchoConsola));
    Console.WriteLine(CentrarTexto(borde, anchoConsola));
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine(CentrarTexto(opcion1, anchoConsola));
    Console.WriteLine(); 
    Console.WriteLine(CentrarTexto(opcion2, anchoConsola));
    Console.WriteLine(); 
    Console.WriteLine(CentrarTexto(opcion3, anchoConsola));
    Console.WriteLine(); 
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
        if (personajeElegido != null)
        {
            IniciarCombatePersonaje(personajeElegido, personajes);
        }
    }
    else
    {
        Console.WriteLine("Selección no válida.");
        Console.ReadKey();
    }
}

private static void IniciarCombatePersonaje(Personaje personajeElegido, List<Personaje> personajes)
{
    while (personajeElegido.Caracteristicas.Salud > 0)
    {
        Personaje personajeOponente = ObtenerOponenteAleatorio(personajeElegido, personajes);
        if (personajeOponente == null)
        {
            Console.WriteLine("No hay más oponentes disponibles. ¡Has ganado el juego!");
            return;
        }

        MostrarInicioCombate();
        Console.ReadKey();

        Combate combate = new Combate(personajeElegido, personajeOponente);
        Personaje ganador = combate.IniciarCombate();

        if (ganador == personajeElegido)
        {
            personajes.Remove(personajeOponente);
            MostrarMensajeVictoria(personajeElegido);
        }
        else
        {
            MostrarMensajeDerrota(personajeElegido);
            return;
        }

        HistorialJson.GuardarGanador(ganador, ArchivoHistorial);
    }
}

private static void MostrarMensajeVictoria(Personaje personajeGanador)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine($"🎉 ¡{personajeGanador.Datos.Nombre} HA GANADO LA BATALLA! 🎉");
    Console.WriteLine($"👊 {personajeGanador.Datos.Nombre} se prepara para la siguiente batalla.");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();
    Console.WriteLine("Presione una tecla para continuar.");
    Console.ReadKey();
}

private static void MostrarMensajeDerrota(Personaje personajePerdedor)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine($"💥 ¡{personajePerdedor.Datos.Nombre} HA SIDO DERROTADO! 💥");
    Console.WriteLine("🛑 Fin del juego. ¡Gracias por jugar!");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();
    Console.WriteLine("Presione una tecla para salir.");
    Console.ReadKey();
}


private static void MostrarInicioCombate()
{
    Console.Clear();
    
    int consoleWidth = Console.WindowWidth;
    string textoBanner = "⚔️ ¡¡¡¡ COMIENZA EL COMBATE !!!! ⚔️";
    int borderLength = consoleWidth;
    
    string borde = new string('=', borderLength);
    string textoCentrado = textoBanner.PadLeft((borderLength + textoBanner.Length) / 2);

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(borde);
    Console.WriteLine(textoCentrado);
    Console.WriteLine(borde);
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
    return null;
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
