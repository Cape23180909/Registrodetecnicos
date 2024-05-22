﻿using RegistrodeTecnicos.Models;
using RegistrodeTecnicos.Pages.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace RegistrodeTecnicos.Services
{
    public class IncentivosTecnicoService
    {
        private readonly Contexto _contexto;
        public IncentivosTecnicoService (Contexto contexto)
        {
            _contexto = contexto;
        }
        // Método Existente
        public async Task<bool> Existe(int tipoTecnicoId)
        {
            return await _contexto
                .TipoTecnicos.AnyAsync(t => t.TipoId == tipoTecnicoId);
        }
        public async Task<bool> Existe(int tipoId, string? descripcion)
        {
            return await _contexto.TipoTecnicos
                .AnyAsync(t => t.TipoId != tipoId && t.Descripcion.Equals(descripcion));
        }
        // Método Insertar
        private async Task<bool> Insertar(TiposTecnicos tipoTecnicos)
        {
            _contexto.TipoTecnicos.Add(tipoTecnicos);
            return await _contexto
                .SaveChangesAsync() > 0;
        }
        // Método Modificar
        private async Task<bool> Modificar(TiposTecnicos tipoTecnicos)
        {
            _contexto.TipoTecnicos.Update(tipoTecnicos);
            return await _contexto
                .SaveChangesAsync() > 0;
        }
        // Método guardar
        public async Task<bool> Guardar(TiposTecnicos tipoTecnicos)
        {
            if (!await Existe(tipoTecnicos.TipoId))
                return await Insertar(tipoTecnicos);
            else
                return await Modificar(tipoTecnicos);
        }
        // Método eliminar
        public async Task<bool> Eliminar(int id)
        {
            var tipoTecnico = await _contexto
                .TipoTecnicos.FindAsync(id);
            if (tipoTecnico == null)
                return false;

            _contexto.TipoTecnicos.Remove(tipoTecnico);
            return await _contexto.SaveChangesAsync() > 0;
        }
        // Método buscar
        public async Task<TiposTecnicos?> Buscar(int id)
        {
            return await _contexto.TipoTecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TipoId == id);
        }
        // Método listar
        public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
        {
            return await _contexto.TipoTecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}