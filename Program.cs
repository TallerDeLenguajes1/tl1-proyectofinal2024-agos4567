using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EspacioPersonaje;
using Fabrica;

namespace ProyectoFinal
{
    class Program
    {

        //almacenan el nombre del archivo JSON donde se guardan y leen los datos de los personajes
        private const string ArchivoPersonajes = "personajes.json";

         //almacenan el nombre del archivo JSON que contiene el historial de los ganadores de las batllas
        private const string ArchivoHistorial = "historial.json";

       static async Task Main(string[] args){


    //
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





//se encarga de obtener una lista de personajes de dos maneras distintias dependiendo si existe o no un archivo local que contiene los datos de los persoanjes.

private static async Task<List<Personaje>> Inicializar()
{
     // Declaro una lista de Personaje que se usará para almacenar los personajes.
    List<Personaje> personajes;
     // Verifico si el archivo que contiene los datos de los personajes ya existe.
    if (PersonajesJson.ExisteArchivo(ArchivoPersonajes))
    {
        // Si existe el archivo, lee los personajes desde el archivo y los asigna a la variable 'personajes'.
        personajes = PersonajesJson.LeerPersonajes(ArchivoPersonajes);
    }
    else
    {
        //si el archivo no existe , se obtiene la lista de personajes desde la API de manera asincrona.
        List<Character> personajesApi = await FabricaDePersonajes.ObtenerPersonajesDesdeApi();


        //y se convierte la lista de personajes obtenidos de la API al tipo personaje.
        personajes = FabricaDePersonajes.ConvertirAReturnPersonajes(personajesApi);

        // Guardo la lista de personajes convertidos en un archivo para su uso futuro.
        PersonajesJson.GuardarPersonajes(personajes, ArchivoPersonajes);
    }

    return personajes;
}






//menu principal
private static void MostrarMenuPrincipal()
{
    Console.Clear();
    
    int anchoConsola = Console.WindowWidth;

    string encabezado = "     🌟 M E N Ú    P R I N C I P A L 🌟";
    string borde = "═════════════════════════════════════════";
    string opcion1 = "1. 🧙‍♂️ Mostrar Personajes";
    string opcion2 = "2. ⚔️ Combatir";
    string opcion3 = "3. 🏆 Mostrar Historial de Ganadores";
    string opcion4 = "4. ❌ Salir";

    Console.ForegroundColor = ConsoleColor.DarkBlue;
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





//Modulo para centrar texto de menu principal
private static string CentrarTexto(string texto, int ancho)
{
    int padding = (ancho - texto.Length) / 2;
    return new string(' ', padding) + texto + new string(' ', padding);
}


//Muestro lista de personajes
private static void MostrarPersonajes(List<Personaje> personajes)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine("          📜 LISTA DE PERSONAJES          ");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();
    Console.ReadKey();


    foreach (var personaje in personajes)
    {
        Console.WriteLine();
         Console.ForegroundColor = ConsoleColor.DarkBlue;
           Console.WriteLine("═══════════════════════════════════");
        Console.ForegroundColor = ConsoleColor.Black;
        //  Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
        Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
        Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
        Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento:dd-MM-yyyy}");
        Console.WriteLine($"Género: {personaje.Datos.Gender}");
        Console.WriteLine($"Ascendencia: {personaje.Datos.Ancestry}");
         Console.ForegroundColor = ConsoleColor.DarkBlue;
         Console.WriteLine("═══════════════════════════════════");
        Console.ResetColor();
        // Console.WriteLine();
        // Console.WriteLine();
    }

    Console.WriteLine("Presione una tecla para volver al menú principal.");
    Console.ReadKey();
}






private static void SeleccionarPersonajeYCombatir(List<Personaje> personajes)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine("        🏰 ¡SELECCIONA UN PERSONAJE! 🏰");
    Console.WriteLine("═════════════════════════════════════════");
    Console.ResetColor();


    // Imprimo la lista de personajes disponibles para que el usuario pueda seleccionar.
    for (int i = 0; i < personajes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {personajes[i].Datos.Nombre}");
        Console.WriteLine();
    }

    Console.Write("Ingrese el número del personaje: ");
    if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion >= 1 && seleccion <= personajes.Count)
    {
        Personaje personajeElegido = personajes[seleccion - 1];
        if (personajeElegido != null)
        {
               // Inicia el combate con el personaje seleccionado y el resto de los personajes.
            IniciarCombatePersonaje(personajeElegido, personajes);
        }
    }
    else
    {
        Console.WriteLine("Selección no válida.");
        Console.ReadKey();
    }
}








//coordina la gestión del juego, selecciona oponentes y maneja el flujo de combate.
private static void IniciarCombatePersonaje(Personaje personajeElegido, List<Personaje> personajes)
{
     // Mientras el personaje elegido tenga salud, se continúa buscando oponentes y combatiendo.
     //asegura que solo los personajes "vivos" participen en combates. 
    while (personajeElegido.Caracteristicas.Salud > 0)
    {

          // Selecciona un oponente aleatorio de la lista de personajes, excluyendo al personaje elegido.
        Personaje personajeOponente = ObtenerOponenteAleatorio(personajeElegido, personajes);
        if (personajeOponente == null)
        {
            Console.WriteLine("No hay más oponentes disponibles. ¡Has ganado el juego!");
            return;
        }
      // aqui
       // Muestro la información inicial del combate entre el personaje elegido y el oponente.
       MostrarInicioCombate(personajeElegido, personajeOponente);

        Console.ReadKey();

         //creo una nueva instancia de la clase combate para gestionar el combate entre los dos personajes.
        Combate combate = new Combate(personajeElegido, personajeOponente);


        //invoco al metodo iniciar combate (que me retorna el ganador)
        Personaje ganador = combate.IniciarCombate();
        

        // Si el personaje elegido es el ganador, elimino al oponente de la lista de personajes.
        if (ganador == personajeElegido)
        {
            personajes.Remove(personajeOponente);
            MostrarMensajeVictoria(personajeElegido);
        //agrego la opcion de seguir jugando o no cuando se gane
          Console.WriteLine("¿Quieres seguir jugando? (S/N): ");
            string? respuesta = Console.ReadLine()?.Trim().ToUpper();

            if (respuesta != "S")
            {
                Console.WriteLine("¡Gracias por jugar! Fin del juego.");
                break;
            }



        }
        else
        {
            // Si el personaje elegido pierde, se muestra un mensaje de derrota y se termina el juego.

            MostrarMensajeDerrota(personajeElegido);
            return;
        }
        //actualiza el historial de ganadores con el personaje ganador.
        HistorialJson.GuardarGanador(ganador, ArchivoHistorial);
    }
}





private static void MostrarMensajeVictoria(Personaje personajeGanador)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("═════════════════════════════════════════");
    Console.WriteLine($"🎉 ¡{personajeGanador.Datos.Nombre} HA GANADO LA BATALLA! 🎉");
        Console.WriteLine($"Casa: {personajeGanador.Datos.Tipo}");
        Console.WriteLine($"Apodo: {personajeGanador.Datos.Apodo}");
        // Console.WriteLine($"Fecha de Nacimiento: {personajeGanador.Datos.FechaNacimiento:dd-MM-yyyy}");
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






//metodo que muestra las caracteristicas de los personajes antes del combate
private static void MostrarInicioCombate(Personaje personaje1, Personaje personaje2)
{
    

    Console.Clear();

    // Mostrar detalles de los personajes que van a pelear
    Console.WriteLine("¡Estos son los personajes que van a pelear!");
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine($"👤 Personaje 1: {personaje1.Datos.Nombre}");
    Console.WriteLine($"  Salud: {personaje1.Caracteristicas.Salud}");
    Console.WriteLine($" Conocimiento de Posiones : {personaje1.Caracteristicas.Pociones}");
    Console.WriteLine($"  Defensa: {personaje1.Caracteristicas.Defensa}");
    Console.WriteLine($"  Hechizos: {personaje1.Caracteristicas.Hechizos}");
     Console.WriteLine($" Transformaciones de objetos: {personaje1.Caracteristicas.Transformaciones}");
      Console.WriteLine($"  Nivel de conocimiento Magico general: {personaje1.Caracteristicas.Nivel}");
    Console.ReadKey();

    Console.WriteLine("************** VS **************");

    Console.ReadKey();

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"👤 Personaje 2!: {personaje2.Datos.Nombre}");
    Console.WriteLine($"  Salud: {personaje2.Caracteristicas.Salud}");
    Console.WriteLine($" Conocimiento de Posiones : {personaje2.Caracteristicas.Pociones}");
    Console.WriteLine($"  Defensa: {personaje2.Caracteristicas.Defensa}");
    Console.WriteLine($"  Hechizos: {personaje2.Caracteristicas.Hechizos}");
     Console.WriteLine($" Transformaciones de objetos: {personaje2.Caracteristicas.Transformaciones}");
      Console.WriteLine($"  Nivel de conocimiento Magico general: {personaje2.Caracteristicas.Nivel}");
    Console.WriteLine();

    Console.ResetColor();
    
    // Esperar a que el usuario presione una tecla antes de continuar
    Console.WriteLine("Presiona cualquier tecla para comenzar el combate...");
    Console.ReadKey();
    
    // Mostrar el banner "¡Comienza el combate!"
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




//obtengo oponente aleatorio

private static Personaje ObtenerOponenteAleatorio(Personaje personajeElegido, List<Personaje> personajes)
{
    // Crea una instancia de la clase Random para generar números aleatorios.
    Random random = new Random();
    
    // Crea una nueva lista de Personaje que es una copia de la lista original de personajes.
    // Esto se hace para no modificar la lista original.
    List<Personaje> posiblesOponentes = new List<Personaje>(personajes);
    
    // Elimina el personaje elegido de la lista de posibles oponentes,
    // ya que no puedes luchar contra ti mismo.
    posiblesOponentes.Remove(personajeElegido);

    // Verifica si hay oponentes disponibles en la lista.
    if (posiblesOponentes.Count > 0)
    {
        // Si hay oponentes, selecciona uno al azar.
        // random.Next(posiblesOponentes.Count) genera un índice aleatorio dentro del rango de la lista.
        return posiblesOponentes[random.Next(posiblesOponentes.Count)];
    }

    // Si no hay oponentes disponibles, lanza una excepción.
    // Esto indica que no se pudo encontrar un oponente para la batalla.
    throw new InvalidOperationException("No se pudo encontrar un oponente.");
}




//muestro el historial de los ganadores
private static void MostrarHistorialGanadores()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkBlue;
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
          int numeroGanador = 1; // Inicializa el contador de ganadores
        
        foreach (var ganador in ganadores)
        {      Console.WriteLine($"Ganado numero #{numeroGanador}:"); // Muestra el número del ganador
            Console.WriteLine($"Nombre: {ganador.Datos.Nombre}");
            Console.WriteLine($"Casa: {ganador.Datos.Tipo}");
            Console.WriteLine($"Apodo: {ganador.Datos.Apodo}");
            Console.WriteLine();
             numeroGanador++; // Incrementa el contador
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