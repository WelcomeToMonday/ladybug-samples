using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Ladybug;
using Ladybug.Input;
using Ladybug.Graphics;

public class MainScene : Scene
{
	public const int GRID_CELL_SIZE = 16; // size of each grid cell in pixels
	public const int GRID_SIZE = 30; // size of grid by cell count

	private int _snakeStartSize = 4;

	private Vector2 _snakeStartLocation = new Vector2(2, 2);

	private Vector2 _appleLocation = Vector2.Zero;

	private Sprite _snakeSprite;
	private Sprite _appleSprite;

	private Snake _snake;

	private int _tickDelay = 10;
	private int _tickTimer;

	private int _score;

	private SpriteFont _scoreFont;

	private KeyboardMonitor _keyboard;

	private Random _random = new Random();

	public MainScene()
	{
		OnLoadContent(LoadContent);
		OnInitialize(Initialize);
		OnUpdate(Update);
		OnDraw(Draw);
	}

	private void LoadContent()
	{
		ResourceCatalog.LoadResource<Texture2D>("snake body", "image/snake-body");
		ResourceCatalog.LoadResource<Texture2D>("apple", "image/apple");
		_scoreFont = ResourceCatalog.LoadResource<SpriteFont>("font", "font/default-font");
	}

	private void Initialize()
	{
		_snakeSprite = new Sprite(ResourceCatalog.GetResource<Texture2D>("snake body"));
		_appleSprite = new Sprite(ResourceCatalog.GetResource<Texture2D>("apple"));
		_keyboard = new KeyboardMonitor();
		_tickTimer = _tickDelay;
		NewGame();
	}
	
	private void NewGame()
	{
		_snake = new Snake(_snakeStartSize, _snakeStartLocation);
		_snake.Collide += OnSnakeCollide;
		_score = 0;
		PlaceApple();
	}

	private void PlaceApple()
	{
		Vector2 location;
		
		do
		{
			location = new Vector2(_random.Next(0, GRID_SIZE), _random.Next(0, GRID_SIZE));
		}
		while (location.Y == 0 || _snake.Positions.Contains(location)); // We prevent the apple from spawning at Y=0 so that it doesn't obfuscate the Score

		_appleLocation = location;
	}

	private void OnSnakeCollide(object sender, EventArgs e)
	{
		NewGame();
	}

	private void Update(GameTime gameTime)
	{
		_keyboard.BeginUpdate(Keyboard.GetState());

		if (_keyboard.CheckButton(Keys.Up, InputState.Released))
		{
			_snake.SetDirection(Direction.Up);
		}
		if (_keyboard.CheckButton(Keys.Down, InputState.Released))
		{
			_snake.SetDirection(Direction.Down);
		}
		if (_keyboard.CheckButton(Keys.Left, InputState.Released))
		{
			_snake.SetDirection(Direction.Left);
		}
		if (_keyboard.CheckButton(Keys.Right, InputState.Released))
		{
			_snake.SetDirection(Direction.Right);
		}

		_keyboard.EndUpdate();

		if (_tickTimer <= 0)
		{
			_snake.Move();
			
			if (_snake.Positions.Contains(_appleLocation))
			{
				_snake.Grow();
				_score++;
				PlaceApple();
			}
			
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

		SpriteBatch.Draw(
			_appleSprite.Texture,
			new Rectangle((int)_appleLocation.X * GRID_CELL_SIZE, (int)_appleLocation.Y * GRID_CELL_SIZE, GRID_CELL_SIZE, GRID_CELL_SIZE),
			_appleSprite.Frame,
			Color.White
		);

		SpriteBatch.DrawString(_scoreFont, $"Score: {_score}", Vector2.Zero, Color.LightGoldenrodYellow);

		SpriteBatch.End();
	}
}