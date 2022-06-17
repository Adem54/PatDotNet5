using System.ComponentModel.DataAnnotations.Schema;

namespace linqDemo {

    public class Student{
        //Auto-increment id veriyoruz
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId {get; set;}
        public string Name {get; set;}
        public string Surname {get; set;}
        public int classId {get; set;}

    }
}