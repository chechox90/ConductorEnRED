using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ConductorEnRed.Models;

namespace WebApplication1.Controllers
{
    public class ReprogramacionController : Controller
    {
        OleDbConnection Econ;

        public ActionResult Index()
        {
            return View("~/Views/Programacion/CargarProgramacion.cshtml");
        }


        public ActionResult ModificarReprogramacion(string id)
        {
            return View("~/Views/Programacion/Reprogramacion.cshtml");
        }


        public ActionResult Create()
        {
            return View();
        }


        public ActionResult CargaHorarioConductor()
        {
            try
            {
                string NombreArchivo = Request.Form["NombreCarga"];
                string FechaCarga = Request.Form["FechaCarga"];
                string comentario = Request.Form["Comenatrio"];

                string mensajeError = "";

                int num = 0;

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string filename = file.FileName;
                    string filepath = "/ExcelFolder/" + filename;
                    file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
                    
                    DataTable resultadoTabla = InsertExceldata(filepath, filename);
                    List<CargaArchivoModel> list = new List<CargaArchivoModel>();
                    for (int i = 1; i < resultadoTabla.Rows.Count; i++)
                    {
                        CargaArchivoModel carga = new CargaArchivoModel();

                        string rutString = resultadoTabla.Rows[i][0].ToString();
                        int sinGuion = int.Parse(resultadoTabla.Rows[i][0].ToString().Split('-')[0]);
                        string guion = resultadoTabla.Rows[i][0].ToString().Split('-')[1];

                        if (!ValidaRut(rutString))
                        {
                            mensajeError = "Se ha detecato que la fila " + (i + 1) + " no contiene un formato valido de R.U.N.";
                            break;
                        }
                        else if (!Digito(sinGuion).ToUpper().Equals(guion.ToUpper()))
                        {
                            mensajeError = "Por favor revise la fila " + (i + 1) + " el dígito verificador no es correcto";
                            break;
                        }
                        else
                        {
                            carga.RUT = resultadoTabla.Rows[i][0].ToString();

                        }

                        carga.NOMBRE = resultadoTabla.Rows[i][1].ToString();
                        carga.APELLIDO = resultadoTabla.Rows[i][2].ToString();
                        carga.NOMBRE_TERMINAL = resultadoTabla.Rows[i][3].ToString();

                        if (int.TryParse(resultadoTabla.Rows[i][4].ToString(), out num))
                            carga.TERMINAL_INICIO = resultadoTabla.Rows[i][4].ToString();
                        else
                        {
                            mensajeError = "La fila " + (i + 1) + "no contiene un formato numérico válido.";
                            break;
                        }

                        carga.BUS_INICIO = resultadoTabla.Rows[i][5].ToString();
                        carga.FECHA_INICIO = resultadoTabla.Rows[i][6].ToString();
                        carga.HORA_INICIO = resultadoTabla.Rows[i][7].ToString();

                        list.Add(carga);
                    }

                    if (mensajeError != "")
                    {
                        return Json(new
                        {
                            EnableError = true,
                            ErrorTitle = "Error",
                            ErrorMsg = mensajeError
                        });
                    }

                    if (list.Count > 0)
                    {
                        return Json(new
                        {
                            data = list,
                            ErrorMsg = "",
                            JsonRequestBehavior.AllowGet
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            EnableError = true,
                            ErrorTitle = "Error",
                            ErrorMsg = "Error en la <b>carga de datos</b>"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Error en la <b>carga de datos</b>"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Error en la <b>carga de datos</b>"
                });
            }
        }

        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        public static bool ValidaRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=NO;IMEX=1;""", filepath);
            Econ = new OleDbConnection(constr);
        }

        private DataTable InsertExceldata(string fileepath, string filename)
        {
            try
            {
                string fullpath = Server.MapPath("/excelfolder/") + filename;
                ExcelConn(fullpath);
                string query = string.Format("Select * from [{0}]", "Hoja1$");
                OleDbCommand Ecom = new OleDbCommand(query, Econ);
                Econ.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
                Econ.Close();
                oda.Fill(ds);
                DataTable dt = ds.Tables[0];

                return dt;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult ObtenerFechaLunes(string FECHA)
        {
            DateTime fechaHoy = DateTime.Now;

            int dia = Convert.ToInt32(fechaHoy.DayOfWeek);
            dia = dia - 1;
            DateTime fechaInicioSemana = fechaHoy.AddDays((dia) * (-1));

            return Json(new
            {
                data = fechaInicioSemana.ToString(),
                ErrorMsg = "",
                JsonRequestBehavior.AllowGet
            });
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reprogramacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reprogramacion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reprogramacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reprogramacion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
