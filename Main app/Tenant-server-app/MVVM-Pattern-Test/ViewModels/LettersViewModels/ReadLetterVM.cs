using WpfApp1.Server.Packages.Letters;

namespace MVVM_Pattern_Test.ViewModels.LettersViewModels
{
    public class ReadLetterVM : BaseVM
    {
        public ReadLetterVM(Letter readLetter)
        {
            ReadLetter = readLetter;
        }
        public Letter ReadLetter { get { return _letter; } private set { _letter = value; OnPropertyChanged(); } }
        private Letter _letter;
        public override string Notice { get { return _infoMessage; } protected set { _infoMessage = value; OnPropertyChanged(); } }
    }
}
