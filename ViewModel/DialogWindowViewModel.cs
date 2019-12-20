using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMPractica2.ViewModel
{
    public class DialogWindowViewModel : ViewModelBase, IUserDialogViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public bool IsModal { get; private set; }
        public virtual void RequestClose()
        {
            if (this.OnCloseRequest != null)
            {
                this.OnCloseRequest(this);
            }
            Close();
        }
        public event EventHandler DialogClosing;

        private string _titol;
        public string Titol
        {
            get { return _titol; }
            set
            {
                _titol = value;
                NotifyPropertyChanged();
            }
        }


        private string _tb;
        public string Tb
        {
            get { return _tb; }
            set
            {
                _tb = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OkCommand { get { return new RelayCommand(Ok); } }
        protected virtual void Ok()
        {
            if (this.OnOk != null)
            {
                this.OnOk(this);
            }
            Close();
        }

        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }
        protected virtual void Cancel()
        {
            if (this.OnCancel != null)
            {
                this.OnCancel(this);
            }
            Close();
        }

        public Action<DialogWindowViewModel> OnOk { get; set; }
        public Action<DialogWindowViewModel> OnCancel { get; set; }
        public Action<DialogWindowViewModel> OnCloseRequest { get; set; }

        public DialogWindowViewModel()
        {
            IsModal = true;
        }

        public void Close()
        {
            if (this.DialogClosing != null)
                this.DialogClosing(this, new EventArgs());
        }
    }
}
