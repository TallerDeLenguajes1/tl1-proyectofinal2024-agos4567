using System;

namespace EspacioPersonaje
{
    public class Personaje
    {
        private Datos datos;
        private Caracteristicas caracteristicas;

        public Datos Datos
        {
            get => datos;
        }

        public Caracteristicas Caracteristicas
        {
            get => caracteristicas;
        }

        public Personaje(Datos datos, Caracteristicas caracteristicas)
        {
            this.datos = datos ?? throw new ArgumentNullException(nameof(datos), "Datos no puede ser nulo");
            this.caracteristicas = caracteristicas ?? throw new ArgumentNullException(nameof(caracteristicas), "Caracteristicas no puede ser nulo");
        }
    }
}
