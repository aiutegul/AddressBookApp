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
        IEnumerable<ContactDTO> GetContacts();
        [OperationContract]
        void InsertContact(ContactDTO wc);
        [OperationContract]
        void UpdateContact(ContactDTO wc);
        [OperationContract]
        void DeleteContact(ContactDTO wc);
    }

    [DataContract]
    public class ContactDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string email { get; set; }
    }
}
