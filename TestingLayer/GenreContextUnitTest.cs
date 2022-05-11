using NUnit.Framework;
using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace TestingLayer
{
    public class GenreContextUnitTest
    {
        private VictorIvanovDbContext dbContext;
        private GenreContext genreContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new VictorIvanovDbContext(builder.Options);
            genreContext = new GenreContext(dbContext);
        }

        [Test]
        public void TestCreateGenre()
        {
            int genresBefore = genreContext.ReadAll().Count();

            genreContext.Create(new Genre("fps"));

            int genresAfter = genreContext.ReadAll().Count();

            Assert.IsTrue(genresBefore != genresAfter);
        }

        [Test]
        public void TestReadGenre()
        {
            genreContext.Create(new Genre("fps"));

            Genre genre = genreContext.Read(1);

            Assert.That(genre != null, "There is no record with id 1!");
        }

        [Test]
        public void TestUpdateGenre()
        {
            genreContext.Create(new Genre("fps"));

            Genre genre = genreContext.Read(1);

            genre.Name = "co-op";
            genreContext.Update(genre);

            Genre genre1 = genreContext.Read(1);

            Assert.IsTrue(genre1.Name == "co-op", "Genre Update() does not change name!");
        }

        [Test]
        public void TestDeleteGenre()
        {
            genreContext.Create(new Genre("fps"));

            int genresBefore = genreContext.ReadAll().Count();

            genreContext.Delete(1);

            int genresAfter = genreContext.ReadAll().Count();

            Assert.AreNotEqual(genresBefore, genresAfter);
        }
    }
}