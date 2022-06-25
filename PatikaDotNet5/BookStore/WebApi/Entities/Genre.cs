
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities {
    public class Genre {
        //Id uzerinden auto-increment yapiyoruz
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name {get; set;}
        public bool IsActive {get; set;}=true;
        //Kategorileri acip kapatmak istiyoruz
    }
}