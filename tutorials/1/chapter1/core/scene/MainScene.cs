using System;
using Microsoft.Xna.Framework;
using Ladybug;

public class MainScene : Scene
{
	public MainScene()
	{
		OnInitialize(Initialize);
		OnUpdate(Update);
		OnDraw(Draw);
	}

	private void Initialize()
	{
		Console.WriteLine("Main Scene Initialized!");
	}

	private void Update(GameTime gameTime)
	{

	}

	private void Draw(GameTime gameTime)
	{

	}
}