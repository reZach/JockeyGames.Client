using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JockeyGames.Models.PingPong;
using JockeyGames.Client.ViewModels;
using JockeyGames.Client.Services;
using System.Threading.Tasks;
using JockeyGames.Models.Shared;

namespace JockeyGames.Client.Controllers
{
    public class RecordController : Controller
    {
        private PlayersService playerService = new PlayersService();
        private PlayerGamesService playerGameService = new PlayerGamesService();
        private GamesService gamesService = new GamesService();
        private MatchService matchService = new MatchService();
        private TournamentService tournamentService = new TournamentService();

        // GET: Record
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Match()
        {
            var match = new AddNewMatchViewModel();
            var players = await playerService.GetPlayersAsync();
            var tournaments = await tournamentService.GetTournamentsAsync();

            match.LoadPlayersIntoSelectList(players);
            match.LoadTournamentsIntoSelectList(tournaments);

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Match(AddNewMatchViewModel match)
        {
            Match addMatch = new Match();
            addMatch.DateTime = DateTime.Now;
            addMatch.Games = new List<Game>();

            // Fill in details
            if (ModelState.IsValid)
            {
                try
                {
                    Player player1 = await playerService.GetPlayer(match.PlayerId1);
                    Player player2 = await playerService.GetPlayer(match.PlayerId2);


                    // Game 1
                    PlayerGame game1p1 = new PlayerGame()
                    {
                        Score = match.Game1Player1Score,
                        PlayerId = player1
                    };
                    PlayerGame game1p2 = new PlayerGame()
                    {
                        Score = match.Game1Player2Score,
                        PlayerId = player2
                    };
                    Game game1 = new Game()
                    {
                        PlayerGameId1 = game1p1,
                        PlayerGameId2 = game1p2
                    };
                    //game1p1 = await playerGameService.AddPlayerGame(game1p1);
                    //game1p2 = await playerGameService.AddPlayerGame(game1p2);
                    //game1 = await gamesService.AddGame(game1);
                    addMatch.Games.Add(game1);

                    // Game 2
                    PlayerGame game2p1 = new PlayerGame()
                    {
                        Score = match.Game2Player1Score,
                        PlayerId = player1
                    };
                    PlayerGame game2p2 = new PlayerGame()
                    {
                        Score = match.Game2Player2Score,
                        PlayerId = player2
                    };
                    Game game2 = new Game()
                    {
                        PlayerGameId1 = game2p1,
                        PlayerGameId2 = game2p2
                    };
                    game2p1 = await playerGameService.AddPlayerGame(game2p1);
                    game2p2 = await playerGameService.AddPlayerGame(game2p2);
                    game2 = await gamesService.AddGame(game2);
                    addMatch.Games.Add(game2);


                    // If scores exist, they played a game 3
                    if (match.Game3Player1Score.HasValue && match.Game3Player2Score.HasValue)
                    {
                        // Game 3
                        PlayerGame game3p1 = new PlayerGame()
                        {
                            Score = match.Game3Player1Score.Value,
                            PlayerId = player1
                        };
                        PlayerGame game3p2 = new PlayerGame()
                        {
                            Score = match.Game3Player2Score.Value,
                            PlayerId = player2
                        };
                        Game game3 = new Game()
                        {
                            PlayerGameId1 = game3p1,
                            PlayerGameId2 = game3p2
                        };
                        game3p1 = await playerGameService.AddPlayerGame(game3p1);
                        game3p2 = await playerGameService.AddPlayerGame(game3p2);
                        game3 = await gamesService.AddGame(game3);
                        addMatch.Games.Add(game3);
                    }

                    await matchService.AddMatch(addMatch);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(match);
                }
            }

            return View(match);
        }
    }
}