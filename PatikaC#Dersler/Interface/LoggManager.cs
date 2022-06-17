using System;

namespace Interface {

//Bu bir yontemdir ama bu islemler daha cok dependency injection ile yapiliyor hemen onun ornegini de  yapalimmm
    public class LoggManager:ILogger{
        ILogger _logger;

        public LoggManager(ILogger logger){
            _logger=logger;
        }
        public void ApllyLogManager(){
            _logger.WriteLogg();
        }

//Istedigmiz herhangi bir loga yazmak icin
        public void ApplyLogg(ILogger logg){
            logg.WriteLogg();
        }

//Loglarin tamamina yazabilmek icin...
        public void ApplyAllLogg(List<ILogger> loggers){
            foreach (var item in loggers)
            {
                item.WriteLogg();
            }
        }

        public void WriteLogg()
        {
              _logger.WriteLogg();
        }
    }
}