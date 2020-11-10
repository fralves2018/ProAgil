using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            this._context = context;
            //maneira de genericamente para todos os usos desse contexto não trackear. 
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }



        //EVENTO
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            //1 - Vai ao banco através da expressão abaixo "_context.Eventos", e aproveitou para pegar junto com os eventos os Lotes e as RedesSociais. 
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            //2 - Se o parametro de entrada passado acima "includePalestrantes" for true, então ele adiciona na query tambem o palestrante. 
            if(includePalestrantes){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            //Ordena por data de evento descendente.
            //O metodo "AsNoTracking" não trava o recurso, para que ele seja retornado. Dessa maneira, o banco não gera lentidão ou mesmo travamento. 
            query = query.AsNoTracking()
                        .OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();

        }
        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            //1 - Vai ao banco através da expressão abaixo "_context.Eventos", e aproveitou para pegar junto com os eventos os Lotes e as RedesSociais. 
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            //2 - Se o parametro de entrada passado acima "includePalestrantes" for true, então ele adiciona na query tambem o palestrante. 
            if(includePalestrantes){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            //Ordena por data de evento descendente.
            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento)
                .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            //1 - Vai ao banco através da expressão abaixo "_context.Eventos", e aproveitou para pegar junto com os eventos os Lotes e as RedesSociais. 
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            //2 - Se o parametro de entrada passado acima "includePalestrantes" for true, então ele adiciona na query tambem o palestrante. 
            if(includePalestrantes){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            //Ordena por data de evento descendente.
            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento)
                .Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }



        //PALESTRANTE
        public async Task<Palestrante> GetAllPalestranteAsync(int PalestranteId, bool includeEventos = false)
        {
            //1 - Vai ao banco através da expressão abaixo "_context.Palestrantes", e aproveitou para pegar junto com os Palestrantes e as RedesSociais. 
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            //2 - Se o parametro de entrada passado acima "includeEventos" for true, então ele adiciona na query tambem o palestrante. 
            if(includeEventos){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }

            //Ordena por data de evento descendente.
            query = query.AsNoTracking().OrderBy(c => c.Nome).Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string nome, bool includeEventos)
        {
            //1 - Vai ao banco através da expressão abaixo "_context.Palestrantes", e aproveitou para pegar junto com os Palestrantes e as RedesSociais. 
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            //2 - Se o parametro de entrada passado acima "includeEventos" for true, então ele adiciona na query tambem o palestrante. 
            if(includeEventos){
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(e => e.Evento);
            }

            //Ordena por data de evento descendente.
            query = query.AsNoTracking().Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }


    }
}