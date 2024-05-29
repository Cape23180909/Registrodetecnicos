using Microsoft.EntityFrameworkCore;
using RegistrodeTecnicos.Models;
using RegistrodeTecnicos.Pages.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistrodeTecnicos.Services;

public class TecnicoService
{
    private readonly Contexto Contexto;

    public TecnicoService(Contexto contexto)
    {
        Contexto = contexto;
    }

    // Método Existe 
    public async Task<bool> Existe(int tecnicoId, string? nombre = null)
    {
        if (nombre == null)
        {
            return await Contexto.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
        }
        else
        {
            return await Contexto.Tecnicos.AnyAsync(t => t.TecnicoId != tecnicoId && t.Nombre.Equals(nombre));
        }
    }

    // Método Insertar
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        Contexto.Tecnicos.Add(tecnico);
        return await Contexto.SaveChangesAsync() > 0;
    }

    // Método Modificar
    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        Contexto.Tecnicos.Update(tecnico);
        var modificado = await Contexto.SaveChangesAsync() > 0;
        Contexto.Entry(tecnico).State = EntityState.Detached;
        return modificado;
    }

    // Método Guardar
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    // Método Eliminar
    public async Task<bool> Eliminar(int id)
    {
        var tecnicosEliminados = await Contexto.Tecnicos.Where(t => t.TecnicoId == id).ExecuteDeleteAsync();
        return tecnicosEliminados > 0;
    }

    // Método Buscar
    public async Task<Tecnicos?> Buscar(int id)
    {
        return await Contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    // Método Listar
    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await Contexto.Tecnicos
            .Include(t => t.TiposTecnicos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Método GetAllTecnicos que me devuelve los registros de la tabla
    public async Task<List<Tecnicos>> GetAllTecnicos()
    {
        return await Contexto.Tecnicos
            .Include(t => t.TiposTecnicos)
            .AsNoTracking()
            .ToListAsync();
    }
}