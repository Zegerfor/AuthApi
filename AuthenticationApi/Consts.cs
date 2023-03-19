namespace AuthenticationApi
{
    public static class Consts
    {
        public const int UsernameMinLength = 5;
        public const int TitleMaxLength = 255;
        public const int TextMaxLength = 500;

        public const string PasswordRegex =
            @"^(?=.*\d{1})(?=.*[a-z]{1})(?=.*[A-Z]{1})(?=.*[!@#$%^&*{|}?~_=+.-]{1})(?=.*[^a-zA-Z0-9])(?!.*\s).{6,24}$";

        public const string UsernameLengthValidationError = "Имя пользователя должно содержать более 5 символов.";
        public const string EmailValidationError = "Электронная почта должна иметь допустимый формат.";

        public const string PasswordRegisterValidationError =
            "Пароль должен иметь минимум 6 символов, минимум 1 символ верхнего регистра, минимум 1 специальный символ.";
        public const string PasswordLoginValidationError = "Неверный пароль";
        public const string TitleLengthValidator = "Превышен лимит в 255 символов";
        public const string TextLengthValidator = "Превышен лимит в 500 символов";
    }
}
