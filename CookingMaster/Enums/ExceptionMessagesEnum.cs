using System.ComponentModel;

namespace CookingMaster.Enums
{
    public enum ExceptionMessagesEnum
    {
        [Description("Some error occured, please contact the support.")]
        DefaultError = 1,

        [Description("Please check your internet or contact the support.")]
        UnknownError = 2,

        [Description("Please check your internet connection!")]
        hasInternet = 3,

        [Description("Url not found!")]
        httpStatusCodeNotFound = 4,

        [Description("Your email or password is invalid. Please try again.")]
        InvalidEmailOrPass = 5,

        [Description("Please enter your email to proceed.")]
        EmptyEmail = 6,

        [Description("Please enter your password to proceed.")]
        EmptyPassword = 7,

        [Description("Your email is invalid. Please try again.")]
        InvalidEmail = 8,

        [Description("You must be logged in.")]
        MustBeLoggedIn = 9,
    }
}
