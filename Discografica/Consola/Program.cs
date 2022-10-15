using System;
using System.Collections.Generic;
using Dominio;

namespace Consola
{
    class Program
    {
        private static Sistema sistema = Sistema.Instancia;
        static void Main(string[] args)
        {
            int opcion = int.MinValue;
            do
            {
                MostrarTitulo("Menu de opciones");
                MostrarMenu();
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        MostrarTodosLosDiscos();
                        break;
                    case 2:
                        MostrarDiscosConMasDuracionQue();
                        break;
                    case 3:
                        MostrarMusicosPorPais();
                        break;
                    case 4:
                        AltaSolista();
                        break;
                    case 5:
                        MostrarDiscosConMayorPrecioQue();
                        break;
                    case 6:
                        DiscosQueContienenUnaCancion();
                        break;
                    case 7:
                        MostrarDiscosMayorDuracion();
                        break;
                    case 8:
                        MostrarMusicosSinDiscos();
                        break;
                    case 0:
                        MostrarMensaje("Saliendo...");
                        break;
                    default:
                        MostrarError("Debe seleccionar una opcion valida");
                        break;
                }

            } while (opcion != 0);
        }

        static void MostrarMenu()
        {
            MostrarMensaje("Ingrese una opcion");
            MostrarMensaje("1 - Mostrar todos los discos");
            MostrarMensaje("2 - Mostrar discos con duracion mayor que...");
            MostrarMensaje("3 - Mostrar musicos por pais...");
            MostrarMensaje("4 - Alta solista");
            MostrarMensaje("5 - Mostrar discos con precio mayor que... ordenados por precio DESC");
            MostrarMensaje("6 - Mostrar discos que contienen una cancion dada");
            MostrarMensaje("7 - Mostrar discos mayor duracion");
            MostrarMensaje("8 - Mostrar musicos sin discos");
        }

        static void MostrarTitulo(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine($"**** {titulo}  ****");
            Console.WriteLine("--------------------");
            Console.WriteLine();
        }

        static void MostrarSeparador()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------");
            Console.WriteLine();
        }

        static void MostrarError(string error)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"**** {error}  ****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static void MostrarExito(string exito)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"**** {exito}  ****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        static string PedirPalabras(string mensaje)
        {
            MostrarMensaje(mensaje);
            return Console.ReadLine();
        }

        static int PedirNumeros(string mensaje)
        {
            int numero;
            bool exito = false;
            do
            {
                MostrarMensaje(mensaje);
                exito = int.TryParse(Console.ReadLine(), out numero);
                if (!exito)
                {
                    MostrarError("Debe ingresar solo numeros");
                }
            } while (!exito);

            return numero;
        }

        static Sexo PedirSexo()
        {
            bool exito;
            Sexo sexo = Sexo.Masculino;
            do
            {
                int numero = PedirNumeros("Ingrese sexo \n-> 1 - Masculino\n-> 2 - Femenino");
                switch (numero)
                {
                    case 1:
                        sexo = Sexo.Masculino;
                        exito = true;
                        break;
                    case 2:
                        sexo = Sexo.Femenino;
                        exito = true;
                        break;
                    default:
                        exito = false;
                        break;
                }

                if (!exito)
                {
                    MostrarError("Debe ingresar un numero correspondiente a un sexo");
                }

            } while (!exito);

            return sexo;
        }

        static void MostrarTodosLosDiscos()
        {
            MostrarTitulo("Listado de discos");
            try
            {
                List<Disco> discos = sistema.Discos;
                if (discos.Count == 0) throw new Exception("No existen discos en el sistema");

                MostrarExito("Discos encontrados");
                foreach (Disco d in discos)
                {
                    Console.WriteLine(d);
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void MostrarDiscosConMasDuracionQue()
        {
            MostrarTitulo("Listado de discos con duracion superior a un numero dado");
            try
            {
                int duracion = PedirNumeros("Ingrese duracion");
                List<Disco> discos = sistema.DiscosConDuracionMayorQue(duracion);
                if (discos.Count == 0) throw new Exception("No existen discos con duracion mayor a la dada");

                MostrarExito("Discos encontrados");
                foreach (Disco d in discos)
                {
                    Console.WriteLine(d);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void MostrarMusicosPorPais()
        {
            MostrarTitulo("Listado de musicos de un pais dado");
            try
            {
                string pais = PedirPalabras("Ingrese pais");
                List<Musico> musicos = sistema.MusicosPorPais(pais);
                if (musicos.Count == 0) throw new Exception("No existen musicos del pais ingresado");

                MostrarExito("Musicos encontrados");
                foreach (Musico m in musicos)
                {
                    Console.WriteLine(m);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private static void AltaSolista()
        {
            MostrarTitulo("Alta de solista");
            try
            {
                string nombre = PedirPalabras("Ingrese nombre");
                if (string.IsNullOrEmpty(nombre)) throw new Exception("Nombre ingresado no puede ser vacio");
                string pais = PedirPalabras("Ingrese pais");
                if (string.IsNullOrEmpty(pais)) throw new Exception("Pais ingresado no puede ser vacio");
                Sexo sexo = PedirSexo();
                int descuento = PedirNumeros("Ingrese porcentaje de descuento (Ej: 10)");
                if (descuento < 0) throw new Exception("El descuento no puede ser negativo");
                Musico m = new Solista(nombre, pais, sexo, descuento);
                sistema.AltaMusico(m);
                MostrarExito("Solista agregado correctamente");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private static void MostrarDiscosConMayorPrecioQue()
        {
            MostrarTitulo("Listado de discos con precio superior a un precio dado");
            try
            {
                int precio = PedirNumeros("Ingrese precio");
                List<Disco> discos = sistema.DiscosConPrecioMayorA(precio);
                if (discos.Count == 0) throw new Exception("No existen discos con duracion mayor a la dada");

                MostrarExito("Discos encontrados");
                foreach (Disco d in discos)
                {
                    Console.WriteLine(d);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private static void DiscosQueContienenUnaCancion()
        {
            MostrarTitulo("Listado de discos que contienen una cancion");
            try
            {
                int codigoCancion = PedirNumeros("Ingrese Id de cancion");
                Cancion c = sistema.ObtenerCancionPorCodigo(codigoCancion);
                if (c == null) throw new Exception("No existe una cancion con ese codigo");
                List<Disco> listado = sistema.DiscosQueContienenUnaCancion(c);
                if (listado.Count == 0) throw new Exception("No se encontraron discos con esa cancion");
                MostrarExito("Discos encontrados");
                
                foreach (Disco d in listado)
                {
                    Console.WriteLine(d);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void MostrarDiscosMayorDuracion()
        {
            MostrarTitulo("Listado de el o los discos de mayor duracion");
            try
            {
                List<Disco> discos = sistema.ElOLosDiscosDeMayorDuracion();
                if (discos.Count == 0) throw new Exception("No existen discos en el sistema");

                MostrarExito("Discos encontrados");
                foreach (Disco d in discos)
                {
                    Console.WriteLine(d);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void MostrarMusicosSinDiscos()
        {
            MostrarTitulo("Listado de musicos sin discos");
            try
            {
                List<Musico> musicos = sistema.MusicosQueNoTienenDiscos();
                if (musicos.Count == 0) throw new Exception("No existen musicos sin discos");

                MostrarExito("Musicos encontrados");
                foreach (Musico m in musicos)
                {
                    Console.WriteLine(m);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }


    }
}
