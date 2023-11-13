using RubiconmpSolution.Entities.Concrete;

namespace RubiconmpSolution.Business.Constants;

public static class ErrorMessages
{
    public const string InputIsNull = "input array does not exist";
    public const string InputIsEmpty = "input array is empty";
    public const string CommonError = "something went wrong, try again later";
    public const string NoRectanglesFound = "no rectangles found";
    public const string UserNotFound = "user could not be found";
    public const string IncorrectPassword = "password is incorrect";
    public const string CouldNotGetUserClaims = "error while trying to get user claims";
    public const string CouldNotCreateAccessToken = "error while creating access token";
    public const string UsernameAlreadyExist = "username already exists";
    public const string FailedRegistration = "registration was failed";
    public const string FailedUserCreation = "user creation was failed";
    public const string UserCouldNotBeCreated = "user could not be created";
}