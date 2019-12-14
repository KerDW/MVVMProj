using GalaSoft.MvvmLight.Command;
using MVVMPractica2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace MVVMPractica2.ViewModel
{
    class View1ViewModel : INotifyPropertyChanged
    {
        public View1ViewModel()
        {
            contactesPopulate();
            BtnFilterContacteCommand = new RelayCommand<string>(BtnFilterContacteClick);
            BtnFilterTelefonCommand = new RelayCommand<string>(BtnFilterTelefonClick);
            BtnFilterEmailCommand = new RelayCommand<string>(BtnFilterEmailClick);
            BtnTableCommand = new RelayCommand<string>(BtnTableClick);
        }

        private void swapTable(String which)
        {
            r = 0;
            r1 = 0;
            r2 = 0;

            switch (which)
            {
                case "Contacts":
                    contactesPopulate();
                    r = 0.1;
                    index = 0;
                    break;
                case "Telefons":
                    telefonsPopulate();
                    r1 = 0.1;
                    index1 = 0;
                    break;
                case "Emails":
                    emailsPopulate();
                    r2 = 0.1;
                    index2 = 0;
                    break;
                default:
                    Console.WriteLine("error");
                    break;
            }

        }

        private void FilterContactes()
        {
            string filter = TextFilterContacte;
            swapTable("Contacts");
            contactesPopulate();
            List<contacte> contactesFiltering = new List<contacte>();
            switch (FilterChoiceContacte)
            {
                case "contacteStartsWith":
                    foreach (contacte c in contactes)
                    {
                        if (c.nom.ToLower().StartsWith(filter.ToLower()) || c.cognoms.ToLower().StartsWith(filter.ToLower()))
                        {
                            contactesFiltering.Add(c);
                        }
                    }
                    break;
                case "contacteContains":
                    foreach (contacte c in contactes)
                    {
                        if (c.nom.ToLower().Contains(filter.ToLower()) || c.cognoms.ToLower().Contains(filter.ToLower()))
                        {
                            contactesFiltering.Add(c);
                        }
                    }
                    break;
                case "contacteEndsWith":
                    foreach (contacte c in contactes)
                    {
                        if (c.nom.ToLower().EndsWith(filter.ToLower()) || c.cognoms.ToLower().EndsWith(filter.ToLower()))
                        {
                            contactesFiltering.Add(c);
                        }
                    }
                    break;
                default:
                    return;
            }
            contactes.Clear();
            contactes.AddRange(contactesFiltering);

        }

        private void FilterEmails()
        {
            string filter = TextFilterEmail;
            swapTable("Emails");
            emailsPopulate();
            List<email> emailsFiltering = new List<email>();
            switch (FilterChoiceEmail)
            {
                case "emailStartsWith":
                    foreach (email e in emails)
                    {
                        if (e.email1.ToLower().EndsWith(filter.ToLower()) || e.tipus.ToLower().EndsWith(filter.ToLower()))
                        {
                            emailsFiltering.Add(e);
                        }
                    }
                    break;
                case "emailContains":
                    foreach (email e in emails)
                    {
                        if (e.email1.ToLower().EndsWith(filter.ToLower()) || e.tipus.ToLower().EndsWith(filter.ToLower()))
                        {
                            emailsFiltering.Add(e);
                        }
                    }
                    break;
                case "emailEndsWith":
                    foreach (email e in emails)
                    {
                        if (e.email1.ToLower().EndsWith(filter.ToLower()) || e.tipus.ToLower().EndsWith(filter.ToLower()))
                        {
                            emailsFiltering.Add(e);
                        }
                    }
                    break;
                default:
                    return;
            }
            emails.Clear();
            emails.AddRange(emailsFiltering);
        }

        private void FilterTelefons()
        {
            string filter = TextFilterTelefon;
            swapTable("Telefons");
            telefonsPopulate();
            List<telefon> telefonsFiltering = new List<telefon>();
            switch (FilterChoiceTelefon)
            {
                case "telefonStartsWith":
                    foreach (telefon t in telefons)
                    {
                        if (t.telefon1.ToLower().EndsWith(filter.ToLower()) || t.tipus.ToLower().EndsWith(filter.ToLower()))
                        {
                            telefonsFiltering.Add(t);
                        }
                    }
                    break;
                case "telefonContains":
                    foreach (telefon t in telefons)
                    {
                        if (t.telefon1.ToLower().EndsWith(filter.ToLower()) || t.tipus.ToLower().EndsWith(filter.ToLower()))
                        {
                            telefonsFiltering.Add(t);
                        }
                    }
                    break;
                case "telefonEndsWith":
                    foreach (telefon t in telefons)
                    {
                        if (t.telefon1.ToLower().EndsWith(filter.ToLower()) || t.tipus.ToLower().EndsWith(filter.ToLower()))
                        {
                            telefonsFiltering.Add(t);
                        }
                    }
                    break;
                default:
                    return;
            }
            telefons.Clear();
            telefons.AddRange(telefonsFiltering);
        }

        public const string TextFilterPropertyNameContacte = "TextFilterContacte";
        private string _TextFilterContacte = "";
        public string TextFilterContacte
        {
            get { return _TextFilterContacte; }
            set
            {
                _TextFilterContacte = value;
                NotifyPropertyChanged();
                FilterContactes();
            }
        }
        public const string TextFilterPropertyNameEmail = "TextFilterEmail";
        private string _TextFilterEmail = "";
        public string TextFilterEmail
        {
            get { return _TextFilterEmail; }
            set
            {
                _TextFilterEmail = value;
                NotifyPropertyChanged();
                FilterEmails();
            }
        }
        public const string TextFilterPropertyNameTelefon = "TextFilterTelefon";
        private string _TextFilterTelefon = "";
        public string TextFilterTelefon
        {
            get { return _TextFilterTelefon; }
            set
            {
                _TextFilterTelefon = value;
                NotifyPropertyChanged();
                FilterTelefons();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        Contactes2Entities db = new Contactes2Entities();

        private List<contacte> _contactes;
        public List<contacte> contactes
        {
            get
            {
                return _contactes;
            }
            set
            {
                _contactes = value;
                NotifyPropertyChanged();
            }
        }

        private List<email> _emails;
        public List<email> emails
        {
            get
            {
                return _emails;
            }
            set
            {
                _emails = value;
                NotifyPropertyChanged();
            }
        }
        private List<telefon> _telefons;
        public List<telefon> telefons
        {
            get
            {
                return _telefons;
            }
            set
            {
                _telefons = value;
                NotifyPropertyChanged();
            }
        }
        public contacte SelectedContacte
        {
            get
            {
                return _selectedContacte;
            }

            set
            {
                _selectedContacte = value;
                if (TableChoice.Equals("Contacts"))
                {
                    emailsContacte();
                    telefonsContacte();
                }
                NotifyPropertyChanged();
            }
        }

        private contacte _selectedContacte;
        public email SelectedEmail
        {
            get
            {
                return _selectedEmail;
            }

            set
            {
                _selectedEmail = value;
                if (TableChoice.Equals("Emails"))
                {
                    contacteEmails();
                    telefonsEmail();
                }

                NotifyPropertyChanged();
            }
        }
        private email _selectedEmail;
        public telefon SelectedTelefon
        {
            get
            {
                return _selectedTelefon;
            }

            set
            {
                _selectedTelefon = value;
                if (TableChoice.Equals("Telefons"))
                {
                    contacteTelefons();
                    emailTelefons();
                }
                NotifyPropertyChanged();
            }
        }
        private telefon _selectedTelefon;

        private void contactesPopulate()
        {
            contactes = db.contactes.OrderBy(a => a.nom).ThenBy(a => a.cognoms).ToList();
        }
        private void emailsPopulate()
        {
            emails = db.emails.OrderBy(a => a.email1).ToList();
        }
        private void telefonsPopulate()
        {
            telefons = db.telefons.OrderBy(a => a.telefon1).ToList();
        }
        private void telefonsContacte()
        {
            try {
                List<int> telIds = new List<int>();

                foreach (var tel in SelectedContacte.telefons)
                {
                    telIds.Add(tel.telId);
                }

                telefons = db.telefons.Where(a => telIds.Contains(a.telId)).OrderBy(a => a.telefon1).ToList();
                SelectedTelefon = telefons.FirstOrDefault();
            }
            catch (Exception e)
            {
                // there was no selected contacte
            }
        }
        private void emailsContacte()
        {

            try {
                List<int> emailIds = new List<int>();

                foreach (var em in SelectedContacte.emails)
                {
                    emailIds.Add(em.emailId);
                }

                emails = db.emails.Where(a => emailIds.Contains(a.emailId)).OrderBy(a => a.email1).ToList();
                SelectedEmail = emails.FirstOrDefault();
            }
            catch (Exception e)
            {
                // there was no selected contacte
            }
        }

        private void contacteTelefons()
        {
            try
            {
                contactes = db.contactes.Where(a => a.contacteId == SelectedTelefon.contacteId).OrderBy(a => a.nom).ToList();
                SelectedContacte = contactes.FirstOrDefault();
            } catch (Exception e)
            {
                // there was no selected telefon
            }
        }
        private void emailTelefons()
        {
            try { 
                emails = db.emails.Where(a => a.contacteId == SelectedTelefon.contacteId).OrderBy(a => a.email1).ToList();
                SelectedEmail = emails.FirstOrDefault();
            }
            catch (Exception e)
            {
                // there was no selected telefon
            }
        }
        private void contacteEmails()
        {
            try
            {
                contactes = db.contactes.Where(a => a.contacteId == SelectedEmail.contacteId).OrderBy(a => a.nom).ToList();
                SelectedContacte = contactes.FirstOrDefault();
            } catch (Exception e)
            {
                // there was no selected email
            }
        }
        private void telefonsEmail()
        {
            try { 
                telefons = db.telefons.Where(a => a.contacteId == SelectedEmail.contacteId).OrderBy(a => a.telefon1).ToList();
                SelectedTelefon = telefons.FirstOrDefault();
            }
            catch (Exception e)
            {
                // there was no selected email
            }
        }

        public void BtnFilterContacteClick(string btName)
        {
            FilterChoiceContacte = btName;
        }
        public void BtnFilterTelefonClick(string btName)
        {
            FilterChoiceTelefon = btName;
        }
        public void BtnFilterEmailClick(string btName)
        {
            FilterChoiceEmail = btName;
        }

        public void BtnTableClick(string btName)
        {
            TableChoice = btName;
        }

        public int _index { get; set; } = 0;
        public int index
        {
            get { return _index; }
            set
            {
                _index = value + 1;
                NotifyPropertyChanged();
                _index = value;
                NotifyPropertyChanged();
            }
        }
        public int _index2 { get; set; }
        public int index2
        {
            get { return _index2; }
            set
            {
                _index2 = value+1;
                NotifyPropertyChanged();
                _index2 = value;
                NotifyPropertyChanged();

            }
        }
        public int _index1 { get; set; }
        public int index1
        {
            get { return _index1; }
            set
            {
                _index1 = value + 1;
                NotifyPropertyChanged();
                _index1 = value;
                NotifyPropertyChanged();
            }
        }

        public double _r { get; set; } = 0.1;
        public double r
        {
            get { return _r; }
            set
            {
                _r = value;
                NotifyPropertyChanged();
            }
        }
        public double _r1 { get; set; }
        public double r1
        {
            get { return _r1; }
            set
            {
                _r1 = value;
                NotifyPropertyChanged();
            }
        }
        public double _r2 { get; set; }
        public double r2
        {
            get { return _r2; }
            set
            {
                _r2 = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<string> BtnFilterContacteCommand { get; set; }
        public RelayCommand<string> BtnFilterTelefonCommand { get; set; }
        public RelayCommand<string> BtnFilterEmailCommand { get; set; }
        public RelayCommand<string> BtnTableCommand { get; set; }

        private string _tableChoice = "Contacts";
        public string TableChoice
        {
            get { return _tableChoice; }
            set
            {
                _tableChoice = value;
                swapTable(value);
                NotifyPropertyChanged();
            }
        }

        private string _filterChoiceContacte = "contacteContains";
        public string FilterChoiceContacte
        {
            get { return _filterChoiceContacte; }
            set
            {
                _filterChoiceContacte = value;
                NotifyPropertyChanged();
                FilterContactes();
            }
        }

        private string _filterChoiceTelefon = "telefonContains";
        public string FilterChoiceTelefon
        {
            get { return _filterChoiceTelefon; }
            set
            {
                _filterChoiceTelefon = value;
                NotifyPropertyChanged();
                FilterTelefons();
            }
        }

        private string _filterchoiceEmail = "emailContains";
        public string FilterChoiceEmail
        {
            get { return _filterchoiceEmail; }
            set
            {
                _filterchoiceEmail = value;
                NotifyPropertyChanged();
                FilterEmails();
            }
        }
    }
}
