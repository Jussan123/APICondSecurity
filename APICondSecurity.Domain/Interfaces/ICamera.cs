﻿using APICondSecurity.Domain.Models;

namespace APICondSecurity.Domain.Interfaces
{
    public interface ICamera
    {
        void Incluir(Cameras camera);
        void Alterar(Cameras camera);
        void Excluir(Cameras camera);
        Task<Cameras> Get(int id);
        Task<IEnumerable<Cameras>> GetAll();
        Task<bool> SaveAllAsync();
    }
}