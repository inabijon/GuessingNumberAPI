# GuessingNumberAPI with dotnet-7
 
How to use it

1. You need to create a new user.
2. You need to create a new game using a new user ID.
3. Playing games. Post request is sent using User Id and Game Id.

pay attention!
`{
   "guessNumber": 4535
}`

The body part should only contain `guessNumber` must be 4 digits. example 4030

4. There are 8 attempts per game created. If you fail to find the hidden number in 8 attempts, the result of the game will be automatically released and you will be declared a loser. If you find the hidden number, you will be the winner. You cannot play this game after the result of the game is known. It is necessary to create a new game.

5. Leaderboard part. Results of all players
