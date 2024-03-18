using System.ComponentModel.DataAnnotations;

namespace TaskFlapKap.DataTransfareObject.Servicedto
{
	public class ResetPassworddto
	{
		public string Code { get; set; }
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[DataType(DataType.Password)]
		public string ConfirmNewPassword { get; set; }
	}
}
