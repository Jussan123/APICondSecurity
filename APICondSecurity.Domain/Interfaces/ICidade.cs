﻿using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
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