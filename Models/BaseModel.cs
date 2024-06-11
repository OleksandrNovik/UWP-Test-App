using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SecondApp.Models
{
    /// <summary>
    /// Base model is parent for all models and has base logic needed for a model 
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
