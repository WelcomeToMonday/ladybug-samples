using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

public class Snake
{
	public Snake(int length, Vector2 startPosition)
	{
		Length = length;
		Positions.Add(startPosition);
	}

	public List<Vector2> Positions { get; private set; } = new List<Vector2>();

	public Vector2 Head { get => Positions[0]; }
	public Vector2 Tail { get => Positions[Positions.Count - 1]; }

	public Direction CurrentDirection { get; private set; }

	public Direction NextDirection { get; private set; } = Direction.Right;

	public int Length { get; private set; }

	public void SetDirection(Direction newDirection)
	{
		if (newDirection == CurrentDirection)
		{
			return;
		}

		switch (newDirection)
		{
			case Direction.Up:
				if (CurrentDirection != Direction.Down)
				{
					NextDirection = newDirection;
				}
				break;
			case Direction.Down:
				if (CurrentDirection != Direction.Up)
				{
					NextDirection = newDirection;
				}
				break;
			case Direction.Left:
				if (CurrentDirection != Direction.Right)
				{
					NextDirection = newDirection;
				}
				break;
			case Direction.Right:
				if (CurrentDirection != Direction.Left)
				{
					NextDirection = newDirection;
				}
				break;
		}
	}

	public void Move()
	{
		var newX = Head.X;
		var newY = Head.Y;

		CurrentDirection = NextDirection;

		switch (CurrentDirection)
		{
			case Direction.Up:
				newY = Head.Y - 1;
				break;
			case Direction.Down:
				newY = Head.Y + 1;
				break;
			case Direction.Left:
				newX = Head.X - 1;
				break;
			case Direction.Right:
				newX = Head.X + 1;
				break;
		}

		var newPos = new Vector2(newX, newY);
		Positions.Insert(0, newPos);

		if (Positions.Count > Length)
		{
			Positions.Remove(Tail);
		}
	}

	public void Grow() => Length++;

	private bool CheckCollision(Vector2 checkPos)
	{
		return
			checkPos.X < 0 || checkPos.X > MainScene.GRID_SIZE - 1 ||
			checkPos.Y < 0 || checkPos.Y > MainScene.GRID_SIZE - 1 ||
			Positions.Contains(checkPos);
	}
}