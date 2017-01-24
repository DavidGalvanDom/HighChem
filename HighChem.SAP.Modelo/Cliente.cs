///Propósito: Modelo de Cliente
///Fecha creación: 10/Febrero/14
///Creador: David Galvan
///Fecha modifiacción: 
///Modificó:
///Dependencias de conexiones e interfaces: 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace HighChem.SAP.Modelo
{
    public class Cliente
    {
        [Display(Name = "RazonSoc", AutoGenerateField = false)]
        public string RazonSoc { get; set; }

        [Display(Name = "RFC", AutoGenerateField = false)]
        public string RFC { get; set; }

        [Display(Name = "Tipo", AutoGenerateField = false)]
        public string Tipo { get; set; }

        [Display(Name = "Mercado", AutoGenerateField = false)]
        public string Mercado { get; set; }

        [Display(Name = "Sucursal", AutoGenerateField = false)]
        public string Sucursal { get; set; }

        [Display(Name = "Ciudad", AutoGenerateField = false)]
        public string Ciudad { get; set; }

        [Display(Name = "Estado", AutoGenerateField = false)]
        public string Estado { get; set; }

        [Display(Name = "Pais", AutoGenerateField = false)]
        public string Pais { get; set; }

        [Display(Name = "IdVendedor", AutoGenerateField = false)]
        public string IdVendedor { get; set; }
       
        [Display(Name = "IdCliente", AutoGenerateField = false)]
        public string IdCliente { get; set; }

        [Display(Name = "FechaCreacion", AutoGenerateField = false)]
        public string FechaCreacion { get; set; }

        [Display(Name = "FechaActualizado", AutoGenerateField = false)]
        public string FechaActualizado { get; set; }
    }
}
