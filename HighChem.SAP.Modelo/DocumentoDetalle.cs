///Propósito: Modelo de Detalle documento
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
    public class DocumentoDetalle
    {
        [Display(Name = "Folio", AutoGenerateField = false)]
        public string Folio { get; set; }

        [Display(Name = "TipoDocumento", AutoGenerateField = false)]
        public string TipoDocumento { get; set; }

        [Display(Name = "Renglon", AutoGenerateField = false)]
        public int Renglon { get; set; }

        [Display(Name = "Cantidad", AutoGenerateField = false)]
        public double Cantidad { get; set; }

        [Display(Name = "Moneda", AutoGenerateField = false)]
        public string Moneda { get; set; }

        [Display(Name = "TipodeCambio", AutoGenerateField = false)]
        public string TipodeCambio { get; set; }

        [Display(Name = "Producto", AutoGenerateField = false)]
        public string Producto { get; set; }

        [Display(Name = "PriceMN", AutoGenerateField = false)]
        public double PriceMN { get; set; }

        [Display(Name = "ImporteMN", AutoGenerateField = false)]
        public double ImporteMN { get; set; }

        [Display(Name = "PriceDLS", AutoGenerateField = false)]
        public double PriceDLS { get; set; }

        [Display(Name = "ImporteDLS", AutoGenerateField = false)]
        public double ImporteDLS { get; set; }

        [Display(Name = "FechaCarga", AutoGenerateField = false)]
        public string FechaCarga { get; set; }

        [Display(Name = "FechaCreacion", AutoGenerateField = false)]
        public string FechaCreacion { get; set; }

        [Display(Name = "FechaActualizado", AutoGenerateField = false)]
        public string FechaActualizado { get; set; }

        [Display(Name = "IdVentdedor", AutoGenerateField = false)]
        public string IdVentdedor { get; set; }

    }
}
