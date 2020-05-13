using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        void IProAgilRepository.Add<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        void IProAgilRepository.Delete<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        Task<Evento[]> IProAgilRepository.GetAllEventoAsync(bool includePalestrante)
        {
            throw new System.NotImplementedException();
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

        Task<bool> IProAgilRepository.SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        void IProAgilRepository.Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}