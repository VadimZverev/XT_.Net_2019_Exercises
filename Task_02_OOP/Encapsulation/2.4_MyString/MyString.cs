namespace _24_MyString
{
    class MyString
    {
        #region Поля

        char[] chars;

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
            if (firstStr.Length != secondStr.Length)
            {
                return false;
            }

            for (int i = 0; i < firstStr.Length; i++)
            {
                if (firstStr[i] != secondStr[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(MyString firstStr, MyString secondStr)
        {
            if (firstStr.Length != secondStr.Length)
            {
                return true;
            }

            for (int i = 0; i < firstStr.Length; i++)
            {
                if (firstStr[i] != secondStr[i])
                    return true;
            }

            return false;
        }

        public static MyString operator +(MyString firstStr, MyString secondStr)
        {
            int tempIndex = firstStr.Length + secondStr.Length;
            MyString tempStr = new MyString
            {
                chars = new char[tempIndex]
            };

            for (int i = 0; i < firstStr.Length; i++)
            {
                tempStr[i] = firstStr[i];
            }

            for (int i = 0, j = firstStr.Length; j < tempIndex; i++, j++)
            {
                tempStr[j] = secondStr[i];
            }

            return tempStr;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает индекс первого вхождения символа в строке.
        /// </summary>
        /// <param name="ch">Искомый символ.</param>
        public int FindFirst(char ch)
        {
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] == ch)
                    return i;
            return -1;
        }

        /// <summary>
        /// Возвращает индекс вхождения символа с конца.
        /// </summary>
        /// <param name="ch">Искомый символ.</param>
        public int FindLast(char ch)
        {
            for (int i = chars.Length - 1; i >= 0; i--)
                if (chars[i] == ch)
                    return i;
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
        /// Возвращает кастомную строку из массива символов.
        /// </summary>
        public static MyString ToString(char[] charsArray) => new MyString(charsArray);

        #endregion
    }
}
