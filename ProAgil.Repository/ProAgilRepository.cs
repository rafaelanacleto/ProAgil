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
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(c => c.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoID, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Id == EventoID);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
               .Include(c => c.Lotes)
               .Include(c => c.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Tema.Contains(tema));
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteAsync(int PalestranteID, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
            }

            query = query.OrderBy(n => n.Nome).Where(p => p.Id == PalestranteID);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsync(bool includePalestrante = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(p => p.Nome);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string nome, bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
            }

            query = query.OrderByDescending(n => n.Nome).Where(n => n.Nome == nome);
            return await query.ToArrayAsync();
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