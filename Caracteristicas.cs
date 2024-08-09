namespace EspacioPersonaje
{
    public class Caracteristicas
    {
        private int pociones;
        private int defensa;
        private int hechizos;
        private int nivel;
        private int transformaciones;
        private int salud;

        public int Pociones
        {
            get => pociones;
            set => pociones = value;
        }

        public int Defensa
        {
            get => defensa;
            set => defensa = value;
        }

        public int Hechizos
        {
            get => hechizos;
            set => hechizos = value;
        }

        public int Nivel
        {
            get => nivel;
            set => nivel = value;
        }

        public int Transformaciones
        {
            get => transformaciones;
            set => transformaciones = value;
        }

        public int Salud
        {
            get => salud;
            set => salud = value;
        }
    }
}
