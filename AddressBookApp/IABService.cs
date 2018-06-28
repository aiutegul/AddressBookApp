using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AddressBookApp
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IABService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IABService
    {
        [OperationContract]
        void DoWork();
    }
}
