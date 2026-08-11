using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Innovation
{
	public class AddonPackageSettings : INotifyPropertyChanged
	{
		protected event PropertyChangedEventHandler PropertyChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				PropertyChanged += value;
			}
			remove
			{
				PropertyChanged -= value;
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
