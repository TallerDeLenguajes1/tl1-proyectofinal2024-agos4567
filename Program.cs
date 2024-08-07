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
                await SeleccionarPersonajeYCombatir(personajes);
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
        // Aquí no puedes usar await porque LeerPersonajes es sincrónico
        personajes = PersonajesJson.LeerPersonajes(ArchivoPersonajes);
    }
    else
    {
        List<Character> personajesApi = await FabricaDePersonajes.ObtenerPersonajesDesdeApi();
        personajes = FabricaDePersonajes.ConvertirAReturnPersonajes(personajesApi);
        // Suponiendo que GuardarPersonajes es sincrónico
        PersonajesJson.GuardarPersonajes(personajes, ArchivoPersonajes);
    }

    return personajes;
}



        private static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Menú Principal:");
            Console.WriteLine("1. Mostrar Personajes");
            Console.WriteLine("2. Combatir");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }

        private static void MostrarPersonajes(List<Personaje> personajes)
        {
            Console.Clear();
            Console.WriteLine("Personajes:");

            foreach (var personaje in personajes)
            {
                Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
                Console.WriteLine($"Casa: {personaje.Datos.Tipo}");
                Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
                Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaNacimiento:dd-MM-yyyy}");
                Console.WriteLine($"Género: {personaje.Datos.Gender}");
                Console.WriteLine($"Ancestry: {personaje.Datos.Ancestry}");
                Console.WriteLine($"Imagen: {personaje.Datos.Imagen}");
                Console.WriteLine();
            }
            
            Console.WriteLine("Presione una tecla para volver al menú principal.");
            Console.ReadKey();
        }

        private static async Task SeleccionarPersonajeYCombatir(List<Personaje> personajes)
        {
            Console.Clear();
            Console.WriteLine("Seleccione un personaje para combatir:");

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
                }
            }
            else
            {
                Console.WriteLine("Selección no válida.");
                Console.ReadKey();
            }
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
