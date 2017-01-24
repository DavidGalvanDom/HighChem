///Propósito: Modelo de Producto
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
    public class Producto
    {
        [Display(Name = "Nombre", AutoGenerateField = false)]
        public string Nombre { get; set; }

        [Display(Name = "Unidad", AutoGenerateField = false)]
        public string Unidad { get; set; }

        [Display(Name = "Activo", AutoGenerateField = false)]
        public bool Activo { get; set; }

        [Display(Name = "ClaveSAP", AutoGenerateField = false)]
        public string ClaveSAP { get; set; }

        [Display(Name = "IdProveedor", AutoGenerateField = false)]
        public string IdProveedor { get; set; }

        [Display(Name = "Categoria", AutoGenerateField = false)]
        public string Categoria { get; set; }

        [Display(Name = "FechaCreacion", AutoGenerateField = false)]
        public string FechaCreacion { get; set; }

        [Display(Name = "FechaActualizado", AutoGenerateField = false)]
        public string FechaActualizado { get; set; }

         [Display(Name = "Negocio", AutoGenerateField = false)]
        public string Negocio { get; set; }

         [Display(Name = "Clasificacion", AutoGenerateField = false)]
         public string Clasificacion { get; set; }
    }
}
