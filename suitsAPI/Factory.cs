using suitsAPI.Models;

namespace suitsAPI
{
    public class Factory
    {
        private Mammoth? _mammoth;
        public Mammoth Mammoth {
            get { 
                _mammoth ??= new Mammoth();
                return _mammoth; 
            }
        }
    }
}
