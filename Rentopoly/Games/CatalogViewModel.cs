using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Rentopoly.Data;

namespace Rentopoly.Games;
public partial class CatalogViewModel : ObservableObject
{
    private BoardGameContext GameContext { get; }
    public ObservableCollection<GameViewModel> Games { get; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveGameCommand))]
    private string? _newGameName;

    public CatalogViewModel(BoardGameContext gameContext)
    {
        GameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
        BindingOperations.EnableCollectionSynchronization(Games, new object());
    }

    public async Task LoadGamesAsync()
    {
        await foreach (var game in GameContext.BoardGames)
        {
            Games.Add(new GameViewModel(game));
        }
    }

    [RelayCommand(CanExecute = nameof(CanSaveGame))]
    private async Task SaveGame()
    {
        BoardGame boardGame = new()
        {
            Name = NewGameName ?? ""
        };

        GameContext.BoardGames.Add(boardGame);
        await GameContext.SaveChangesAsync();
    }

    private bool CanSaveGame() => !string.IsNullOrWhiteSpace(NewGameName);
}