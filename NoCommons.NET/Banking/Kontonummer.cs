using System;
using System.Text;
using NoCommons.Common;

namespace NoCommons.Banking
{
    public class Kontonummer : StringNumber {
        
        public Kontonummer(string kontonummer) : base(kontonummer) {
            
        }

        public string getRegisternummer() {
            return GetValue().Substring(0, 4);
        }

        public string getAccountType() {
            return GetValue().Substring(4, 6);
        }

        public string getKonto() {
            return GetValue().Substring(4, 10);
        }

        public string getGroupedValue() {
            var sb = new StringBuilder();
            sb.Append(getRegisternummer()).Append(Constants.DOT);
            sb.Append(getAccountType()).Append(Constants.DOT);
            sb.Append(getPartAfterAccountType());
            return sb.ToString();
        }

        private string getPartAfterAccountType() {
            return GetValue().Substring(6);
        }

    }

 
}