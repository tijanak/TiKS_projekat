using EntityFrameworkCoreMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomponentniTestovi
{
    public class ProjectContextMock
    {
        public static ProjectContext getDbContext(Donacija[] donacije=null, Korisnik[] korisnici=null, Slucaj[] slucajevi = null, Lokacija[] lokacije = null, Kategorija[] kategorije = null, Novost[] novosti = null, Zivotinja[] zivotinje = null, Trosak[]troskovi=null)
        {
            if(donacije == null) { donacije = []; }
            if (korisnici == null) { korisnici = []; }
            if (slucajevi == null) { slucajevi = []; }
            if (kategorije == null) { kategorije = []; }
            if (lokacije == null) { lokacije = []; }
            if (novosti == null) { novosti = []; }
            if (troskovi == null) { troskovi = []; }
            if (zivotinje == null) { zivotinje = []; }
            DbContextMock<ProjectContext> dbContextMock = new DbContextMock<ProjectContext>(new DbContextOptionsBuilder<ProjectContext>().Options);
            dbContextMock.CreateDbSetMock(x => x.Donacije, donacije);
            dbContextMock.CreateDbSetMock(x => x.Korisnici, korisnici);
            dbContextMock.CreateDbSetMock(x => x.Slucajevi, slucajevi);
            dbContextMock.CreateDbSetMock(x => x.Kategorije, kategorije);
            dbContextMock.CreateDbSetMock(x => x.Lokacije, lokacije);
            dbContextMock.CreateDbSetMock(x => x.Novosti, novosti);
            dbContextMock.CreateDbSetMock(x => x.Troskovi, troskovi);
            dbContextMock.CreateDbSetMock(x => x.Zivotinje, zivotinje);
            return dbContextMock.Object;
        }
    }
}
