using System.Text;
using NoCommons.Common;

namespace NoCommons.Banking
{
    public class Kontonummer : StringNumber {
        
        public Kontonummer(string kontonummer) : base(kontonummer) {
            
        }

        public string GetRegisternummer() {
            return GetValue().Substring(0, 4);
        }

        public string GetAccountType() {
            return GetValue().Substring(4, 2);
        }

        public string getKonto() {
            return GetValue().Substring(4, 6);
        }

        public string GetGroupedValue() {
            var sb = new StringBuilder();
            sb.Append(GetRegisternummer()).Append(Constants.DOT);
            sb.Append(GetAccountType()).Append(Constants.DOT);
            sb.Append(GetPartAfterAccountType());
            return sb.ToString();
        }

        private string GetPartAfterAccountType() {
            return GetValue().Substring(6);
        }

    }

 
}