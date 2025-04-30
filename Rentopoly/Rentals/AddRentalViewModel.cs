using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using Rentopoly.Data;

namespace Rentopoly.Rentals;
public partial class AddRentalViewModel : ObservableObject
{
    public ISnackbarMessageQueue MessageQueue { get; }
    private BoardGameContext DbContext { get; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string? _loanedTo;

    public AddRentalViewModel(BoardGameContext dbContext, ISnackbarMessageQueue messageQueue)
    {
        MessageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task OnSubmit()
    {
        if (string.IsNullOrWhiteSpace(LoanedTo))
        {
            return;
        }
        Rental newRental = new()
        {
            LoanedOn = DateTime.Today,
            LoanedTo = LoanedTo
        };
        DbContext.Rentals.Add(newRental);
        await DbContext.SaveChangesAsync();
        LoanedTo = string.Empty;
        MessageQueue.Enqueue("Saved!");
    }

    private bool CanSubmit() => !string.IsNullOrWhiteSpace(LoanedTo);
}

