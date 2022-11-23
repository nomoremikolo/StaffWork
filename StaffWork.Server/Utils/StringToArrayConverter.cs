namespace StaffWork.Server.Utils
{
    public static class StringToArrayConverter
    {
        public static List<int> StringToArrayOfNumbers(string str)
        {
            if (str == null)
            {
                return null;
            }
            var listOfInt = new List<int>();

            var listOfString = str.Split(' ');

            foreach (var i in listOfString)
            {
                int number;
                if (int.TryParse(i, out number))
                {
                    listOfInt.Add(number);
                }
            }

            return listOfInt;
        }

        public static string ArrayOfNumbersToString(List<int> array)
        {
            var resString = "";

            foreach (var i in array)
            {
                resString += i.ToString() + " ";
            }

            return resString;
        }
    }
}
