using System;
using System.Linq;
using System.Reactive;
using Ledger.App.Models;
using ReactiveUI;

namespace Ledger.App.ViewModels;

public class TodayViewModel : ViewModelBase
{
    private bool _isPaneOpen = false;

    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
    }

    private bool _isEditable;

    public bool IsEditable
    {
        get => _isEditable;
        set => this.RaiseAndSetIfChanged(ref _isEditable, value);
    }

    private bool _editBtnVis;

    public bool EditBtnVis
    {
        get => _editBtnVis;
        set => this.RaiseAndSetIfChanged(ref _editBtnVis, value);
    }

    private int _dataGridSelectedIndex;

    public int DataGridSelectedIndex
    {
        get => _dataGridSelectedIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);
            IsPaneOpen = DataGridSelectedIndex >= 0;
        }
    }

    private string _taskName;

    public string TaskName
    {
        get => _taskName;
        set => this.RaiseAndSetIfChanged(ref _taskName, value);
    }

    private LedgerModel _selectedLedger;

    public LedgerModel SelectedLedger
    {
        get => _selectedLedger;
        set => this.RaiseAndSetIfChanged(ref _selectedLedger, value);
    }

    private Database _database;

    public Database Database
    {
        get => _database;
        set => this.RaiseAndSetIfChanged(ref _database, value);
    }

    public ReactiveCommand<Unit, Unit> CloseCmd { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewLedgerCmd { get; set; }
    public ReactiveCommand<Unit, Unit> EditSelectedLedgerCmd { get; set; }
    public ReactiveCommand<Unit, Unit> SaveSelectedLedgerCmd { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteSelectedLedgerCmd { get; set; }


    public TodayViewModel()
    {
        _database = new Database();
        _database.GetMyTodaysLedger();
        _dataGridSelectedIndex = -1;
        _taskName = "";
        _isEditable = false;
        _editBtnVis = true;
        _isPaneOpen = false;
        CloseCmd = ReactiveCommand.Create(CloseBtnClk);
        AddNewLedgerCmd = ReactiveCommand.Create(AddNewLedgerBtnClk);
        EditSelectedLedgerCmd = ReactiveCommand.Create(EditSelectedLedgerBtnClk);
        SaveSelectedLedgerCmd = ReactiveCommand.Create(SaveSelectedLedgerBtnClk);
        DeleteSelectedLedgerCmd = ReactiveCommand.Create(DeleteSelectedLedgerBtnClk);
    }

    private void EditSelectedLedgerBtnClk()
    {
        IsEditable = true;
        EditBtnVis = false;
    }

    private void SaveSelectedLedgerBtnClk()
    {
        var res = Database.UpdateLedgerRecord(SelectedLedger);
        if (res <= 0) return;
        EditBtnVis = true;
        IsEditable = false;
    }

    private void DeleteSelectedLedgerBtnClk()
    {
        var res = Database.DeleteLedgerRecord(SelectedLedger);
        if (res <= 0) return;
        EditBtnVis = true;
        IsEditable = false;
        Database.GetMyTodaysLedger();
        IsPaneOpen = false;
        if (Database.MyLedgers.Count <= 0) return;
        SelectedLedger = Database.MyLedgers.Last();
    }

    private void AddNewLedgerBtnClk()
    {
        if (string.IsNullOrWhiteSpace(TaskName)) return;
        var record = new LedgerModel()
        {
            TaskName = TaskName,
            TaskDescription = "",
            StartDate = DateTime.Now.ToString("dd/MM/YYYY"),
            StartTime = DateTime.Now.ToString("hh:mm tt"),
            EndDate = DateTime.Now.ToString("dd/MM/YYYY"),
            EndTime = DateTime.Now.AddHours(1).ToString("hh:mm tt"),
            ProjectId = 0,
            TagId = 0
        };
        var res = Database.AddNewLedger(record);
        if (res <= 0) return;
        Database.GetMyTodaysLedger();
        SelectedLedger = Database.MyLedgers.Last();
    }

    private void CloseBtnClk()
    {
        IsPaneOpen = false;
        DataGridSelectedIndex = -1;
    }
}