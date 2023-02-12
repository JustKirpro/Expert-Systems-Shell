﻿namespace ExpertSystemsShell.Entities;

public class Variable
{
    public string Name { get; set; }

    public string? Question { get; set; }

    public Domain Domain { get; set; }

    public VariableType Type { get; set; }

    public string FormattedType => Type switch
    {
        VariableType.Requested => "Запрашимаемая",
        VariableType.Inferred => "Выводимая",
        VariableType.RequestedInferred => "Запрашиваемо-выводимая",
        _ => "Неизвестный",
    };

    public Variable(string name, Domain domain, VariableType type)
    {
        Name = name;
        Domain = domain;
        Type = type;
    }

    public Variable(string name, string question, Domain domain, VariableType type) : this(name, domain, type) => Question = question;

    public Variable() { }
}