using System.Reactive;
using Ledger.App.Models;
using ReactiveUI;

namespace Ledger.App.ViewModels;

public class SettingsViewModel: ViewModelBase
{

    private bool _isProjectEditable;

    public bool IsProjectEditable
    {
        get => _isProjectEditable;
        set => this.RaiseAndSetIfChanged(ref _isProjectEditable, value);
    }

    private bool _isTagEditable;

    public bool IsTagEditable
    {
        get => _isTagEditable;
        set => this.RaiseAndSetIfChanged(ref _isTagEditable, value);
    }

    private Database _database;

    public Database Database
    {
        get => _database;
        set => this.RaiseAndSetIfChanged(ref _database, value);
    }

    private ProjectModel _selectedProject;

    public ProjectModel SelectedProject
    {
        get => _selectedProject;
        set => this.RaiseAndSetIfChanged(ref _selectedProject, value);
    }

    private TagModel _selectedTag;

    public TagModel SelectedTag
    {
        get => _selectedTag;
        set => this.RaiseAndSetIfChanged(ref _selectedTag, value);
    }
    
    public ReactiveCommand<Unit, Unit> AddNewProjectCmd { get; set; }
    public ReactiveCommand<Unit, Unit> EditSelectedProjectCmd { get; set; }
    public ReactiveCommand<Unit, Unit> SaveSelectedProjectCmd { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteSelectedProjectCmd { get; set; }
    
    public ReactiveCommand<Unit, Unit> AddNewTagCmd { get; set; }
    public ReactiveCommand<Unit, Unit> EditSelectedTagCmd { get; set; }
    public ReactiveCommand<Unit, Unit> SaveSelectedTagCmd { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteSelectedTagCmd { get; set; }
    
    public SettingsViewModel()
    {
        _isProjectEditable = false;
        _isTagEditable = false;

        _database = new Database();
        _selectedProject = new ProjectModel();
        _selectedTag = new TagModel();

        AddNewProjectCmd = ReactiveCommand.Create(AddNewProjectBtnClk);
        EditSelectedProjectCmd = ReactiveCommand.Create(EditSelectedProjectBtnClk);
        SaveSelectedProjectCmd = ReactiveCommand.Create(SaveSelectedProjectBtnClk);
        DeleteSelectedProjectCmd = ReactiveCommand.Create(DeleteSelectedProjectBtnClk);
        
        AddNewTagCmd = ReactiveCommand.Create(AddNewTagBtnClk);
        EditSelectedTagCmd = ReactiveCommand.Create(EditSelectedTagBtnClk);
        SaveSelectedTagCmd = ReactiveCommand.Create(SaveSelectedTagBtnClk);
        DeleteSelectedTagCmd = ReactiveCommand.Create(DeleteSelectedTagBtnClk);
    }

    private void DeleteSelectedTagBtnClk()
    {
        var res = Database.DeleteTag(SelectedTag);
        if (res <= 0)
            return;
        Database.GetMyTags();
        SelectedTag = new TagModel();
        IsTagEditable = false;
    }

    private void SaveSelectedTagBtnClk()
    {
        var res = Database.AddNewTag(SelectedTag);
        if (res <= 0) return;
        Database.GetMyTags();
        SelectedTag = new TagModel();
        IsTagEditable = false;
    }

    private void EditSelectedTagBtnClk()
    {
        IsTagEditable = true;
    }

    private void AddNewTagBtnClk()
    {
        Database.MyTags.Add(new TagModel(){ TagId = Database.MyTags.Count, Tag = ""});
    }

    private void EditSelectedProjectBtnClk()
    {
        IsProjectEditable = true;
    }

    private void SaveSelectedProjectBtnClk()
    {
        var res = Database.AddNewProject(SelectedProject);
        if (res <= 0)
            return;
        Database.GetMyProjects();
        SelectedProject = new ProjectModel();
        IsProjectEditable = false;
    }

    private void DeleteSelectedProjectBtnClk()
    {
        var res = Database.DeleteProject(SelectedProject);
        if (res <= 0)
            return;
        Database.GetMyProjects();
        SelectedProject = new ProjectModel();
        IsProjectEditable = false;
    }

    private void AddNewProjectBtnClk()
    {
        Database.MyProjects.Add(new ProjectModel(){ ProjectId = Database.MyProjects.Count, ProjectName = ""});
    }
}