using System;

using Ladybug;

namespace xplatform
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new Game())
			{
				game.GraphicsDeviceManager.PreferredBackBufferHeight = 480;
				game.GraphicsDeviceManager.PreferredBackBufferWidth = 480;
				game.GraphicsDeviceManager.ApplyChanges();
				game.LoadScene<MainScene>();
				game.Run();
			}
		}
	}
}
