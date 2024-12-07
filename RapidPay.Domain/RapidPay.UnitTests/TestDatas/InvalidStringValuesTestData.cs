namespace RapidPay.UnitTests.TestDatas;

public class InvalidStringValuesTestData : TheoryData<string?>
{
	public InvalidStringValuesTestData()
	{
		Add(null);
		Add(string.Empty);
		Add(" ");
	}
}
