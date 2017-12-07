using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientLogin.PassowrdChecker
{
    public static class PasswordCheck
    {
        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }
        public static class PasswordAdvisor
        {
            public static PasswordScore CheckStrength(string password)
            {
                int score = 0;

                if (password.Length < 1)
                    return PasswordScore.Blank;
                if (password.Length < 4)
                    return PasswordScore.VeryWeak;

                if (password.Length >= 8)
                    score++;
                if (password.Length >= 12)
                    score++;
                if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
                  Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                    score++;

                return (PasswordScore)score;
            }

        }

        public static class PasswordValidate
        {
            public static int ValidatePassword(string password)
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    return 0;
                }

                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMiniMaxChars = new Regex(@".{8,10}");
                var hasLowerChar = new Regex(@"[a-z]+");
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                if (!hasLowerChar.IsMatch(password))
                {
                    return 1;
                }

                if (!hasUpperChar.IsMatch(password))
                {
                    return 2;
                }

                if (!hasMiniMaxChars.IsMatch(password))
                {
                    return 3;
                }

                if (!hasNumber.IsMatch(password))
                {
                    return 4;
                }

                if (!hasSymbols.IsMatch(password))
                {
                    return 5;
                }

                return 6;
            }
        }
    }
}