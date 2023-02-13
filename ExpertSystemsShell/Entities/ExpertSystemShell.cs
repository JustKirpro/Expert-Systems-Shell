namespace ExpertSystemsShell.Entities;

public class ExpertSystemShell
{
    public Rules Rules { get; set; } = new();

    public Variables Variables { get; set; } = new();

    public Domains Domains { get; set; } = new();
}