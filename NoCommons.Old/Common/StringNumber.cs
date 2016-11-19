using System;

namespace NoCommons.Common
{
    public abstract class StringNumber {

        private readonly string value;

        protected StringNumber(string value) {
            this.value = value;
        }

        public string GetValue() {
            return value;
        }

        public int GetAt(int i) {
            return GetValue()[i] - '0';
        }

        public override string ToString() {
            return GetValue();
        }

        public override int GetHashCode() {
            const int prime = 31;
            int result = 1;
            result = prime * result + ((value == null) ? 0 : value.GetHashCode());
            return result;
        }

        public override bool Equals(Object obj) {
            if (this == obj) {
                return true;
            }
            if (obj == null) {
                return false;
            }
            if (GetType() != obj.GetType()) {
                return false;
            }
            var other = (StringNumber) obj;
            if (value == null) {
                if (other.value != null) {
                    return false;
                }
            } else if (!value.Equals(other.value)) {
                return false;
            }
            return true;
        }

        public int GetLength() {
            return GetValue().Length;
        }

        public int GetChecksumDigit() {
            return GetAt(GetLength()-1);
        }

 
    }
}