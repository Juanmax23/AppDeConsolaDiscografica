using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Sistema
    {
        #region Singleton
        private static Sistema _instancia;

        private Sistema()
        {
            PrecargarMusicos();
            PrecargarDiscos();
            PrecargarCanciones();
            PrecargarCancionesADiscos();
        }

        public static Sistema Instancia
        {
            get 
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia; 
            }
        }
        #endregion

        private List<Disco> _discos = new List<Disco>();
        private List<Cancion> _canciones = new List<Cancion>();
        private List<Musico> _musicos = new List<Musico>();

        public List<Disco> Discos
        {
            get { return _discos; }
        }

        public List<Cancion> Canciones
        {
            get { return _canciones; }
        }

        public List<Musico> Musicos
        {
            get { return _musicos; }
        }

        private void PrecargarMusicos()
        {
            try
            {
                AltaMusico(new Banda("Cuarteto de Nos", "Uruguay", 5));
                AltaMusico(new Solista("Michael Jackson", "USA", Sexo.Masculino, 5));
                AltaMusico(new Banda("Red Hot Chilli Peppers", "USA", 4));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrecargarDiscos()
        {
            try
            {
                AltaDisco(new Disco("DISK001", ObtenerMusicoPorNombre("Cuarteto de Nos"), "Jueves", 2019));
                AltaDisco(new Disco("DISK002", ObtenerMusicoPorNombre("Michael Jackson"), "Thriller", 1982));
                AltaDisco(new Disco("DISK003", ObtenerMusicoPorNombre("Red Hot Chilli Peppers"), "Californication", 1999));
                AltaDisco(new Disco("DISK004", ObtenerMusicoPorNombre("Michael Jackson"), "Thriller Remake", 2010));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrecargarCanciones()
        {
            try
            {
                //Alta Canciones Cuarteto de Nos
                AltaCancion(new Cancion("Mario Neta", 3.5, 100));
                AltaCancion(new Cancion("Punta Cana", 3, 200));
                AltaCancion(new Cancion("Hombre con alas", 3.5, 140));
                AltaCancion(new Cancion("Anonimo", 3.5, 180));

                //Alta Canciones Michael Jackson
                AltaCancion(new Cancion("Thriller", 6, 300));
                AltaCancion(new Cancion("Beat It", 3, 100));
                AltaCancion(new Cancion("Billie Jean", 4, 240));
                AltaCancion(new Cancion("Baby be Mine", 4, 120));

                //Alta Canciones Red Hot
                AltaCancion(new Cancion("Californication", 5, 230));
                AltaCancion(new Cancion("Scar Tissue", 3.5, 170));
                AltaCancion(new Cancion("Porcelain", 2.5, 90));
                AltaCancion(new Cancion("Get on Top", 3, 260));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrecargarCancionesADiscos()
        {
            try
            {
                //Agrego Canciones al disco del Cuarteto de Nos
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK001"), new PosicionCancion(1, ObtenerCancionPorCodigo(1)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK001"), new PosicionCancion(2, ObtenerCancionPorCodigo(2)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK001"), new PosicionCancion(3, ObtenerCancionPorCodigo(3)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK001"), new PosicionCancion(4, ObtenerCancionPorCodigo(4)));


                //Agrego Canciones al disco de Michael Jackson
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK002"), new PosicionCancion(1, ObtenerCancionPorCodigo(5)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK002"), new PosicionCancion(2, ObtenerCancionPorCodigo(6)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK002"), new PosicionCancion(3, ObtenerCancionPorCodigo(7)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK002"), new PosicionCancion(4, ObtenerCancionPorCodigo(8)));

                //Agrego Canciones al disco de los Red Hot Chilli Pepers
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK003"), new PosicionCancion(1, ObtenerCancionPorCodigo(9)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK003"), new PosicionCancion(2, ObtenerCancionPorCodigo(10)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK003"), new PosicionCancion(3, ObtenerCancionPorCodigo(11)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK003"), new PosicionCancion(4, ObtenerCancionPorCodigo(12)));

                //Agrego Canciones al disco de Michael Jackson Remake
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK004"), new PosicionCancion(1, ObtenerCancionPorCodigo(5)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK004"), new PosicionCancion(2, ObtenerCancionPorCodigo(6)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK004"), new PosicionCancion(3, ObtenerCancionPorCodigo(7)));
                AgregarCancionADisco(ObtenerDiscoPorCodigo("DISK004"), new PosicionCancion(4, ObtenerCancionPorCodigo(8)));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaDisco(Disco d)
        {
            try
            {
                if (d == null) throw new Exception("El disco no puede ser nulo");
                d.Validar();
                VerificarSiYaExisteElDisco(d);
                _discos.Add(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void VerificarSiYaExisteElDisco(Disco d)
        {
            if (_discos.Contains(d)) throw new Exception("Ya existe el disco en el sistema");
        }

        public void AltaCancion(Cancion c)
        {
            try
            {
                if (c == null) throw new Exception("La cancion no puede ser nula");
                c.Validar();
                _canciones.Add(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void VerificarSiYaExisteMusico(Musico m)
        {
            if (_musicos.Contains(m)) throw new Exception("Ya existe el musico con ese nombre en el sistema");
        }

        public void AltaMusico(Musico m)
        {
            try
            {
                if (m == null) throw new Exception("El musico no puede ser nulo");
                m.Validar();
                VerificarSiYaExisteMusico(m);
                _musicos.Add(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarCancionADisco(Disco d, PosicionCancion pc)
        {
            try
            {
                if (d == null) throw new Exception("El disco no puede ser nulo");
                if(pc == null) throw new Exception("La cancion y su posicion no pueden ser nulos");
                d.AgregarCancion(pc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cancion ObtenerCancionPorCodigo(int codigo)
        {
            Cancion buscada = null;
            int i = 0;
            while (buscada == null && i < _canciones.Count)
            {
                if (_canciones[i].Id.Equals(codigo)) buscada = _canciones[i];
                i++;
            }
            return buscada;
        }

        public Musico ObtenerMusicoPorNombre(string nombre)
        {
            Musico buscado = null;
            int i = 0;
            while (buscado == null && i < _musicos.Count)
            {
                if (_musicos[i].Nombre.Equals(nombre)) buscado = _musicos[i];
                i++;
            }
            return buscado;
        }

        public Disco ObtenerDiscoPorCodigo(string codigo)
        {
            Disco buscado = null;
            int i = 0;
            while (buscado == null && i < _discos.Count)
            {
                if (_discos[i].Codigo.Equals(codigo)) buscado = _discos[i];
                i++;
            }
            return buscado;
        }

        public List<Disco> DiscosConDuracionMayorQue(double duracion)
        {
            List<Disco> listado = new List<Disco>();
            foreach (Disco d in _discos)
            {
                if (d.CalcularDuracionDelDisco() > duracion) listado.Add(d);
            }

            return listado;
        }

        public List<Musico> MusicosPorPais(string pais)
        {
            List<Musico> listado = new List<Musico>();
            foreach (Musico m in _musicos)
            {
                if (m.Pais.ToUpper().Equals(pais.ToUpper())) listado.Add(m);
            }

            return listado;
        }

        public List<Disco> DiscosConPrecioMayorA(double precio)
        {
            List<Disco> listado = new List<Disco>();
            foreach (Disco d in _discos)
            {
                if (d.CalcularCostoDisco() >= precio) listado.Add(d);
            }
            listado.Sort();
            return listado;
        }

        public List<Disco> DiscosQueContienenUnaCancion(Cancion cancion)
        {
            try
            {
                if (cancion == null) throw new Exception("La cancion no puede ser nula");
                List<Disco> listado = new List<Disco>();
                foreach (Disco d in _discos)
                {
                    if (d.ContieneCancion(cancion)) listado.Add(d);
                }
                return listado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public List<Disco> ElOLosDiscosDeMayorDuracion()
        {
            List<Disco> listado = new List<Disco>();
            double mayorDuracion = double.MinValue;
            foreach (Disco d in _discos)
            {
                if(d.Duracion > mayorDuracion)
                {
                    mayorDuracion = d.Duracion;
                    listado.Clear();
                    listado.Add(d);
                }
                else if(d.Duracion == mayorDuracion)
                {
                    listado.Add(d);
                }
            }

            return listado;
        }

        private bool MusicoTieneDiscos(Musico m)
        {
            bool tieneDisco = false;
            int i = 0;
            while (!tieneDisco && i < _discos.Count)
            {
                foreach (Disco d in _discos)
                {
                    if (d.Musico.Equals(m)) tieneDisco = true;
                }
                i++;
            }

            return tieneDisco;
        }

        public List<Musico> MusicosQueNoTienenDiscos()
        {
            List<Musico> musicos = new List<Musico>();
            foreach (Musico m in _musicos)
            {
                if (!MusicoTieneDiscos(m)) musicos.Add(m);
            }

            return musicos;
        }


    }
}
