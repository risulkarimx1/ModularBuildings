namespace ModularBuildings
{
	public class BlockSurrounding
	{
		private string[,,] _floors;

		private int _facadeLength;
		private int _facadeHeight;
		private int _facadeDepth;

		public BlockSurrounding(string[,,] floors, int facadeLength, int facadeHeight, int facadeDepth)
		{
			_floors = floors;
			_facadeLength = facadeLength;
			_facadeHeight = facadeHeight;
			_facadeDepth = facadeDepth;
		}

		public bool HasGapInLeft { get; private set; }
		public bool HasGapInRight { get; private set; }
		public bool HasGapInfront { get; private set; }
		public bool HasGapInBack { get; private set; }

		public bool HasWindowInleft { get; private set; }
		public bool HasWindowInRight { get; private set; }
		public bool HasWindowInfront { get; private set; }
		public bool HasWindowInBack { get; private set; }

		public bool HasLeftCornerOrDepthInBack { get; private set; }
		public bool HasRightCornerOrDepthInBack { get; private set; }

		public bool IsTop { get; private set; }

		// Utility Function
		public void ApplyCondition(int i, int j, int k)
		{
			HasGapInLeft = i == 0 || _floors[i - 1, j, k] == BlockSymbol.Gap; // if its the left most block or the block on the left has a gap
			HasGapInRight = i == _facadeLength - 1 || _floors[i + 1, j, k] == BlockSymbol.Gap; // if its the rightmost block OR right next block is a gap
			HasGapInfront = k == _facadeDepth - 1 || _floors[i, j, k + 1] == BlockSymbol.Gap; // if the block is in the border or Block infront is a gap
			HasGapInBack = k == 0 || _floors[i, j, k - 1] == BlockSymbol.Gap; // if its the first row OR there is a gap in the back

			HasWindowInleft = i > 0 && _floors[i - 1, j, k] == BlockSymbol.Window; // if its not the left most block AND left next block is a window
			HasWindowInRight = i < _facadeLength - 1 && _floors[i + 1, j, k] == BlockSymbol.Window; // if its not the Right most block AND right next block is a window
			HasWindowInfront = k < _facadeDepth - 1 && _floors[i, j, k + 1] == BlockSymbol.Window; // if the block is within the range AND next block is a window
			HasWindowInBack = k > 0 && _floors[i, j, k - 1] == BlockSymbol.Window; // if its more than first row AND block in the back is a window

			// if its more than first row AND block in the back is a Left Corner or a left depth
			HasLeftCornerOrDepthInBack = k > 0 && (_floors[i, j, k - 1] == BlockSymbol.CornerLeft || _floors[i, j, k - 1] == BlockSymbol.DepthLeft);
			// if its more than first row AND block in the back is a Left Corner or a left depth
			HasRightCornerOrDepthInBack = k > 0 && (_floors[i, j, k - 1] == BlockSymbol.CornerLeft.HorizontalMirror() ||
													_floors[i, j, k - 1] == BlockSymbol.DepthLeft.HorizontalMirror());

			IsTop = j == _facadeHeight - 1 || _floors[i, j + 1, k] == BlockSymbol.Gap; // 
		}
	}
}
