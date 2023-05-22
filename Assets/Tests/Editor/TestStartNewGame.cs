using AltTester.AltTesterUnitySDK.Driver;
using NUnit.Framework;

public class StartNewGameTests {

	private AltDriver altDriver;

	[OneTimeSetUp]
	public void OneTimeSetUp() {
		altDriver = new AltDriver();
	}

	[OneTimeTearDown]
	public void TearDown() {
		altDriver.Stop();
	}

	[Test]
	public void TestPlayButtonStartANewGame() {

		altDriver.LoadScene("StartScene", true);

		altDriver.WaitForObject(By.PATH, "/Canvas/MainContainer/Logo");
		altDriver.WaitForObject(By.PATH, "/Canvas/MainContainer/InfoText/Title");
		altDriver.WaitForObject(By.PATH, "//InfoText/Description");
		
		var PlayButton = altDriver.WaitForObject(By.NAME, "PlayButton");
		PlayButton.Click();

		altDriver.WaitForCurrentSceneToBe("PlayScene");
	}
}