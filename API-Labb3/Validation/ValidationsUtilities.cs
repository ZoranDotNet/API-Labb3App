namespace API_Labb3.Validation
{
    public class ValidationsUtilities
    {
        public static string NonEmptyMessage = "The field {PropertyName} is required!";
        public static string MaximumLengthMessage = "The field {PropertyName} should be less than {MaxLength} characters";
        public static string FirstLetterIsUpperCaseMessage = "The field {PropertyName} should start with uppercase";
        public static string EmailMessage = "The field {PropertyName} is not a valid email address";

        public static bool FirstLetterIsUpperCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            var firstLetter = value[0].ToString();
            return firstLetter == firstLetter.ToUpper();
        }


    }
}
