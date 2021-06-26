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
				game.LoadScene<MainScene>();
				game.Run();
			}
		}
	}
}
