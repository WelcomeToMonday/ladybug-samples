using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ladybug;
using Ladybug.Graphics;
using Ladybug.Input;

public class MainScene : Scene
{
	private Sprite _snakeSprite;
	private KeyboardMonitor _keyboard;

	public MainScene()
	{
		OnInitialize(Initialize);
		OnLoadContent(LoadContent);
		OnUpdate(Update);
		OnDraw(Draw);
	}

	private void Initialize()
	{
		Console.WriteLine("Main Scene Initialized!");
		_keyboard = new KeyboardMonitor();
	}

	private void LoadContent()
	{
		Texture2D snakeTexture = ResourceCatalog.LoadResource<Texture2D>("snake body segment", "image/snake-body");
		_snakeSprite = new Sprite(snakeTexture);
	}

	private void Update(GameTime gameTime)
	{
		_keyboard.BeginUpdate(Keyboard.GetState());

		if (_keyboard.CheckButton(Keys.Up, InputState.Down))
		{
			_snakeSprite.Transform.Move(0, -10);
		}

		if (_keyboard.CheckButton(Keys.Left, InputState.Down))
		{
			_snakeSprite.Transform.Move(-10, 0);
		}

		if (_keyboard.CheckButton(Keys.Right, InputState.Down))
		{
			_snakeSprite.Transform.Move(10, 0);
		}

		if (_keyboard.CheckButton(Keys.Down, InputState.Down))
		{
			_snakeSprite.Transform.Move(0, 10);
		}

		_keyboard.EndUpdate();
	}

	private void Draw(GameTime gameTime)
	{
		SpriteBatch.Begin();
		_snakeSprite.Draw(SpriteBatch);
		SpriteBatch.End();
	}
}