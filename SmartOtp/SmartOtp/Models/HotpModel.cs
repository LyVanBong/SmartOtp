using OtpNet;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartOtp.Models
{
    public class HotpModel : INotifyPropertyChanged
    {
        private long _counter = 9999;
        private bool _isSha1 = true;
        private bool _isSha256;
        private bool _isSha512;
        private string _secret;
        private int _digits = 6;

        public int Digits
        {
            get => _digits;
            set => SetField(ref _digits, value);
        }

        public string Secret
        {
            get => _secret;
            set => SetField(ref _secret, value);
        }

        public bool IsSha512
        {
            get => _isSha512;
            set => SetField(ref _isSha512, value);
        }

        public bool IsSha256
        {
            get => _isSha256;
            set => SetField(ref _isSha256, value);
        }

        public bool IsSha1
        {
            get => _isSha1;
            set
            {
                if (value == _isSha1) return;
                _isSha1 = value;
                OnPropertyChanged();
            }
        }

        public long Counter
        {
            get => _counter;
            set => SetField(ref _counter, value);
        }


        public OtpHashMode OtpMode()
        {
            if (IsSha256)
                return OtpHashMode.Sha256;
            if (IsSha512)
                return OtpHashMode.Sha512;
            return OtpHashMode.Sha1;
        }
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}