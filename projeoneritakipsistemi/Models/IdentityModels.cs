using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace projeoneritakipsistemi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string kullaniciadi { get; set; }
        public string kullanici_turu { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //Session["username"] = userIdentity.Name;
          
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("GenelAzureDatabase", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.ogrenci> ogrencis { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.bolum> bolums { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.fakulte> fakultes { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.akademisyen> akademisyens { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.diger_kullanicilar> diger_kullanicilar { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.grup> grups { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.kaynak> kaynaks { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.proje> projes { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.universite> universites { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.yorum> yorums { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.yazilimmuh> yazilimmuhs { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.azureprojekontrol> azureprojekontrols { get; set; }

        public System.Data.Entity.DbSet<projeoneritakipsistemi.Models.begenme> begenmes { get; set; }
    }
}