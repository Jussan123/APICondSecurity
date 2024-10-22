﻿using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Infra.Data.Interfaces
{
    public interface IVeiculoTerceiro
    {
        void Incluir(VeiculoTerceiro veiculoTerceiro);
        void Alterar(VeiculoTerceiro veiculoTerceiro);
        void Excluir(VeiculoTerceiro veiculoTerceiro);
        Task<VeiculoTerceiro> Get(int id);
        Task<IEnumerable<VeiculoTerceiro>> GetAll();
        Task<bool> SaveAllAsync();
        Task<VeiculoTerceiro> GetByPlaca(string placa);
    }
}
