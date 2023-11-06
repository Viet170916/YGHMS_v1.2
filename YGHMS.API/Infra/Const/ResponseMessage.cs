namespace YGHMS.API.Infra.Const
{
	public static class ResponseMessage
	{
		public const string LOGIN_FAIL = "You have entered the wrong account or password!";
		public const string LOGIN_SUCCESS = "Login successfully";
		public const string REPASSWORD_DONT_MATCH = "Re-entered password dont match!";
		public const string WRONG_PASSWORD_FORMAT = "Wrong password format";
		public const string PASSWORD_MUST_DIFFERENT = "The new password must be different from the old password";
		public const string PASSWORD_CHANGED = "Password changed successfully!";
		public const string USERNAME_ALREADY_EXIST = "Username already exists";
		public const string UNAUTHORIZED = "User unauthorized";
		public const string COMMON_ERROR = "An error has occurred";
		public const string WRONG_EMAIL_FORMAT = "Wrong email format";
		public const string EMAIL_ALREADY_EXIST = "Email already exists";
		public const string USER_NOT_FOUND = "User not found";
		public const string WRONG_PASSWORD = "Wrong password";
		public const string UPDATE_SUCCESSFULLY = "Update successfully!";
		public const string LIMITED_LENGTH = "Limited length";
		public const string INVALID_PHONENUMBER = "Invalid phone number";
		public const string NOT_FOUND = "Not found";
		public const string CANNOT_FIND_POST = "Cannot find post";
		public const string NEW_EXPIRATION_DATE_MUST_BE_AFTER_OLD_EXPIRATION_DATE = "The new expiration date must be after the old expiration date";
		public const string NEW_EXPIRATION_DATE_MUST_BE_AFTER_TODAY_DATE = "The expiration date must be after today's date";
		public const string ACCOUNT_DELETED = "Account Deleted!";
		public const string USERNAME_MUST_HAS_5_OR_MORE_CHARACTERS = "Username must has five or more characters";
		public const string ALREADY_REVIEWED = "You have already reviewed it!";
		public const string REVIEW_DOES_NOT_EXIST = "Review does not exist";
	}
}
