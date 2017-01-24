///Propósito: Modelo de Documento
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
    public class Documento
    {
        [Display(Name = "Folio", AutoGenerateField = false)]
        public string Folio { get; set; }

        [Display(Name = "TipoDocumento", AutoGenerateField = false)]
        public string TipoDocumento { get; set; }

        [Display(Name = "FechaDoc", AutoGenerateField = false)]
        public string FecaDoc { get; set; }

        [Display(Name = "Sucursal", AutoGenerateField = false)]
        public string Sucursal { get; set; }

        [Display(Name = "Negocio", AutoGenerateField = false)]
        public string Negocio { get; set; }

        [Display(Name = "IdCliente", AutoGenerateField = false)]
        public string IdCliente { get; set; }

        [Display(Name = "IdVentdedor", AutoGenerateField = false)]
        public string IdVentdedor { get; set; }

        [Display(Name = "Moneda", AutoGenerateField = false)]
        public string Moneda { get; set; }

        [Display(Name = "TipodeCambio", AutoGenerateField = false)]
        public string TipodeCambio { get; set; }

        [Display(Name = "CondicionPago", AutoGenerateField = false)]
        public string CondicionPago { get; set; }

        [Display(Name = "Mercado", AutoGenerateField = false)]
        public string Mercado  { get; set; }

        [Display(Name = "SubTotalMN", AutoGenerateField = false)]
        public double SubTotalMN{ get; set; }

        [Display(Name = "ImpuestoMN", AutoGenerateField = false)]
        public double ImpuestoMN { get; set; }

        [Display(Name = "TotalMN", AutoGenerateField = false)]
        public double TotalMN  { get; set; }

        [Display(Name = "SubTotalDLS", AutoGenerateField = false)]
        public double SubTotalDLS { get; set; }

        [Display(Name = "ImpuestoDLS", AutoGenerateField = false)]
        public double ImpuestoDLS { get; set; }

        [Display(Name = "TotalDLS", AutoGenerateField = false)]
        public double TotalDLS { get; set; }

        [Display(Name = "FechaCarga", AutoGenerateField = false)]
        public string FechaCarga { get; set; }

        [Display(Name = "Proveedor", AutoGenerateField = false)]
        public string Proveedor { get; set; }

        [Display(Name = "FechaCreacion", AutoGenerateField = false)]
        public string FechaCreacion { get; set; }

        [Display(Name = "FechaActualizado", AutoGenerateField = false)]
        public string FechaActualizado { get; set; }

    }
}
