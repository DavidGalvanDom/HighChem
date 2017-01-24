///Propósito: Modelo de Seguridad
///Fecha creación: 04/Febrero/14
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
    public class Seguridad
    {
        [Display(Name = "id", AutoGenerateField = false)]
        public string id { get; set; }

        [Display(Name = "FehaExpira", AutoGenerateField = false)]
        public DateTime FehaExpira { get; set; }

    }
}
