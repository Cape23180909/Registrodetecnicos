using Microsoft.EntityFrameworkCore;
using RegistrodeTecnicos.Models;
using RegistrodeTecnicos.Pages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistrodeTecnicos.Services;

public class TiposTecnicoService
{
    private readonly Contexto contexto;
    public TiposTecnicoService(Contexto contexto)
    {
        contexto = contexto;
    }
    // Método Existente
    public async Task<bool> Existe(int tipoTecnicoId)
    {
        return await contexto
            .TipoTecnicos.AnyAsync(t => t.TipoId == tipoTecnicoId);
    }
    public async Task<bool> Existe(int tipoId, string? descripcion)
    {
        return await contexto.TipoTecnicos
            .AnyAsync(t => t.TipoId != tipoId && t.Descripcion.Equals(descripcion));
    }
    // Método Insertar
    private async Task<bool> Insertar(TiposTecnicos tipoTecnicos)
    {
        contexto.TipoTecnicos.Add(tipoTecnicos);
        return await contexto
            .SaveChangesAsync() > 0;
    }
    // Método Modificar
    private async Task<bool> Modificar(TiposTecnicos tipoTecnicos)
    {
        contexto.TipoTecnicos.Update(tipoTecnicos);
        return await contexto
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
        var tipoTecnico = await contexto
            .TipoTecnicos.FindAsync(id);
        if (tipoTecnico == null)
            return false;

        contexto.TipoTecnicos.Remove(tipoTecnico);
        return await contexto.SaveChangesAsync() > 0;
    }
    // Método buscar
    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await contexto.TipoTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoId == id);
    }
    // Método listar
    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await contexto.TipoTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}