using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Configuration;
using System.ServiceModel;
using System.Text;
using System.Reflection;

using HighChem.SAP.Data;
using HighChem.SAP.Modelo;

namespace HighChem.SAP.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "sapService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select sapService.svc or sapService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class sapService : IsapService
    {
        private static List<Seguridad> lstSessiones;

        /// <summary>
        /// Valida y autentica al usuario que puede llamar estos servicios.
        /// </summary>
        /// <param name="pidUser"></param>
        /// <param name="pcontrasena"></param>
        /// <returns> Id que autentifica al usuario </returns>
        /// <historico>
        /// David Galvan 04/Junio/2014 Creación
        /// </historico>
        public string Autenticacion(string pidUser, string pcontrasena)
        {
            string idNuevaSesion = Guid.NewGuid().ToString("N");

            try
            {
                if (pidUser == WebConfigurationManager.AppSettings["User"] &&
                    pcontrasena == WebConfigurationManager.AppSettings["Contrasena"])
                {
                    RemueveSessiones();

                    if (lstSessiones == null) lstSessiones = new List<Seguridad>();

                    lstSessiones.Add(new Seguridad()
                    {
                        id = idNuevaSesion,
                        FehaExpira = DateTime.Now.AddHours(4)
                    });

                    return (idNuevaSesion);
                }
                else
                {
                    return ("-1");
                }
            }
            catch (Exception exp)
            {
                throw new FaultException<ManejoError>(new ManejoError(String.Format("Error Autenticacion: {0} FullName: {1}", exp.Message, ((((MemberInfo)(exp.TargetSite))).ReflectedType).FullName), ((MemberInfo)(exp.TargetSite)).Name));
            }
        }


        /// <summary>
        /// Listado de ventas de acuerdo al rango de fehcas 
        /// </summary>
        /// <param name="pidSession">Id Session</param>
        /// <param name="pfechaInicio">YYYY-MM-DD  Ano Mes Dia</param>
        /// <param name="pfechaFin">YYYY-MM-DD  Ano Mes Dia</param>
        /// <returns>Lista de ventas</returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        public List<Documento> Documento(string pidSession, string pfechaInicio, string pfechaFin)
        {
            if (ExisteSession(pidSession))
            {
                if (!ValidaFecha(pfechaInicio))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Inicio no es valido " + pfechaInicio + "  Formato valido YYYY-MM-DD ", "Documento"));
                }

                if (!ValidaFecha(pfechaFin))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Fin no es valido " + pfechaFin  + "  Formato valido YYYY-MM-DD ", "Documento"));
                }
                return (AccesoGeneral.Documento(pfechaInicio,pfechaFin));
            }
            else
            {
                throw new FaultException<ManejoError>(new ManejoError("Error Documento Descripcion: No existe la Session","Documento" ));
            }
        }


        /// <summary>
        /// Listado de ventas de acuerdo al rango de fehcas 
        /// </summary>
        /// <param name="pidSession">Id Session</param>
        /// <param name="pfechaInicio">YYYY-MM-DD  Ano Mes Dia</param>
        /// <param name="pfechaFin">YYYY-MM-DD  Ano Mes Dia</param>
        /// <returns>Lista de ventas</returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        public List<DocumentoDetalle> DocumentoDetalle(string pidSession, string pfechaInicio, string pfechaFin)
        {
            if (ExisteSession(pidSession))
            {
                if (!ValidaFecha(pfechaInicio))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Inicio no es valido " + pfechaInicio + "  Formato valido YYYY-MM-DD ", "Documento"));
                }

                if (!ValidaFecha(pfechaFin))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Fin no es valido " + pfechaFin + "  Formato valido YYYY-MM-DD ", "Documento"));
                }
                return (AccesoGeneral.DocumentoDetalle(pfechaInicio, pfechaFin));
            }
            else
            {
                throw new FaultException<ManejoError>(new ManejoError("Error DocumentoDetalle Descripcion: No existe la Session", "DocumentoDetalle"));
            }
        }

        /// <summary>
        /// Listado de Clientes de acuerdo a la fecha
        /// </summary>
        /// <param name="pidSession">Id Session</param>
        /// <param name="pfechaInicio">YYYY-MM-DD  Ano Mes Dia</param>
        /// <param name="pfechaFin">YYYY-MM-DD  Ano Mes Dia</param>
        /// <returns>Lista de Clientes que se capturarron a partir de la fecha de parametro</returns>
        /// <historico>
        /// David Galvan 10/Junio/2014 Creación
        /// </historico>
        public List<Modelo.Cliente> Clientes(string pidSession, string pfechaInicio, string pfechaFin)
        {
            if (ExisteSession(pidSession))
            {
                if (!ValidaFecha(pfechaInicio))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Inicio no es valido " + pfechaInicio + "  Formato valido YYYY-MM-DD ", "Clientes"));
                }

                if (!ValidaFecha(pfechaFin))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Fin no es valido " + pfechaFin + "  Formato valido YYYY-MM-DD ", "Clientes"));
                }
                return (AccesoGeneral.Clientes(pfechaInicio, pfechaFin));
            }
            else
            {
                throw new FaultException<ManejoError>(new ManejoError("Error Clientes Descripcion: No existe la Session", "Clientes"));
            }
        }

        /// <summary>
        /// Listado de Productos modificacos a partir de la fecha
        /// </summary>
        /// <param name="pidSession">Id Session</param>
        /// <param name="pfechaInicio">YYYY-MM-DD  Ano Mes Dia</param>
        /// <param name="pfechaFin">YYYY-MM-DD  Ano Mes Dia</param>
        /// <returns>Lista de Productos que se capturarron a partir de la fecha de parametro</returns>
        /// <historico>
        /// David Galvan 10/Junio/2014 Creación
        /// </historico>
        public List<Modelo.Producto> Productos(string pidSession, string pfechaInicio, string pfechaFin)
        {
            if (ExisteSession(pidSession))
            {
                if (!ValidaFecha(pfechaInicio))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha inicio no es valido " + pfechaInicio + "  Formato valido YYYY-MM-DD ", "Productos"));
                }

                if (!ValidaFecha(pfechaFin))
                {
                    throw new FaultException<ManejoError>(new ManejoError("El formato de fecha Fin no es valido " + pfechaFin + "  Formato valido YYYY-MM-DD ", "Productos"));
                }
                return (AccesoGeneral.Productos(pfechaInicio, pfechaFin));
            }
            else
            {
                throw new FaultException<ManejoError>(new ManejoError("Error Productos Descripcion: No existe la Session", "Productos"));
            }
        }

        /// <summary>
        /// Valida si la session existe en la collecion 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true si existe la session</returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        private bool ExisteSession(string id)
        {
            try
            {
                if (lstSessiones != null)
                {
                    foreach (var session in lstSessiones)
                    {
                        if (session.id == id)
                        {
                            if (session.FehaExpira > DateTime.Now)
                            {
                                return (true);
                            }
                            else
                            {
                                RemueveSessiones();
                                return (false);
                            }
                        }
                    }
                }
                return (false);
            }
            catch (Exception) { return (false); }
        }

        /// <summary>
        ///  Se remueven las sessiones obsoletas del listado.
        /// </summary>
        /// <historico>
        /// David Galvan 04/Junio/2014 Creación
        /// </historico>
        private void RemueveSessiones()
        {
            try
            {
                if (lstSessiones != null)
                {
                    for (int index = 0; index < lstSessiones.Count; index++)
                    {
                        if (lstSessiones[index].FehaExpira < DateTime.Now)
                        {
                            lstSessiones.Remove(lstSessiones[index]);
                            index--;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Se valida que el formato de la fecha sea correcto
        /// </summary>
        /// <param name="pFecha">YYYY-MM-DD</param>
        /// <returns></returns>
        private bool ValidaFecha(string pFecha)
        {
            string[] arrFecha = null;
            bool result = true;
            try
            {
                arrFecha = pFecha.Split('-');
                if (arrFecha.Length != 3) return (false);

                var objDate = new DateTime(Convert.ToInt32(arrFecha[0]), Convert.ToInt32(arrFecha[1]), Convert.ToInt32(arrFecha[2]));

                if (objDate.IsDaylightSavingTime())                
                    return (true);
                
            }
            catch (Exception) { result = false; }

            return (result);
        }

        //using System.Web.Services.Description;
        //using System.Web.Services.Protocols;
        //[SoapDocumentMethod(Informacion.Namespace.SAPWebService,
        //RequestNamespace = Informacion.NamespaceInicial,
        //ResponseNamespace = Informacion.NamespaceInicial,
        //Use = SoapBindingUse.Literal,
        //ParameterStyle = SoapParameterStyle.Wrapped)]
    }
}
