using System;

namespace EspacioPersonaje
{
    public enum Hogwarts
    {
        Gryffindor=1 ,
        Hufflepuff=2 ,
        Ravenclaw=3 ,
        Slytherin=4 
    }

    public class Personaje
    {
        private Datos datos; // Campo privado para almacenar Datos
        public Datos Datos
        {
            get => datos;
            private set => datos = value; // El set es privado para que solo la clase pueda modificarlo
        }

        private Caracteristicas caracteristicas; // Campo privado para almacenar Caracteristicas
        public Caracteristicas Caracteristicas
        {
            get => caracteristicas;
            private set => caracteristicas = value; // El set es privado para que solo la clase pueda modificarlo
        }

        // Constructor que inicializa las propiedades
        public Personaje(Datos datos, Caracteristicas caracteristicas)
        {
            Datos = datos ?? throw new ArgumentNullException(nameof(datos));
            Caracteristicas = caracteristicas ?? throw new ArgumentNullException(nameof(caracteristicas));
        }
    }

    public class Caracteristicas
    {
        private int encantamientos;
        public int Encantamientos
        {
            get => encantamientos;
            set => encantamientos = value;
        }

        private int defensa;
        public int Defensa
        {
            get => defensa;
            set => defensa = value;
        }

        private int pociones;
        public int Pociones
        {
            get => pociones;
            set => pociones = value;
        }

        private int transformaciones;
        public int Transformaciones
        {
            get => transformaciones;
            set => transformaciones = value;
        }

        private int adivinacion;
        public int Adivinacion
        {
            get => adivinacion;
            set => adivinacion = value;
        }

        private int salud; // Cambiado a privado
        public int Salud
        {
            get => salud;
            set => salud = value;
        }

        // Propiedades adicionales según el contexto
        // public int Agilidad { get; set; }
        // public int Resistencia { get; set; }
        // public int Energia { get; set; }
    }

    public class Datos
    {
        private string tipo;
        public string Tipo
        {
            get => tipo;
            set => tipo = value ?? throw new ArgumentNullException(nameof(value), "El valor no puede ser nulo");
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set => nombre = value ?? throw new ArgumentNullException(nameof(value), "El valor no puede ser nulo");
        }

        private string apodo;
        public string Apodo
        {
            get => apodo;
            set => apodo = value ?? throw new ArgumentNullException(nameof(value), "El valor no puede ser nulo");
        }

        private DateTime fechaNacimiento;
        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => fechaNacimiento = value;
        }

        private int edad;
        public int Edad
        {
            get => edad;
            set => edad = value;
        }
    }
}
