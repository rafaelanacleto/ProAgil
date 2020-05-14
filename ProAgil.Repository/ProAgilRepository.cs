using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            this._context = context;
        }

        void IProAgilRepository.Add<T>(T entity)
        {
            _context.Add(entity);
        }

        void IProAgilRepository.Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lote)
                .Include(c => c.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        Task<Evento> IProAgilRepository.GetAllEventoAsyncById(int EventoID, bool includePalestrante)
        {
            throw new System.NotImplementedException();
        }

        Task<Evento[]> IProAgilRepository.GetAllEventoAsyncByTema(string tema, bool includePalestrante)
        {
            throw new System.NotImplementedException();
        }

        Task<Evento> IProAgilRepository.GetAllPalestranteAsync(int PalestranteID, bool includePalestrante)
        {
            throw new System.NotImplementedException();
        }

        Task<Evento[]> IProAgilRepository.GetAllPalestranteAsyncByName(bool includePalestrante)
        {
            throw new System.NotImplementedException();
        }

        async Task<bool> IProAgilRepository.SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        void IProAgilRepository.Update<T>(T entity)
        {
            _context.Update(entity);
        }
    }
}