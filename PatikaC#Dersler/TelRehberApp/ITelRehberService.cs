
using System.Collections.Generic;
using System.Linq.Expressions;
namespace TelRehberApp{
    public interface ITelRehberService{
        void Save(Person person);
        bool Remove(string text);
        bool Modify(Person person);
        List<Person> GetAll();
        
        //Filtreleme burda olmamalidir
        //Filtrelem islemi ni biz her zaman GetAll() u ya opsiyonel bir sekilde yazarak hem isteyen tum listeyi kullansin  isteyende filtreden gecirsin seklinde delegeleri kullanarak List<Person> GetAll(Expression<Func<T>> filter=null) seklinde bir kullanim var bu mantikta kullanmaliyiz...yoksa buraya her bir filtreleme islemi icin ayri bir method yazmaya calismamaliyiz...
        // List<Person> GetPersonByNameOrLastName(string item)
        // List<Person> GetPersonByTelNumber(int number); 
    }
}