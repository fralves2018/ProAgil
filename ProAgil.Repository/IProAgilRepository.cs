using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //GERAL
        //QUALQUER ENTIDADE QUE QUEIRAMOS CRIAR, TERÃO ESSAS FUNCIONALIDADES
        void Add<T>(T entity) where T: class;

        void Update<T>(T entity) where T: class;

        void Delete<T>(T entity) where T: class;

        Task<bool> SaveChangesAsync();

        //EVENTOS
        //CRIAÇÃO DO CRUD DE EVENTOS.
        Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);

        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);

        Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes);

////////////////////////////////////////////////////////////////////////////////////////////////////
        //PALESTRANTES
        //CRIAÇÃO DO CRUD DE PALESTRANTES.
        Task<Palestrante[]> GetAllPalestranteAsyncByName(string nome, bool includeEventos);

        Task<Palestrante> GetAllPalestranteAsync(int PalestranteId, bool includeEventos);

    }
}