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
    private readonly Contexto _contexto;
    public TiposTecnicoService(Contexto contexto)
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
    public async Task<List<Tecnicos>> ObtenerTecnicosPorTipo(int tipoTecnicoId)
    {
        return await _contexto.Tecnicos
            .Where(t => t.TipoId == tipoTecnicoId)
            .Include(t => t.IncentivoId) // Incluir los incentivos relacionados Ojo con eso
            .ToListAsync();
    }

    public async Task<List<int>> ObtenerTecnicosIdsPorTipo(int tipoTecnicoId)
    {
        var tecnicosIds = await _contexto.Tecnicos
            .Where(t => t.TipoId == tipoTecnicoId)
            .Select(t => t.TecnicoId)
            .ToListAsync();

        return tecnicosIds;
    }

    public async Task ActualizarTotalIncentivos(int tipoTecnicoId)
    {
        var tecnicosIds = await ObtenerTecnicosIdsPorTipo(tipoTecnicoId);

        var incentivos = await _contexto.IncentivosTecnicos
            .Where(i => tecnicosIds.Contains(i.TecnicoId))
            .ToListAsync();

        var totalIncentivos = incentivos.Sum(i => i.Monto ?? 0);

        var tipoTecnico = await _contexto.TipoTecnicos.FindAsync(tipoTecnicoId);
        if (tipoTecnico != null)
        {
            tipoTecnico.Incentivo = totalIncentivos;
            await _contexto.SaveChangesAsync();
        }
    }

}