using System.Collections.Generic;
using System.Text;

namespace ExpertSystemsShell.Entities;

public class Rule
{
    public string Name { get; set; }

    public string Reason { get; set; }

    public List<Fact> ConditionPart { get; set; }
    
    public List<Fact> ActionPart { get; set; }

    public string FormattedRule 
    { 
        get
        {
            var stringBuilder = new StringBuilder();

            if (ConditionPart.Count > 0)
            {
                stringBuilder.Append("ЕСЛИ ");
            }

            for (var i = 0; i < ConditionPart.Count; i++)
            {
                stringBuilder.Append(ConditionPart[i].FormattedFact);

                if (i < ConditionPart.Count - 1)
                {
                    stringBuilder.Append(" И ");
                }
            }

            if (ConditionPart.Count > 0)
            {
                stringBuilder.Append(" ТО ");
            }    

            for (var i = 0; i < ActionPart.Count; i++)
            {
                stringBuilder.Append(ActionPart[i].FormattedFact);

                if (i < ActionPart.Count - 1)
                {
                    stringBuilder.Append(" И ");
                }
            }

            return stringBuilder.ToString();
        } 
    }

    public Rule(string name, string reason, List<Fact> conditionPart, List<Fact> actionPart)
    {
        Name = name;
        Reason = reason;
        ConditionPart = conditionPart;
        ActionPart = actionPart;
    }
}