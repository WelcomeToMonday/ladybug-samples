using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ladybug;
using Ladybug.Graphics;
using Ladybug.Input;

public class MainScene : Scene
{
	public const int GRID_CELL_SIZE = 16;
	public const int GRID_SIZE = 30;

	private int _tickDelay = 10;
	private int _tickTimer;

	private Snake _snake;
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
		_tickTimer = _tickDelay;
		_snake = new Snake(4, new Vector2(2, 2));
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
			_snake.SetDirection(Direction.Up);
		}

		if (_keyboard.CheckButton(Keys.Left, InputState.Down))
		{
			_snake.SetDirection(Direction.Left);
		}

		if (_keyboard.CheckButton(Keys.Right, InputState.Down))
		{
			_snake.SetDirection(Direction.Right);
		}

		if (_keyboard.CheckButton(Keys.Down, InputState.Down))
		{
			_snake.SetDirection(Direction.Down);
		}

		_keyboard.EndUpdate();

		if (_tickTimer <= 0)
		{
			_snake.Move();
			_tickTimer = _tickDelay;
		}
		else
		{
			_tickTimer--;
		}
	}

	private void Draw(GameTime gameTime)
	{
		SpriteBatch.Begin();

		foreach (var segment in _snake.Positions)
		{
			SpriteBatch.Draw(
				_snakeSprite.Texture,
				new Rectangle((int)segment.X * GRID_CELL_SIZE, (int)segment.Y * GRID_CELL_SIZE, GRID_CELL_SIZE, GRID_CELL_SIZE),
				_snakeSprite.Frame,
				Color.White
			);
		}

		SpriteBatch.End();
	}
}