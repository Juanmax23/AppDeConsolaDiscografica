using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Interfaces;

namespace Dominio
{
    class Banda : Musico, IValidable
    {
        private int _cantIntegrantes;
        private static double _porcentajeDescuentoComun = 10;

        public Banda(string nombre, string pais, int cantIntegrantes) : base(nombre, pais)
        {
            this._cantIntegrantes = cantIntegrantes;
        }

        public override void Validar()
        {
            base.Validar();
            ValidarIntegrantes();
        }

        private void ValidarIntegrantes()
        {
            if (_cantIntegrantes <= 1) throw new Exception("La banda debe tener mas de un integrante");
        }

        //TODO
        public override double CalcularDescuento()
        {
            double descuento = 0;
            if (_cantIntegrantes > 4) descuento = _porcentajeDescuentoComun;
            return descuento;
        }
    }
}
