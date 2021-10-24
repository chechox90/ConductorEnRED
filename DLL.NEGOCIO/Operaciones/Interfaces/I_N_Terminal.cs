﻿using DLL.DTO.Terminales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_Terminal
    {
        int GetTerminalByNombre(string nombreTerminal, int idEmpresa);
        List<DTO_Terminal> GetTerminalByAllActive();
    }
}
