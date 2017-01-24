///Propósito: Interfaz del servicio sapServices
///Fecha creación: 04/Febrero/14
///Creador: David Galvan
///Fecha modifiacción: 
///Modificó:
///Dependencias de conexiones e interfaces: 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using HighChem.SAP.Modelo;

namespace HighChem.SAP.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IsapService" in both code and config file together.
    [ServiceContract(Name = "SapService", Namespace = Informacion.Namespace.SAPWebService)]
    public interface IsapService
    {
        [OperationContract]
        [FaultContractAttribute(typeof(ManejoError))]
        string Autenticacion(string pidUser, string pcontrasena);

        [OperationContract]
        [FaultContractAttribute(typeof(ManejoError))]
        List<Documento> Documento(string pidSession, string pfechaInicio, string pfechaFin);

        [OperationContract]
        [FaultContractAttribute(typeof(ManejoError))]
        List<DocumentoDetalle> DocumentoDetalle(string pidSession, string pfechaInicio, string pfechaFin);

        [OperationContract]
        [FaultContractAttribute(typeof(ManejoError))]
        List<Cliente> Clientes(string pidSession, string pfechaInicio, string pfechaFin);

        [OperationContract]
        [FaultContractAttribute(typeof(ManejoError))]
        List<Producto> Productos(string pidSession, string pfechaInicio, string pfechaFin);
    }
}
