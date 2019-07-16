namespace _24_MyString
{
    class MyString
    {
        #region Поля

        private char[] chars;

        #endregion

        #region Конструкторы

        public MyString()
        {
            chars = new char[0];
        }

        public MyString(char[] charsArray)
        {
            chars = charsArray;
        }

        public MyString(string str)
        {
            chars = str.ToCharArray();
        }

        #endregion

        #region Индексатор

        public char this[int index]
        {
            get => chars[index];
            set => chars[index] = value;
        }

        #endregion

        #region Свойства

        public int Length => chars.Length;

        #endregion

        #region Операторы

        public static bool operator >(MyString firstStr, MyString secondStr)
            => firstStr.Length > secondStr.Length;

        public static bool operator <(MyString firstStr, MyString secondStr)
            => firstStr.Length < secondStr.Length;

        public static bool operator ==(MyString firstStr, MyString secondStr)
        {
            return firstStr.Equals(secondStr);
        }

        public static bool operator !=(MyString firstStr, MyString secondStr)
        {
            return !firstStr.Equals(secondStr);
        }

        public static MyString operator +(MyString firstStr, MyString secondStr)
        {
            int temp = firstStr.Length + secondStr.Length;
            char[] chars = new char[temp];

            temp = firstStr.Length > secondStr.Length ? firstStr.Length : secondStr.Length;

            for (int i = 0; i < temp; i++)
            {
                if (i < firstStr.Length)
                {
                    chars[i] = firstStr[i];
                }

                if (i < secondStr.Length)
                {
                    chars[firstStr.Length + i] = secondStr[i];
                }
            }

            return new MyString(chars);
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает индекс первого вхождения символа в строке.
        /// </summary>
        /// <param name="symbol">Искомый символ.</param>
        public int FindFirst(char symbol)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == symbol)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Возвращает индекс вхождения символа с конца.
        /// </summary>
        /// <param name="symbol">Искомый символ.</param>
        public int FindLast(char symbol)
        {
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                if (chars[i] == symbol)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Возвращает массив символов из строки.
        /// </summary>
        public char[] ToCharArray() => chars;

        /// <summary>
        /// Возвращает строку символов.
        /// </summary>
        public override string ToString() => new string(chars);

        /// <summary>
        /// Возвращает строку из массива символов.
        /// </summary>
        public static MyString ToMyString(char[] charsArray) => new MyString(charsArray);

        public override bool Equals(object obj)
        {
            if (obj is MyString myStr)
            {
                return Equals(myStr);
            }

            return false;
        }

        public bool Equals(MyString value)
        {
            if (this.Length == value.Length)
            {

                return GetHashCode() == value.GetHashCode();

                #region Альтернатива
                //for (int i = 0; i < this.Length; i++)
                //{
                //    if (chars[i] != value.chars[i])
                //    {
                //        return false;
                //    }
                //} 
                #endregion
            }

            return true;
        }

        public override int GetHashCode()
        {
            int n = 0;

            foreach (int item in chars)
            {
                n += item;
            }

            return n;
        }

        #endregion
    }
}
