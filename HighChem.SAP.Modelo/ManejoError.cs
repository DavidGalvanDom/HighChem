///Propósito: Modelo de ManejoError
///Fecha creación: 04/Febrero/14
///Creador: David Galvan
///Fecha modifiacción: 
///Modificó:
///Dependencias de conexiones e interfaces: 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HighChem.SAP.Modelo
{
    [DataContractAttribute]
    public class ManejoError
    {
        private string mstrReport;
        private string mstrMetodo;

        public ManejoError(string pstrMensaje, string pstrMetodo)
        {
            this.mstrReport = pstrMensaje;
            this.mstrMetodo = pstrMetodo;
        }

        [DataMemberAttribute]
        public string Mensaje
        {
            get { return this.mstrReport; }
            set { this.mstrReport = value; }
        }

        [DataMemberAttribute]
        public string Metodo
        {
            get { return this.mstrMetodo; }
            set { this.mstrMetodo = value; }
        }
    }
}
