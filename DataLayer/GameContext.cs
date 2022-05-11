using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class GameContext : IDB<Game, int>
    {
        private VictorIvanovDbContext _context;

        public GameContext(VictorIvanovDbContext context) { _context = context; }

        public void Create(Game item)
        {
            try
            {
                List<Genre> genres = new List<Genre>();

                foreach (var genre in item.Genres)
                {
                    Genre fromDb = _context.Genres.Find(genre.Id);

                    if(fromDb != null)
                    {
                        genres.Add(fromDb);
                    }
                    else
                    {
                        genres.Add(genre);
                    }
                }

                item.Genres = genres;

                _context.Games.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Games.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Game Read(int key, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Game> query = _context.Games;

                if (useNavigationProperties)
                {
                    query = query.Include(g => g.Genres).Include(b => b.Customers);
                }

                return query.SingleOrDefault(g => g.Id == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Game> ReadAll(bool useNavigationProperties = false)
        {
            try
            {

                IQueryable<Game> query = _context.Games.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(g => g.Genres).Include(b => b.Customers);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Game item, bool useNavigationProperties = false)
        {
            try
            {
                Game fromDb = Read(item.Id, useNavigationProperties);

                if (useNavigationProperties)
                {
                    List<Genre> genres = new List<Genre>();

                    foreach (var genre in item.Genres)
                    {
                        Genre fromDB = _context.Genres.Find(genre.Id);

                        if(fromDB != null)
                        {
                            genres.Add(fromDB);
                        }
                        else
                        {
                            genres.Add(genre);
                        }
                    }

                    fromDb.Genres = genres;
                }

                _context.Entry(fromDb).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
