using System;

namespace EspacioPersonaje
{
    public enum Hogwarts
    {
        Gryffindor = 1,
        Hufflepuff = 2,
        Ravenclaw = 3,
        Slytherin = 4
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
            Datos = datos ?? throw new ArgumentNullException(nameof(datos)); // Lanza excepción si datos es nulo
            Caracteristicas = caracteristicas ?? throw new ArgumentNullException(nameof(caracteristicas)); // Lanza excepción si caracteristicas es nulo
        //      Datos = datos; // Lanza excepción si datos es nulo
        //     Caracteristicas = caracteristicas ; // Lanza excepción si caracteristicas es nulo
        }
    }

    public class Caracteristicas
    {
        private int pociones;
        public int Pociones
        {
            get => pociones;
            set => pociones = value;
        }

        private int defensa;
        public int Defensa
        {
            get => defensa;
            set => defensa = value;
        }

        private int hechizos;
        public int Hechizos
        {
            get => hechizos;
            set => hechizos = value;
        }

        private int nivel;
        public int Nivel
        {
            get => nivel;
            set => nivel = value;
        }

        private int transformaciones;
        public int Transformaciones
        {
            get => transformaciones;
            set => transformaciones = value;
        }

        private int salud; // Cambiado a privado
        public int Salud
        {
            get => salud;
            set => salud = value;
        }
    }

    public class Datos
    {
        private string tipo;
        public string Tipo
        {
            get => tipo;
            set => tipo = value; // Permite valores nulos
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set => nombre = value; // Permite valores nulos
        }

        private string apodo;
        public string Apodo
        {
            get => apodo;
            set => apodo = value; // Permite valores nulos
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

        private string gender;
        public string Gender
        {
            get => gender;
            set => gender = value; // Permite valores nulos
        }

        private string ancestry;
        public string Ancestry
        {
            get => ancestry;
            set => ancestry = value; // Permite valores nulos
        }

        private string imagen;
        public string Imagen
        {
            get => imagen;
            set => imagen = value; // Permite valores nulos
        }
    }
}
