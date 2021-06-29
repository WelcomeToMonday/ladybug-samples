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
				game.GraphicsDeviceManager.PreferredBackBufferHeight = MainScene.GRID_CELL_SIZE * MainScene.GRID_SIZE;
				game.GraphicsDeviceManager.PreferredBackBufferWidth = MainScene.GRID_CELL_SIZE * MainScene.GRID_SIZE;
				game.GraphicsDeviceManager.ApplyChanges();

				game.LoadScene<MainScene>();
				game.Run();
			}
		}
	}
}
