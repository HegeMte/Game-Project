﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "das", Scope = "member", Target = "~P:StorageRepository.IStorageRepository.ChestList")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "dasd", Scope = "member", Target = "~P:StorageRepository.IStorageRepository.ChestList")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "dasd", Scope = "member", Target = "~P:StorageRepository.StorageRepo.ChestList")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "dasd", Scope = "member", Target = "~M:StorageRepository.StorageRepo.SaveHighScore(GameModel.Models.IGameModel)")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "adsdasd", Scope = "member", Target = "~M:StorageRepository.StorageRepo.LoadHighScores~System.String[]")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "asdasd", Scope = "member", Target = "~M:StorageRepository.StorageRepo.SaveGame(GameModel.Models.IGameModel)")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "asdas", Scope = "member", Target = "~M:StorageRepository.StorageRepo.LoadGame(System.String)~GameModel.Models.GameModel")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "sdfsd", Scope = "member", Target = "~M:StorageRepository.StorageRepo.LoadGame(System.String)~GameModel.Models.GameModel")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "dasd", Scope = "member", Target = "~M:StorageRepository.StorageRepo.SaveGame(GameModel.Models.IGameModel)")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "dasd", Scope = "member", Target = "~M:StorageRepository.StorageRepo.LoadGame(System.String)~GameModel.Models.GameModel")]
