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

            BtnContacteCommand = new RelayCommand<string>(BtnContacteClick);
            BtnTelefonCommand = new RelayCommand<string>(BtnTelefonClick);
            BtnEmailCommand = new RelayCommand<string>(BtnEmailClick);

            BtnTableCommand = new RelayCommand<string>(BtnTableClick);
        }

        public RelayCommand<string> BtnFilterContacteCommand { get; set; }
        public RelayCommand<string> BtnFilterTelefonCommand { get; set; }
        public RelayCommand<string> BtnFilterEmailCommand { get; set; }
        public RelayCommand<string> BtnTableCommand { get; set; }
        public RelayCommand<string> BtnContacteCommand { get; set; }
        public RelayCommand<string> BtnEmailCommand { get; set; }
        public RelayCommand<string> BtnTelefonCommand { get; set; }

        public void refreshTables()
        {
            contactesPopulate();
            emailsPopulate();
            telefonsPopulate();
        }

        public void BtnContacteClick(string btName)
        {
            contacte c = new contacte();
            switch (btName)
            {
                case "addContacte":
                    c.nom = contacteNom;
                    c.cognoms = contacteCognoms;
                    db.contactes.Add(c);
                    db.SaveChanges();
                    TableChoice = "Contacts";
                    break;
                case "removeContacte":
                    c = db.contactes.Find(SelectedContacte.contacteId);

                    foreach (telefon t in db.telefons.Where(x => x.contacteId == c.contacteId))
                    {
                        db.telefons.Remove(t);
                    }
                    foreach (email e in db.emails.Where(x => x.contacteId == c.contacteId))
                    {
                        db.emails.Remove(e);
                    }

                    db.contactes.Remove(c);
                    db.SaveChanges();

                    if (TableChoice.Equals("Telefons"))
                    {
                        telefonsPopulate();
                    } else if(TableChoice.Equals("Emails")) {
                        emailsPopulate();
                    } else {
                        contactesPopulate();
                    }
                    break;
                case "modifyContacte":
                    c = db.contactes.Find(SelectedContacte.contacteId);
                    c.nom = contacteNom;
                    c.cognoms = contacteCognoms;
                    db.SaveChanges();
                    if (TableChoice.Equals("Telefons"))
                    {
                        contacteTelefons();
                    }
                    else if (TableChoice.Equals("Emails"))
                    {
                        contacteEmails();
                    }
                    else
                    {
                        contactesPopulate();
                    }
                    break;
                case "duplicateContacte":
                    c = db.contactes.Find(SelectedContacte.contacteId);
                    contacte c0 = new contacte();
                    int new_id = db.contactes.OrderByDescending(x => x.contacteId).Select(x => x.contacteId).FirstOrDefault()+1;

                    c0.nom = c.nom;
                    c0.cognoms = c.cognoms;

                    // collections arent duplicating
                    
                    //foreach(email e in c.emails.ToList())
                    //{
                    //    c0.emails.Add(e);
                    //}

                    //foreach(telefon t in c.telefons.ToList())
                    //{
                    //    telefon telefon0 = t;
                    //    telefon0.contacteId = new_id;
                    //    telefons0.Add(telefon0);
                    //}

                    //c0.telefons = telefons0;
                    // c0.emails = emails0;

                    //db.contactes.Add(c0);
                    //db.SaveChanges();
                    //TableChoice = "Contacts";
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }
        public void BtnEmailClick(string btName)
        {
            email e = new email();
            switch (btName)
            {
                case "addEmail":
                    e.email1 = emailEmail1;
                    e.tipus = emailTipus;
                    e.contacteId = SelectedContacte.contacteId;

                    db.emails.Add(e);
                    db.SaveChanges();

                    if (TableChoice.Equals("Emails"))
                    {
                        emailsPopulate();
                    }
                    else
                    {
                        emailsContacte();
                    }
                    break;
                case "removeEmail":
                    e = db.emails.Find(SelectedEmail.emailId);

                    db.emails.Remove(e);
                    db.SaveChanges();

                    if (TableChoice.Equals("Emails"))
                    {
                        emailsPopulate();
                    }else {
                        emailsContacte();
                    }
                    break;
                case "modifyEmail":
                    e = db.emails.Find(SelectedEmail.emailId);

                    e.email1 = emailEmail1;
                    e.tipus = emailTipus;

                    db.SaveChanges();

                    if (TableChoice.Equals("Emails"))
                    {
                        emailsPopulate();
                    }
                    else
                    {
                        emailsContacte();
                    }
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }
        public void BtnTelefonClick(string btName)
        {
            telefon t = new telefon();
            switch (btName)
            {
                case "addTelefon":
                    t.telefon1 = telefonTelefon1;
                    t.tipus = telefonTipus;
                    t.contacteId = SelectedContacte.contacteId;

                    db.telefons.Add(t);
                    db.SaveChanges();

                    if (TableChoice.Equals("Telefons"))
                    {
                        telefonsPopulate();
                    }
                    else
                    {
                        telefonsContacte();
                    }
                    break;
                case "removeTelefon":
                    t = db.telefons.Find(SelectedTelefon.telId);

                    db.telefons.Remove(t);
                    db.SaveChanges();

                    if (TableChoice.Equals("Telefons"))
                    {
                        telefonsPopulate();
                    }
                    else
                    {
                        telefonsContacte();
                    }
                    break;
                case "modifyTelefon":
                    t = db.telefons.Find(SelectedTelefon.telId);

                    t.telefon1 = telefonTelefon1;
                    t.tipus = telefonTipus;

                    db.SaveChanges();

                    if (TableChoice.Equals("Telefons"))
                    {
                        telefonsPopulate();
                    }
                    else
                    {
                        telefonsContacte();
                    }
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
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
                    ContacteChecked = true;
                    break;
                case "Telefons":
                    telefonsPopulate();
                    r1 = 0.1;
                    index1 = 0;
                    TelefonChecked = true;
                    break;
                case "Emails":
                    emailsPopulate();
                    r2 = 0.1;
                    index2 = 0;
                    EmailChecked = true;
                    break;
                default:
                    Console.WriteLine("error");
                    break;
            }

        }

        private void FilterContactes()
        {
            string filter = TextFilterContacte;
            TableChoice = "Contacts";
            List<contacte> contactesFiltering = new List<contacte>();
            try
            {
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
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            contactes.Clear();
            contactes.AddRange(contactesFiltering);

        }

        private void FilterEmails()
        {
            string filter = TextFilterEmail;
            TableChoice = "Emails";
            List<email> emailsFiltering = new List<email>();
            try
            {
                switch (FilterChoiceEmail)
                {
                    case "emailStartsWith":
                        foreach (email e in emails)
                        {
                            if (e.email1.ToLower().StartsWith(filter.ToLower()) || e.tipus.ToLower().EndsWith(filter.ToLower()))
                            {
                                emailsFiltering.Add(e);
                            }
                        }
                        break;
                    case "emailContains":
                        foreach (email e in emails)
                        {
                            if (e.email1.ToLower().Contains(filter.ToLower()) || e.tipus.ToLower().EndsWith(filter.ToLower()))
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
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            emails.Clear();
            emails.AddRange(emailsFiltering);
        }

        private void FilterTelefons()
        {
            string filter = TextFilterTelefon;
            TableChoice = "Telefons";
            List<telefon> telefonsFiltering = new List<telefon>();
            try
            {
                switch (FilterChoiceTelefon)
                {
                    case "telefonStartsWith":
                        foreach (telefon t in telefons)
                        {
                            if (t.telefon1.ToLower().StartsWith(filter.ToLower()) || t.tipus.ToLower().EndsWith(filter.ToLower()))
                            {
                                telefonsFiltering.Add(t);
                            }
                        }
                        break;
                    case "telefonContains":
                        foreach (telefon t in telefons)
                        {
                            if (t.telefon1.ToLower().Contains(filter.ToLower()) || t.tipus.ToLower().EndsWith(filter.ToLower()))
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
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
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
                contacteNom = value != null ? value.nom : "";
                contacteCognoms = value != null ? value.cognoms : "";
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
                emailEmail1 = value != null ? value.email1 : "";
                emailTipus = value != null ? value.tipus : "";
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
                telefonTelefon1 = value != null ? value.telefon1 : "";
                telefonTipus = value != null ? value.tipus : "";
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

        private bool _contacteChecked { get; set; } = true;
        public bool ContacteChecked
        {
            get { return _contacteChecked; }
            set
            {
                _contacteChecked = value;
                NotifyPropertyChanged();
            }
        }
        private bool _telefonChecked { get; set; } = false;
        public bool TelefonChecked
        {
            get { return _telefonChecked; }
            set
            {
                _telefonChecked = value;
                NotifyPropertyChanged();
            }
        }
        private bool _emailChecked { get; set; } = false;
        public bool EmailChecked
        {
            get { return _emailChecked; }
            set
            {
                _emailChecked = value;
                NotifyPropertyChanged();
            }
        }

        private string _contacteNom { get; set; } = "";
        public string contacteNom
        {
            get { return _contacteNom; }
            set
            {
                _contacteNom = value;
                NotifyPropertyChanged();
            }
        }
        private string _contacteCognoms { get; set; } = "";
        public string contacteCognoms
        {
            get { return _contacteCognoms; }
            set
            {
                _contacteCognoms = value;
                NotifyPropertyChanged();
            }
        }
        private string _emailEmail1 { get; set; } = "";
        public string emailEmail1
        {
            get { return _emailEmail1; }
            set
            {
                _emailEmail1 = value;
                NotifyPropertyChanged();
            }
        }
        private string _emailTipus { get; set; } = "";
        public string emailTipus
        {
            get { return _emailTipus; }
            set
            {
                _emailTipus = value;
                NotifyPropertyChanged();
            }
        }
        private string _telefonTelefon1 { get; set; } = "";
        public string telefonTelefon1
        {
            get { return _telefonTelefon1; }
            set
            {
                _telefonTelefon1 = value;
                NotifyPropertyChanged();
            }
        }
        private string _telefonTipus { get; set; } = "";
        public string telefonTipus
        {
            get { return _telefonTipus; }
            set
            {
                _telefonTipus = value;
                NotifyPropertyChanged();
            }
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
    }
}
