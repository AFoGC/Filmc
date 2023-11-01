﻿using Filmc.Wpf.Services;
using Filmc.Xtl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Wpf.SettingsServices
{
    public class AutoSaveService
    {
        public event Action? AutosaveIsEnableChanged;
        public event Action? AutosaveIntervalChanged;

        private readonly ProfilesService _profilesService;
        private readonly System.Timers.Timer _saveTimer;

        private bool _isAutosaveEnable = false;
        private double _autosaveInterval = 30;
        private Profile? _currentProfile;

        public AutoSaveService(ProfilesService profilesService)
        {
            _profilesService = profilesService;
            _saveTimer = new System.Timers.Timer();
            _saveTimer.Elapsed += Autosave;

            OnSelectedProfileChanged(_profilesService.SelectedProfile);
            _profilesService.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public double SaveTimerInterval
        {
            get => _autosaveInterval;
            set
            {
                _autosaveInterval = value;
                _saveTimer.Interval = _autosaveInterval * 1000;
                AutosaveIntervalChanged?.Invoke();
            }
        }

        public bool IsAutosaveEnable
        {
            get => _isAutosaveEnable;
            set
            {
                _isAutosaveEnable = value;
                StopSaveTimer();
                AutosaveIsEnableChanged?.Invoke();
            }
        }

        private void OnSelectedProfileChanged(Profile profile)
        {
            if (_currentProfile != null)
            {
                TablesContext tablesContex = _currentProfile.TablesContext;
                tablesContex.TablesSaved -= OnTablesSaved;

                foreach (var table in tablesContex.Tables)
                {
                    table.CollectionChanged -= OnTableCollectionChanged;
                    table.RecordsPropertyChanged -= OnTableRecordsPropertyChanged; ;
                }
                    
            }

            _currentProfile = profile;
            _currentProfile.TablesContext.TablesSaved += OnTablesSaved;
        }

        private void OnTableRecordsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            StartSaveTimer();
        }

        private void OnTableCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            StartSaveTimer();
        }

        private void OnTablesSaved(TablesCollection sender)
        {
            StopSaveTimer();
        }

        private void Autosave(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _saveTimer.Stop();
            _currentProfile?.SaveTables();
        }

        private void StartSaveTimer()
        {
            if (_saveTimer.Enabled)
                _saveTimer.Stop();

            if (_isAutosaveEnable)
                _saveTimer.Start();
        }

        private void StopSaveTimer()
        {
            _saveTimer.Stop();
        }
    }
}
