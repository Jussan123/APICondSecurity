﻿using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface ITipoUsuario
    {
        void Incluir(TipoUsuario tipoUsuario);
        void Alterar(TipoUsuario tipoUsuario);
        void Excluir(TipoUsuario tipoUsuario);
        Task<TipoUsuario> Get(int id);
        Task<IEnumerable<TipoUsuario>> GetAll();
        Task<bool> SaveAllAsync();
    }
}