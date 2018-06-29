using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AddressBookApp
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ABService" в коде и файле конфигурации.
    public class ABService : IABService
    {
        addressbookEntities db;
        public void DeleteContact(ContactDTO wc)
        {
            db = new addressbookEntities();
            Contact contact = db.Contacts.Find(wc.Id);
            db.Contacts.Remove(contact);
            db.SaveChanges();

        }

        public IEnumerable<ContactDTO> GetContacts()
        {
            db = new addressbookEntities();
            var contactList = db.Contacts.ToList();
            var webContactList = new List<ContactDTO>();
            foreach(Contact contact in contactList)
            {
                ContactDTO cd = new ContactDTO();
                cd.Id = contact.Id;
                cd.FullName = contact.FullName;
                cd.BirthDate = contact.BirthDate;
                cd.Description = contact.Description;
                cd.email = contact.email;
                webContactList.Add(cd);
            }
            return webContactList;
        }

        public void InsertContact(ContactDTO wc)
        {
            Contact contact = new Contact();
            contact.FullName = wc.FullName;
            contact.BirthDate = wc.BirthDate;
            contact.Description = wc.Description;
            contact.email = wc.email;
            db = new addressbookEntities();
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public void UpdateContact(ContactDTO wc)
        {
            db = new addressbookEntities();
            Contact contact = (from x in db.Contacts where x.Id == wc.Id select x).First();
            contact.FullName = wc.FullName;
            contact.BirthDate = wc.BirthDate;
            contact.Description = wc.Description;
            contact.email = wc.email;
            db.SaveChanges();
        }
    }
}
