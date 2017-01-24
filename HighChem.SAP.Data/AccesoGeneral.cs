///Propósito: Acceso a datos de SAP
///Fecha creación: 03/Junio/14
///Creador: David Galvan
///Fecha modifiacción: 
///Modificó:
///Dependencias de conexiones e interfaces: SQLServer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using HighChem.SAP.Modelo;

namespace HighChem.SAP.Data
{
    public class AccesoGeneral
    {
        /// <summary>
        /// Se carga la informacion del documento seleccionado de acuardo a la fecha de modificiacion y de Creacion
        /// </summary>
        /// <param name="pFechaInicio"></param>
        /// <param name="pFechaFin"></param>
        /// <returns></returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        public static List<Documento> Documento(string pFechaInicio, string pFechaFin)
        {
            var lstDocumentos = new List<Documento>();
            string sqlQuery = "";

            try
            {
                var db = DatabaseFactory.CreateDatabase("SQLStringConnSAP");

                sqlQuery = " SELECT T0.[DocNum] as Folio, T5.Dscription, ";
                sqlQuery += " case when T0.[DocSubType] = 'DN' then 'NA' else 'VE'end  as Tipo,  ";
                sqlQuery += " CONVERT(VARCHAR(19),T0.[DocDate] ,23)  as FechaDoc, T1.[U_Sucursal] as Sucursal,  ";                
                sqlQuery += " Left(CASE when T0.[DocType] = 'I' then (SELECT T12.[Name] FROM OITM T11 INNER JOIN \"@NEGOCIO_ARTICULO\" T12 ON T11.U_NEGOCIO = T12.Code WHERE T11.[ItemCode] = T5.[ItemCode]) ";
                sqlQuery += " else '' end,40) as Negocio, ";                
                sqlQuery += " T0.[CardCode] as Cliente, T2.[SlpName] as Vendedor, T0.[DocCur] as Moneda, ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') else T0.[DocRate] end as TipodeCambio, ";
                sqlQuery += " Left(T3.[PymntGroup],60) as CondicionPago, T4.[GroupName] as Mercado,  ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then (T0.[DocTotal]- T0.[VatSum]) else 0 end as SubTotalMN,  ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then T0.[VatSum] else 0 end as ImpuestoMN,  ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then T0.[DocTotal] else 0 end as TotalMN,  ";
                sqlQuery += " case when T0.[DocCur] = 'USD' then (T0.[DocTotalFC]- T0.[VatSumFC])  ";
                sqlQuery += "  else (T0.[DocTotal]- T0.[VatSum])/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as SubTotalDLS,  ";
                sqlQuery += " case when T0.[DocCur] = 'USD' then T0.[VatSumFC]  ";
                sqlQuery += "  else T0.[VatSum]/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as ImpuestoDLS,  ";
                sqlQuery += " case when T0.[DocCur] = 'USD' then T0.[DocTotalFC]  ";
                sqlQuery += "  else T0.[DocTotal]/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as TotalDLS,  ";
                sqlQuery += " CASE when T0.[DocType] = 'I' then (SELECT T10.[FirmName] FROM OMRC T10  INNER JOIN OITM T11 ON T10.FirmCode = T11.FirmCode WHERE T11.[ItemCode] = T5.[ItemCode]) ";
                sqlQuery += " else '' end as Proveedor, ";
                sqlQuery += " CONVERT(VARCHAR(19),T0.[CreateDate] ,23) as CreateDate, CONVERT(VARCHAR(19),T0.[UpdateDate] ,23)  as UpdateDate, T0.[SlpCode] as IdVendedor, CONVERT(VARCHAR(19),GetDate() ,23)  as FechaCarga  ";
                sqlQuery += " FROM OINV T0   ";
                sqlQuery += " INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode  ";
                sqlQuery += " INNER JOIN OSLP T2 ON T0.SlpCode = T2.SlpCode  ";
                sqlQuery += " INNER JOIN OCTG T3 ON T0.GroupNum = T3.GroupNum  ";
                sqlQuery += " INNER JOIN OCRG T4 ON T1.GroupCode = T4.GroupCode ";
                sqlQuery += " INNER JOIN INV1 T5 ON T0.DocEntry = T5.DocEntry and T5.LineNum = (SELECT TOP 1 LineNum FROM INV1 T6 WHERE T6.DocEntry = T0.DocEntry ) ";
                sqlQuery += " WHERE T0.[DocDate]  between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) ";
                
                sqlQuery += " UNION ALL ";

                sqlQuery += " SELECT T0.[DocNum] as Folio, T5.Dscription, ";
                sqlQuery += " 'NC' as Tipo,  ";
                sqlQuery += " CONVERT(VARCHAR(19),T0.[DocDate] ,23)  as FechaDoc, T1.[U_Sucursal] as Sucursal,  ";                
                sqlQuery += " Left(CASE when T0.[DocType] = 'I' then (SELECT T12.[Name] FROM OITM T11 INNER JOIN \"@NEGOCIO_ARTICULO\" T12 ON T11.U_NEGOCIO = T12.Code WHERE T11.[ItemCode] = T5.[ItemCode]) ";
                sqlQuery += " else '' end,40) as Negocio, ";                
                sqlQuery += " T0.[CardCode] as Cliente, T2.[SlpName] as Vendedor, T0.[DocCur] as Moneda, ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') else T0.[DocRate] end as TipodeCambio, ";
                sqlQuery += " Left(T3.[PymntGroup],60) as CondicionPago, T4.[GroupName] as Mercado,  ";              
                sqlQuery += " case when T0.[DocCur] = 'MXP' then (T0.[DocTotal]- T0.[VatSum]) else 0 end as SubTotalMN,  ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then T0.[VatSum] else 0 end as ImpuestoMN,  ";
                sqlQuery += " case when T0.[DocCur] = 'MXP' then T0.[DocTotal] else 0 end as TotalMN,  ";
                sqlQuery += " case when T0.[DocCur] = 'USD' then (T0.[DocTotalFC]- T0.[VatSumFC])  ";
                sqlQuery += " else (T0.[DocTotal]- T0.[VatSum])/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as SubTotalDLS,  ";
                sqlQuery += " case when T0.[DocCur] = 'USD' then T0.[VatSumFC]  ";
                sqlQuery += " else T0.[VatSum]/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as ImpuestoDLS,  ";
                sqlQuery += " case when T0.[DocCur] = 'USD' then T0.[DocTotalFC]  ";
                sqlQuery += " else T0.[DocTotal]/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as TotalDLS,  ";
                sqlQuery += " CASE when T0.[DocType] = 'I' then (SELECT T10.[FirmName] FROM OMRC T10  INNER JOIN OITM T11 ON T10.FirmCode = T11.FirmCode WHERE T11.[ItemCode] = T5.[ItemCode]) ";
                sqlQuery += " else '' end as Proveedor, ";
                sqlQuery += " CONVERT(VARCHAR(19),T0.[CreateDate] ,23) as CreateDate, CONVERT(VARCHAR(19),T0.[UpdateDate] ,23)  as UpdateDate, T0.[SlpCode] as IdVendedor, CONVERT(VARCHAR(19),GetDate() ,23)  as FechaCarga  ";
                sqlQuery += " FROM ORIN T0   ";
                sqlQuery += " INNER JOIN OCRD T1 ON T0.CardCode = T1.CardCode  ";
                sqlQuery += " INNER JOIN OSLP T2 ON T0.SlpCode = T2.SlpCode  ";
                sqlQuery += " INNER JOIN OCTG T3 ON T0.GroupNum = T3.GroupNum  ";
                sqlQuery += " INNER JOIN OCRG T4 ON T1.GroupCode = T4.GroupCode ";
                sqlQuery += " INNER JOIN RIN1 T5 ON T0.DocEntry = T5.DocEntry and T5.LineNum = (SELECT TOP 1 LineNum FROM RIN1 T6 WHERE T6.DocEntry = T0.DocEntry ) ";
                sqlQuery += " WHERE T0.[DocDate]  between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) ";
                                

                DbCommand dbCom = db.GetSqlStringCommand(sqlQuery);
                dbCom.CommandType = CommandType.Text;
                db.AddInParameter(dbCom, "@FechaInicio", DbType.String, pFechaInicio + " 00:00:00");
                db.AddInParameter(dbCom, "@FechaFin", DbType.String, pFechaFin + " 00:00:00");

                using (IDataReader dataReader = db.ExecuteReader(dbCom))
                {
                    while (dataReader.Read())
                    {
                        lstDocumentos.Add(new Documento()
                        {
                            Folio = dataReader["Folio"] == DBNull.Value ? "" : Convert.ToString(dataReader["Folio"]),
                            TipoDocumento = dataReader["Tipo"] == DBNull.Value ? "" : Convert.ToString(dataReader["Tipo"]),
                            Sucursal = dataReader["Sucursal"] == DBNull.Value ? "" : Convert.ToString(dataReader["Sucursal"]),
                            Negocio = dataReader["Negocio"] == DBNull.Value ? "" : Convert.ToString(dataReader["Negocio"]),
                            IdCliente = dataReader["Cliente"] == DBNull.Value ? "" : Convert.ToString(dataReader["Cliente"]),
                            IdVentdedor = dataReader["IdVendedor"] == DBNull.Value ? "" : Convert.ToString(dataReader["IdVendedor"]),
                            Moneda = dataReader["Moneda"] == DBNull.Value ? "" : Convert.ToString(dataReader["Moneda"]),
                            Proveedor = dataReader["Proveedor"] == DBNull.Value ? "" : Convert.ToString(dataReader["Proveedor"]),
                            TipodeCambio = dataReader["TipodeCambio"] == DBNull.Value ? "" : Convert.ToString(dataReader["TipodeCambio"]),
                            CondicionPago = dataReader["CondicionPago"] == DBNull.Value ? "" : Convert.ToString(dataReader["CondicionPago"]),
                            Mercado = dataReader["Mercado"] == DBNull.Value ? "" : Convert.ToString(dataReader["Mercado"]),
                            SubTotalMN = dataReader["SubTotalMN"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["SubTotalMN"]),
                            ImpuestoMN = dataReader["ImpuestoMN"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["ImpuestoMN"]),
                            TotalMN = dataReader["TotalMN"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["TotalMN"]),
                            SubTotalDLS = dataReader["SubTotalDLS"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["SubTotalDLS"]),
                            ImpuestoDLS = dataReader["ImpuestoDLS"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["ImpuestoDLS"]),
                            TotalDLS = dataReader["TotalDLS"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["TotalDLS"]),
                            FecaDoc = dataReader["FechaDoc"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["FechaDoc"]),
                            FechaActualizado = dataReader["UpdateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["UpdateDate"]),
                            FechaCreacion = dataReader["CreateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["CreateDate"]) ,
                            FechaCarga = dataReader["FechaCarga"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["FechaCarga"])
                        });
                    }
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message, exp);
            }

            return lstDocumentos;
        }



        /// <summary>
        /// Se carga el detalle del documento
        /// </summary>
        /// <param name="pFechaInicio"></param>
        /// <param name="pFechaFin"></param>
        /// <returns></returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        public static List<DocumentoDetalle> DocumentoDetalle(string pFechaInicio, string pFechaFin)
        {
            var lstDetalle = new List<DocumentoDetalle>();
            string sqlQuery = "";

            try
            {
                var db = DatabaseFactory.CreateDatabase("SQLStringConnSAP");
           
                 sqlQuery = " SELECT T0.[DocNum] as Folio, ";
                 sqlQuery += " case when T0.[DocSubType] = 'DN' then 'NA' else 'VE'end  as Tipo, T1.LineNum as Renglon, ";
                 sqlQuery += " T1.[Currency] as Moneda, T1.[LineTotal] as Importe,  ";
                 sqlQuery += " case when T1.[Currency] = 'MXP' then (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') else T1.[Rate] end  as TipodeCambio, ";
                 sqlQuery += " case when T1.[Currency] = 'MXP' then T1.[LineTotal] else 0 end as ImporteMN,  ";
                 sqlQuery += " case when T1.[Currency] = 'MXP' then T1.[Price] else 0 end as PriceMN,  ";
                 sqlQuery += " case when T1.[Currency] = 'USD' then T1.[TotalFrgn] else T1.[LineTotal]/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as ImporteDLS,  ";
                 sqlQuery += " case when T1.[Currency] = 'USD' then T1.[Price] else T1.[Price] / (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as PriceDLS,  ";
                 sqlQuery += " T1.ItemCode as Producto, T1.Quantity as Cantidad, T1.[SlpCode] as IdVendedor,";
                 sqlQuery += " CONVERT(VARCHAR(19),T0.[CreateDate] ,23) as CreateDate,  ";
                 sqlQuery += " CONVERT(VARCHAR(19),T0.[UpdateDate] ,23)  as UpdateDate, CONVERT(VARCHAR(19),GetDate() ,23)  as FechaCarga  ";
                 sqlQuery += " FROM OINV T0   ";
                 sqlQuery += " INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry  ";
                 sqlQuery += " WHERE T0.[DocDate]  between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) ";
                 
 
                 sqlQuery += " UNION ALL ";

                 sqlQuery += " SELECT T0.[DocNum] as Folio, ";
                 sqlQuery += " 'NC' as Tipo, T1.LineNum as Renglon,";
                 sqlQuery += " T1.[Currency] as Moneda, T1.[LineTotal] as Importe,  ";
                 sqlQuery += " case when T1.[Currency] = 'MXP' then (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') else T1.[Rate] end  as TipodeCambio, ";
                 sqlQuery += " case when T1.[Currency] = 'MXP' then T1.[LineTotal] else 0 end as ImporteMN,  ";
                 sqlQuery += " case when T1.[Currency] = 'MXP' then T1.[Price] else 0 end as PriceMN,  ";
                 sqlQuery += " case when T1.[Currency] = 'USD' then T1.[TotalFrgn] else T1.[LineTotal]/ (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as ImporteDLS,  ";
                 sqlQuery += " case when T1.[Currency] = 'USD' then T1.[Price] else T1.[Price] / (SELECT T9.[Rate] FROM ORTT T9 WHERE T9.[RateDate] = T0.DocDate and  T9.[Currency]  = 'USD') end as PriceDLS,  ";
                 sqlQuery += " T1.ItemCode as Producto, T1.Quantity as Cantidad, T1.[SlpCode] as IdVendedor,"; 
                 sqlQuery += " CONVERT(VARCHAR(19),T0.[CreateDate] ,23) as CreateDate,  ";
                 sqlQuery += " CONVERT(VARCHAR(19),T0.[UpdateDate] ,23)  as UpdateDate, CONVERT(VARCHAR(19),GetDate() ,23)  as FechaCarga  ";
                 sqlQuery += " FROM ORIN T0    ";
                 sqlQuery += " INNER JOIN RIN1 T1 ON T0.DocEntry = T1.DocEntry ";
                 sqlQuery += " WHERE T0.[DocDate]  between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) ";
                 
           
                DbCommand dbCom = db.GetSqlStringCommand(sqlQuery);
                dbCom.CommandType = CommandType.Text;
                db.AddInParameter(dbCom, "@FechaInicio", DbType.String, pFechaInicio + " 00:00:00");
                db.AddInParameter(dbCom, "@FechaFin", DbType.String, pFechaFin + " 00:00:00");

                using (IDataReader dataReader = db.ExecuteReader(dbCom))
                {
                    while (dataReader.Read())
                    {
                        lstDetalle.Add(new DocumentoDetalle()
                        {
                            Folio = dataReader["Folio"] == DBNull.Value ? "" : Convert.ToString(dataReader["Folio"]),                            
                            Producto = dataReader["Producto"] == DBNull.Value ? "" : Convert.ToString(dataReader["Producto"]),
                            TipoDocumento = dataReader["Tipo"] == DBNull.Value ? "" : Convert.ToString(dataReader["Tipo"]),
                            Renglon = dataReader["Renglon"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Renglon"]),
                            Moneda = dataReader["Moneda"] == DBNull.Value ? "" : Convert.ToString(dataReader["Moneda"]),
                            TipodeCambio = dataReader["TipodeCambio"] == DBNull.Value ? "" : Convert.ToString(dataReader["TipodeCambio"]),
                            Cantidad = dataReader["Cantidad"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["Cantidad"]),
                            PriceMN = dataReader["PriceMN"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["PriceMN"]),
                            ImporteMN = dataReader["ImporteMN"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["ImporteMN"]),
                            PriceDLS = dataReader["PriceDLS"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["PriceDLS"]),
                            ImporteDLS = dataReader["ImporteDLS"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["ImporteDLS"]),
                            IdVentdedor = dataReader["IdVendedor"] == DBNull.Value ? "" : Convert.ToString(dataReader["IdVendedor"]),
                            FechaActualizado = dataReader["UpdateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["UpdateDate"]),
                            FechaCreacion = dataReader["CreateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["CreateDate"]),
                            FechaCarga = dataReader["FechaCarga"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["FechaCarga"])
                        });
                    }
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message, exp);
            }

            return lstDetalle;
        }

        /// <summary>
        /// Lista de clientes 
        /// </summary>
        /// <param name="pfechaInicio"></param>
        /// <param name="pfechaFin"></param>
        /// <returns></returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        public static List<Cliente> Clientes(string pfechaInicio, string pfechaFin)
        {
            var lstClientes = new List<Cliente>();
            string sqlQuery = "";

            try
            {
                var db = DatabaseFactory.CreateDatabase("SQLStringConnSAP");
               
                sqlQuery = " SELECT Left(T0.[CardName],80) as Nombre, Left(T0.[LicTradNum],23) as RFC, T0.[U_Tipo] as Tipo, T1.[GroupName] as Mercado,  ";
                sqlQuery += "  T0.[U_Sucursal] as Sucursal, Left(T0.[City],50) as Ciudad,Left (T2.[Name],50) as Estado, Left (T3.[Name],30) as Pais ,  ";
                sqlQuery += "  T0.[SlpCode] as Vendedor, T0.[CardCode] as Cliente,";
                sqlQuery += "  CONVERT(VARCHAR(19),T0.[CreateDate] ,23) as CreateDate,  ";
                sqlQuery += "  CONVERT(VARCHAR(19),T0.[UpdateDate] ,23)  as UpdateDate  ";
                sqlQuery += "  FROM OCRD T0   ";
                sqlQuery += "   INNER JOIN OCRG T1 ON T0.GroupCode = T1.GroupCode  ";
                sqlQuery += "   LEFT JOIN OCST T2 ON T0.State1 = T2.Code and T0.Country = T2.Country   ";
                sqlQuery += "   LEFT JOIN OCRY T3 ON T0.Country = T3.Code  ";
                //sqlQuery += "   INNER JOIN OSLP T4 ON T0.SlpCode = T4.SlpCode  ";
                sqlQuery += "  WHERE T0.[CardType]  = 'C' and  T0.[frozenFor]  = 'N' ";
                sqlQuery += "  and (T0.[CreateDate] between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) ";
                sqlQuery += "    or T0.[UpdateDate] between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) )";

                DbCommand dbCom = db.GetSqlStringCommand(sqlQuery);
                dbCom.CommandType = CommandType.Text;
                db.AddInParameter(dbCom, "@FechaInicio", DbType.String, pfechaInicio + " 00:00:00");
                db.AddInParameter(dbCom, "@FechaFin", DbType.String, pfechaFin + " 00:00:00");
                
                using (IDataReader dataReader = db.ExecuteReader(dbCom))
                {
                    while (dataReader.Read())
                    {
                        lstClientes.Add(new Cliente()
                        {
                            RazonSoc = dataReader["Nombre"] == DBNull.Value ? "" : Convert.ToString(dataReader["Nombre"]),
                            RFC = dataReader["RFC"] == DBNull.Value ? "" : Convert.ToString(dataReader["RFC"]),
                            Tipo = dataReader["Tipo"] == DBNull.Value ? "" : Convert.ToString(dataReader["Tipo"]),
                            Mercado = dataReader["Mercado"] == DBNull.Value ? "" : Convert.ToString(dataReader["Mercado"]),
                            Sucursal = dataReader["Sucursal"] == DBNull.Value ? "" : Convert.ToString(dataReader["Sucursal"]),
                            Ciudad = dataReader["Ciudad"] == DBNull.Value ? "" : Convert.ToString(dataReader["Ciudad"]),
                            Estado = dataReader["Estado"] == DBNull.Value ? "" : Convert.ToString(dataReader["Estado"]),
                            Pais = dataReader["Pais"] == DBNull.Value ? "" : Convert.ToString(dataReader["Pais"]),
                            IdVendedor = dataReader["Vendedor"] == DBNull.Value ? "" : Convert.ToString(dataReader["Vendedor"]),
                            IdCliente = dataReader["Cliente"] == DBNull.Value ? "" : Convert.ToString(dataReader["Cliente"]),
                            FechaActualizado = dataReader["UpdateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["UpdateDate"]),
                            FechaCreacion = dataReader["CreateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["CreateDate"])
                        });
                    }
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message, exp);
            }

            return lstClientes;
        }

        /// <summary>
        /// Lista de clientes 
        /// </summary>
        /// <param name="pfechaInicio"></param>
        /// <param name="pfechaFin"></param>
        /// <returns></returns>
        /// <historico>
        /// David Galvan 03/Junio/2014 Creación
        /// </historico>
        public static List<Producto> Productos(string pfechaInicio, string pfechaFin)
        {
            var lstProducto = new List<Producto>();
            string sqlQuery = "";

            try
            {
                var db = DatabaseFactory.CreateDatabase("SQLStringConnSAP");

                sqlQuery = " SELECT Left(T0.[ItemName],80) as Nombre, Left(T0.[SalUnitMsr],10) as Unidad, ";
                sqlQuery += " 		T0.[validFor] as Activo, T0.[ItemCode] as ClaveSAP, T1.[FirmName] as Proveedor, ";
                sqlQuery += "		Left(T0.[U_Categoria],10) as Categoria,  ";
                sqlQuery += "		CONVERT(VARCHAR(19),T0.[CreateDate] ,23) as CreateDate, CONVERT(VARCHAR(19),T0.[UpdateDate] ,23) as UpdateDate,  ";
                sqlQuery += "       T0.U_NEGOCIO as Negocio, T0.U_CLASIFICACION Clasificacion ";
                sqlQuery += " FROM OITM T0  ";
                sqlQuery += " INNER JOIN OMRC T1 ON T0.FirmCode = T1.FirmCode ";
                sqlQuery += " WHERE T0.[SellItem] = 'Y' ";
                sqlQuery += "  and (T0.[CreateDate] between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20) ";
                sqlQuery += "    or T0.[UpdateDate] between Convert(datetime, @FechaInicio,20) and Convert(datetime, @FechaFin,20))";
                sqlQuery += " ORDER BY T0.[ItemCode] ";
                

                DbCommand dbCom = db.GetSqlStringCommand(sqlQuery);
                dbCom.CommandType = CommandType.Text;
                db.AddInParameter(dbCom, "@FechaInicio", DbType.String, pfechaInicio + " 00:00:00");
                db.AddInParameter(dbCom, "@FechaFin", DbType.String, pfechaFin + " 00:00:00");

                using (IDataReader dataReader = db.ExecuteReader(dbCom))
                {
                    while (dataReader.Read())
                    {
                        lstProducto.Add(new Producto()
                        {
                            Nombre = dataReader["Nombre"] == DBNull.Value ? "" : Convert.ToString(dataReader["Nombre"]),
                            Unidad = dataReader["Unidad"] == DBNull.Value ? "" : Convert.ToString(dataReader["Unidad"]),
                            Activo = Convert.ToString(dataReader["Activo"]) == "N" ? false : true,
                            Negocio = dataReader["Negocio"] == DBNull.Value ? "" : Convert.ToString(dataReader["Negocio"]),
                            ClaveSAP = dataReader["ClaveSAP"] == DBNull.Value ? "" : Convert.ToString(dataReader["ClaveSAP"]),
                            IdProveedor = dataReader["Proveedor"] == DBNull.Value ? "" : Convert.ToString(dataReader["Proveedor"]),
                            Categoria = dataReader["Categoria"] == DBNull.Value ? "" : Convert.ToString(dataReader["Categoria"]),
                            Clasificacion = dataReader["Clasificacion"] == DBNull.Value ? "" : Convert.ToString(dataReader["Clasificacion"]),
                            FechaActualizado = dataReader["UpdateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["UpdateDate"]),
                            FechaCreacion = dataReader["CreateDate"] == DBNull.Value ? "1900-01-01" : Convert.ToString(dataReader["CreateDate"])
                        });
                    }
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message, exp);
            }

            return lstProducto;
        }
    }
}
