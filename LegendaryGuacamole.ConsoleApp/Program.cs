﻿using System.CommandLine;
using LegendaryGuacamole.ConsoleApp.Commands;

HttpClient httpClient = new()
{
    BaseAddress = new("http://localhost:5152/")
};

RootCommand rootCommand = new("Gestion des finances personnelles");

List<ConsoleCommand> commands = [
    new PrintBilling(),
    new PrintRepetitiveBilling(),
    new PrintBalance(),
    new AddBilling(),
    new AddRepetitiveBilling(),
    new InsertNextBilling(),
    new MultipleInsertNextBilling(),
    new EditBilling(),
    new EditRepetitiveBilling(),
    new DeleteBilling(),
    new DeleteRepetitiveBilling(),
    new ListBillings(),
    new ListRepetitiveBillings(),
];

commands.ForEach(c => rootCommand.AddCommand(c.GetCommand(httpClient)));

var exitCode = await rootCommand.InvokeAsync(args);

return exitCode;