using Microsoft.EntityFrameworkCore;
using RegistrodeTecnicos.Models;
using RegistrodeTecnicos.Pages.DAL;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace RegistrodeTecnicos.Servies
{
    public class TipoTecnicoServies
    {
        private readonly Contexto Contexto;

        public TipoTecnicoServies(Contexto contexto)
        {
            Contexto = contexto;
        }

        // Método Existe
        public async Task<bool> Existe(int tipoTecnicoId)
        {
            return await Contexto.TipoTecnicos.AnyAsync(t => t.TipoId == tipoTecnicoId);
        }

        // Método Insertar
        private async Task<bool> Insertar(TipoTecnicos tipoTecnico)
        {
            Contexto.TipoTecnicos.Add(tipoTecnico);
            return await Contexto.SaveChangesAsync() > 0;
        }

        // Método Modificar
        private async Task<bool> Modificar(TipoTecnicos tipoTecnico)
        {
            Contexto.TipoTecnicos.Update(tipoTecnico);
            var modificado = await Contexto.SaveChangesAsync() > 0;
            Contexto.Entry(tipoTecnico).State = EntityState.Detached;
            return modificado;
        }

        // Método Guardar
        public async Task<bool> Guardar(TipoTecnicos tipoTecnico)
        {
            if (!await Existe(tipoTecnico.TipoId))
                return await Insertar(tipoTecnico);
            else
                return await Modificar(tipoTecnico);
        }

        // Método Eliminar
        public async Task<bool> Eliminar(int id)
        {
            var tipoTecnico = await Contexto.TipoTecnicos.FindAsync(id);
            if (tipoTecnico != null)
            {
                Contexto.TipoTecnicos.Remove(tipoTecnico);
                return await Contexto.SaveChangesAsync() > 0;
            }
            return false;
        }

        // Método Buscar
        public async Task<TipoTecnicos?> Buscar(int id)
        {
            return await Contexto.TipoTecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TipoId == id);
        }

        // Método Listar
        public async Task<List<TipoTecnicos>> Listar(Expression<Func<TipoTecnicos, bool>> criterio)
        {
            return await Contexto.TipoTecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
