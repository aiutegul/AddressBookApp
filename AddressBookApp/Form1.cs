using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBookApp
{
    public partial class Form1 : Form
    {
        addressbookEntities db;
        private ServiceHost Host;
        public Form1()
        {
            InitializeComponent();
            db = new addressbookEntities();
            db.Contacts.Load();
            dataGridView1.DataSource = db.Contacts.Local.ToBindingList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ContactInfo ci = new ContactInfo();
            DialogResult result = ci.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Contact contact = new Contact();
            contact.FullName = ci.txtFullName.Text;
            contact.BirthDate = ci.dateTimePicker1.Value;
            contact.Description = ci.txtDescription.Text;
            contact.email = ci.txtEmail.Text;

            db.Contacts.Add(contact);
            db.SaveChanges();

            MessageBox.Show("New Contact Added!");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Contact contact = db.Contacts.Find(id);

                ContactInfo ci = new ContactInfo();

                ci.txtFullName.Text = contact.FullName;
                ci.dateTimePicker1.Value = contact.BirthDate;
                ci.txtDescription.Text = contact.Description;
                ci.txtEmail.Text = contact.email;

                DialogResult result = ci.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                contact.FullName = ci.txtFullName.Text;
                contact.BirthDate = ci.dateTimePicker1.Value;
                contact.Description = ci.txtDescription.Text;
                contact.email = ci.txtEmail.Text;

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Contact Updated!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Contact contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();

                MessageBox.Show("Contact Deleted!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Host = new ServiceHost(typeof(ABService));
            Host.Open();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Host.Close();
        }
    }
}
