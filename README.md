v0.0.1-r05

Can no longer Hit or Stand while isGameOver == true

Most games end with a Winner now. No Winner is possible.

Bet Value now updates when Slider is changed

Cash Value now subtracts or increments the Bet Value on Win/Lose condition

Added isRewarded global variable to GameManager.cs

Added UpdateBet()

<img src="https://i.gyazo.com/0bde061df5cdaca43e235506436eaaaf.jpg">


v0.0.1-r04

Added CardIndex

Added Player and Dealer Hints (Sums card Values)

(Aces not evaluated yet)

Added PlayerValue and DealerValue

Added Dealer/Player Cards 3,4,5

Added Bet Slider

Added Bet Label

Added Player/DealerValue Hint Labels

Added CheckGame() and HideDeck()

Cards are checked against virtual deck

Aces not processed.

Not playable yet.

<img src="https://i.gyazo.com/805e2a038d1f587b17eaec26dc0e5208.jpg">


v0.0.1-r03

Added Card face down texture

Added New Game button to Game Scene

CreateDeck() is now called on Start() of BlackJack.cs

Added 52 prefabs for Card Face Textures

Added DealCards() and DestroyDeck()

on DealCards() 4 random cards are generated.

Not checked against a virtual deck yet.

<img src="https://i.gyazo.com/a9bfcda3e43241383dc8e9376df98f42.jpg">


v0.0.1-r02

Added background

Added Settings panel

Added card textures

Added Hit & Stay buttons to Game Scene

Added Pause, Cash, Record and Player Label to Game Scene

Added TitleText to Game Scene

Created BlackJack.cs

Added CreateDeck() and DealNewGame()
(Unused for now, Using GetKeyDown("n") and bool isNewGame in Update())

Creates 4 Cards when 'N' is pressed in Game Scene

Card, ScriptsObject and UI Buttons are all prefabs now

<img src="https://i.gyazo.com/ec851349c658f4e2149f34ddac891aad.jpg">

v0.0.1-r01

Very first compile. Just a MainMenu and an empty Game Scene

TitleText prefab.

ScoreScript.cs and GameManager.cs created.

<img src="https://i.gyazo.com/24b517fbbf4ec5d603de04226bb04471.png">