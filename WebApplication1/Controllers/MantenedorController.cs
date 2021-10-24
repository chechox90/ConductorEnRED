using DLL.DTO.Terminales;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConductorEnRed.Controllers
{
    public class MantenedorController : Controller
    {
        private readonly I_N_Terminal _i_n_Terminal;
        private readonly I_N_Usuario _i_n_usuario;
        public MantenedorController(I_N_Terminal i_n_Terminal,
            I_N_Usuario i_n_usuario)
        {
            this._i_n_Terminal = i_n_Terminal;
            this._i_n_usuario = i_n_usuario;
        }

        public ActionResult HomeTerminales()
        {
            return View("~/Views/Mantenedores/Terminal.cshtml");
        }

        public ActionResult GetTerminalesActivos()
        {
            try
            {
                List<DTO_Terminal> DtoTerminal = new List<DTO_Terminal>();
                DtoTerminal = _i_n_Terminal.GetTerminalByAllActiveForTable();

                if (DtoTerminal != null)
                {
                    return Json(new { data = DtoTerminal, });
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Ha ocurrido una insidencia al <b>obtener los terminales</b>"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Mantenedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mantenedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mantenedor/Create
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

        // GET: Mantenedor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mantenedor/Edit/5
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

        // GET: Mantenedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mantenedor/Delete/5
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
