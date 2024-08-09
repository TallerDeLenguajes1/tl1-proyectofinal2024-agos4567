using System;

namespace EspacioPersonaje
{
    public class Datos
    {
        private string? tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechaNacimiento;
        private int edad;
        private string? gender;
        private string? ancestry;
        private string? imagen;

        public string? Tipo
        {
            get => tipo;
            set => tipo = value;
        }

        public string? Nombre
        {
            get => nombre;
            set => nombre = value;
        }

        public string? Apodo
        {
            get => apodo;
            set => apodo = value;
        }

        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => fechaNacimiento = value;
        }

        public int Edad
        {
            get => edad;
            set => edad = value;
        }

        public string? Gender
        {
            get => gender;
            set => gender = value;
        }

        public string? Ancestry
        {
            get => ancestry;
            set => ancestry = value;
        }

        public string? Imagen
        {
            get => imagen;
            set => imagen = value;
        }
    }
}
