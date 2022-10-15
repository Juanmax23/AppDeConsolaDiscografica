using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Interfaces;

namespace Dominio
{
    public class Solista : Musico, IValidable
    {
        private Sexo _sexo;
        private double _porcentajeDescuento;

        public Solista(string nombre, string pais, Sexo sexo, double descuento) :base(nombre, pais)
        {
            this._sexo = sexo;
            this._porcentajeDescuento = descuento;
        }

        public override void Validar()
        {
            base.Validar();
            ValidarPorcentaje();
        }

        public void ValidarPorcentaje()
        {
            if (_porcentajeDescuento < 0) throw new Exception("El descuento debe ser mayor a 0");
        }

        //TODO
        public override double CalcularDescuento()
        {
            double descuento = 0;
            if (_sexo.Equals(Sexo.Masculino)) descuento = _porcentajeDescuento;
            return descuento;
        }
    }
}
