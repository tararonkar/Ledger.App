using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ledger.App.ViewModels;
using Microsoft.VisualBasic;
using ReactiveUI;
using SQLite;

namespace Ledger.App.Models;

public class Database: ViewModelBase
{
    private ObservableCollection<LedgerModel> _myLedgers;

    public ObservableCollection<LedgerModel> MyLedgers
    {
        get => _myLedgers;
        set => this.RaiseAndSetIfChanged(ref _myLedgers, value);
    }

    private ObservableCollection<ProjectModel> _myProjects;

    public ObservableCollection<ProjectModel> MyProjects
    {
        get => _myProjects;
        set => this.RaiseAndSetIfChanged(ref _myProjects, value);
    }

    private ObservableCollection<TagModel> _myTags;

    public ObservableCollection<TagModel> MyTags
    {
        get => _myTags;
        set => this.RaiseAndSetIfChanged(ref _myTags, value);
    }

    public Database()
    {
        _myLedgers = new ObservableCollection<LedgerModel>();
        _myProjects = new ObservableCollection<ProjectModel>();
        _myTags = new ObservableCollection<TagModel>();
        
        GetMyTags();
        GetMyProjects();
        GetMyLedger();
    }
    public void GetMyLedger()
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        var list = conn.Table<LedgerModel>().ToList();
        MyLedgers.Clear();
        if (list.Count <= 0) return;
        foreach (var ledger in list)
        {
            MyLedgers.Add(ledger);
        }
    }

    public void GetMyPlannedLedger()
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        var list = conn.Table<LedgerModel>().Where((x) => x.TagId == 0).ToList();
        MyLedgers.Clear();
        if (list.Count <= 0) return;
        foreach (var ledger in list)
        {
            MyLedgers.Add(ledger);
        }
    }
    
    public void GetMyUnplannedLedger()
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        var list = conn.Table<LedgerModel>().Where((x) => x.TagId == 1).ToList();
        MyLedgers.Clear();
        if (list.Count <= 0) return;
        foreach (var ledger in list)
        {
            MyLedgers.Add(ledger);
        }
    }
    
    public void GetMyTodaysLedger()
    {
        var today = DateTime.Now.ToString("dd/MM/yyyy");
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        var list = conn.Table<LedgerModel>().Where(ledger => ledger.StartDate.Contains(today)).ToList();
        MyLedgers.Clear();
        if (list.Count <= 0) return;
        foreach (var ledger in list)
        {
            MyLedgers.Add(ledger);
        }
    }

    public void GetMyProjects()
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<ProjectModel>();
        var list = conn.Table<ProjectModel>().ToList();
        MyProjects.Clear();
        if (list.Count <= 0) return;
        foreach (var project in list)
        {
            MyProjects.Add(project);
        }
    }

    public void GetMyTags()
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<TagModel>();
        var list = conn.Table<TagModel>().ToList();
        MyTags.Clear();
        if (list.Count <= 0) return;
        foreach (var tag in list)
        {
            MyTags.Add(tag);
        }
    }

    public int AddNewLedger(LedgerModel ledger)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        return conn.Insert(ledger);
    }

    public int AddNewProject(ProjectModel project)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<ProjectModel>();
        return conn.Insert(project);
    }

    public int AddNewTag(TagModel tag)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<TagModel>();
        return conn.Insert(tag);
    }
    
    public int UpdateLedgerRecord(LedgerModel ledger)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        return conn.Update(ledger);
    }

    public int UpdateProject(ProjectModel project)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<ProjectModel>();
        return conn.Update(project);
    }

    public int UpdateTag(TagModel tag)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<TagModel>();
        return conn.Update(tag);
    }
    
    public int DeleteLedgerRecord(LedgerModel ledger)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<LedgerModel>();
        return conn.Delete(ledger);
    }

    public int DeleteProject(ProjectModel project)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<ProjectModel>();
        return conn.Delete(project);
    }

    public int DeleteTag(TagModel tag)
    {
        using var conn = new SQLiteConnection(App.ConnString);
        conn.CreateTable<TagModel>();
        return conn.Delete(tag);
    }

    
    
}