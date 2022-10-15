using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Dominio
{
    public class Disco : IComparable<Disco>
    {
        private string _codigo;
        private Musico _musico;
        private string _titulo;
        private int _anio;
        private List<PosicionCancion> _posicionCanciones;
        private double _duracion;

        public Disco(string codigo, Musico musico, string titulo, int anio)
        {
            this._codigo = codigo;
            this._musico = musico;
            this._titulo = titulo;
            this._anio = anio;
            _posicionCanciones = new List<PosicionCancion>();
            _duracion = 0;
        }

        public string Codigo
        {
            get { return _codigo; }
        }

        public double Duracion
        {
            get { return _duracion; }
        }

        public Musico Musico
        {
            get { return _musico; }
        }

        public void Validar()
        {
            ValidarCodigo();
            ValidarMusico();
            ValidarTitulo();
            ValidarAnio();
        }

        private void ValidarCodigo()
        {
            if (string.IsNullOrEmpty(_codigo)) throw new Exception("Codigo no puede ser vacio");
        }

        private void ValidarTitulo()
        {
            if (string.IsNullOrEmpty(_titulo)) throw new Exception("Titulo no puede ser vacio");
        }

        private void ValidarMusico()
        {
            if (_musico == null) throw new Exception("Musico no puede ser vacio");
        }

        private void ValidarAnio()
        {
            if (_anio < 0) throw new Exception("El año debe ser mayor a 0");
        }

        private void VerificarExisteCancionEnDisco(Cancion c)
        {
            foreach (PosicionCancion p in _posicionCanciones)
            {
                if (p.Cancion.Equals(c)) throw new Exception("Ya existe la cancion en el disco");
            }
        }

        public bool ContieneCancion(Cancion c)
        {
            int i = 0;
            bool contiene = false;
            while(!contiene && i < _posicionCanciones.Count)
            {
                foreach (PosicionCancion p in _posicionCanciones)
                {
                    if (p.Cancion.Equals(c)) contiene = true;
                }
                i++;
            }
            return contiene;
        }

        private void VerificarExisteLaPosicionEnDisco(int pos)
        {
            foreach (PosicionCancion p in _posicionCanciones)
            {
                if (p.Posicion.Equals(pos)) throw new Exception("Ya existe una cancion en esa posicion en el disco");
            }
        }

        public void AgregarCancion(PosicionCancion pC)
        {
            try
            {
                if (pC == null) throw new Exception("Posicion cancion es nulo");
                pC.Validar();
                VerificarExisteCancionEnDisco(pC.Cancion);
                VerificarExisteLaPosicionEnDisco(pC.Posicion);
                _posicionCanciones.Add(pC);
                //Aca podría llamar al metodo CalcularDuracion y asignarle el resultado al atributo duracion del disco
                _duracion = CalcularDuracionDelDisco();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double CalcularDuracionDelDisco()
        {
            double duracion = 0;
            foreach (PosicionCancion p in _posicionCanciones)
            {
                duracion += p.Cancion.Duracion;
            }
            return duracion;
        }

        public override bool Equals(object obj)
        {
            Disco d = obj as Disco;
            return d != null && this._codigo.Equals(d._codigo);
        }

        public override string ToString()
        {
            string retorno = $"Disco {_codigo} | {_titulo} | {_musico.Nombre} | Duracion: {CalcularDuracionDelDisco()} minutos | Precio: $ {CalcularCostoDisco()}";
            if (_posicionCanciones.Count > 0)
            {
                foreach (PosicionCancion item in _posicionCanciones)
                {
                    retorno += item.ToString();
                }
            }

            return retorno;
        }

        
        public double CalcularCostoDisco()
        {
            double costo = 0;
            foreach (PosicionCancion p in _posicionCanciones)
            {
                costo += p.Cancion.Precio;
            }

            double descuento = _musico.CalcularDescuento();
            return costo - (costo * descuento / 100);
        }

        public int CompareTo([AllowNull] Disco other)
        {
            return this.CalcularCostoDisco().CompareTo(other.CalcularCostoDisco()) * -1;
        }
    }
}
