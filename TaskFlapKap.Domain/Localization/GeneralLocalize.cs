using System.Globalization;

namespace TaskFlapKap.Domain.Localization
{
	public class GeneralLocalize
	{
		public string Localize(string txtAr, string txtEn)
		{
			CultureInfo culture = Thread.CurrentThread.CurrentCulture;
			if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
				return txtAr;
			return txtEn;
		}
	}
}
