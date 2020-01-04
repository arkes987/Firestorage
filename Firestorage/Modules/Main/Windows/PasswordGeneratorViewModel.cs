using Firestorage.Libs;

namespace Firestorage.Modules.Main.Windows
{
	public class PasswordGeneratorViewModel : NotifyObject
    {
		private int _length = 20;
		public int Length
		{
			get => _length;
			set
			{
				if (_length == value) return;
				_length = value;
				OnPropertyChanged(() => Length);
			}
		}

		private bool _lowercase = true;
		public bool Lowercase
		{
			get => _lowercase;
			set
			{
				if (_lowercase == value) return;
				_lowercase = value;
				OnPropertyChanged(() => Lowercase);
			}
		}

		private bool _uppercase = true;
		public bool Uppercase
		{
			get => _uppercase;
			set
			{
				if (_uppercase == value) return;
				_uppercase = value;
				OnPropertyChanged(() => Uppercase);
			}
		}

		private bool _numbers = true;
		public bool Numbers
		{
			get => _numbers;
			set
			{
				if (_numbers == value) return;
				_numbers = value;
				OnPropertyChanged(() => Numbers);
			}
		}

		private bool _special = true;
		public bool Special
		{
			get => _special;
			set
			{
				if (_special == value) return;
				_special = value;
				OnPropertyChanged(() => Special);
			}
		}

		private bool _excludeSimilar = true;
		public bool ExcludeSimilar
		{
			get => _excludeSimilar;
			set
			{
				if (_excludeSimilar == value) return;
				_excludeSimilar = value;
				OnPropertyChanged(() => ExcludeSimilar);
			}
		}

		private bool _excludeAmbigous = true;
		public bool ExcludeAmbigous
		{
			get => _excludeAmbigous = true;
			set
			{
				if (_excludeAmbigous = true == value) return;
				_excludeAmbigous =  value;
				OnPropertyChanged(() => ExcludeAmbigous);
			}
		}



		public PasswordGeneratorViewModel(ref string input)
        {

        }
    }
}
