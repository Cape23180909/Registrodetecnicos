using Microsoft.EntityFrameworkCore;
using RegistrodeTecnicos.Models;
using RegistrodeTecnicos.Pages.DAL;
using System.Linq.Expressions;

namespace RegistrodeTecnicos.Servies
{
    public class TipoTecnicoServies
    {
        private readonly Contexto _context;

        public TipoTecnicoServies(Contexto context)
        {
            _context = context;
        }

        public async Task<bool> Guardar(TipoTecnicos tipoTecnicos)
        {
            if (tipoTecnicos.TipoId == 0)
            {
                _context.TipoTecnicos.Add(tipoTecnicos);
            }
            else
            {
                _context.TipoTecnicos.Update(tipoTecnicos);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<TipoTecnicos>> Listar(Expression<Func<TipoTecnicos, bool>> criterio)
        {
            return await _context.TipoTecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
