﻿using APICondSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICondSecurity.Interfaces
{
    public interface ICidade
    {
        void Incluir(Cidade cidade);
        void Alterar(Cidade cidade);
        void Excluir(Cidade cidade);
        Task<Cidade> Get(int id);
        Task<IEnumerable<Cidade>> GetAll();
        Task<bool> SaveAllAsync();
    }
}