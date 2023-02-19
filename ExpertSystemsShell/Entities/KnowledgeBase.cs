﻿using System.Linq;

namespace ExpertSystemsShell.Entities;

public class KnowledgeBase
{
    public Rules Rules { get; } = new();

    public Variables Variables { get; } = new();

    public Domains Domains { get; } = new();

    public bool IsVariableUsed(Variable variable) => Rules.Any(r => r.ConditionPart.Concat(r.ActionPart).Any(f => f.Variable == variable));

    public bool IsDomainUsed(Domain domain) => Variables.Any(v => v.Domain == domain);
}